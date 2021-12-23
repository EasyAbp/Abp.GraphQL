using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.GraphTypes;
using GraphQL.Types;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Reflection;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;

public static class GraphTypeMapper
{
    public static readonly IReadOnlyDictionary<Type, Type> BuiltInScalarMappings = new Dictionary<Type, Type>
    {
        [typeof(int)] = typeof(IntGraphType),
        [typeof(long)] = typeof(LongGraphType),
        [typeof(BigInteger)] = typeof(BigIntGraphType),
        [typeof(double)] = typeof(FloatGraphType),
        [typeof(float)] = typeof(FloatGraphType),
        [typeof(decimal)] = typeof(DecimalGraphType),
        [typeof(string)] = typeof(StringGraphType),
        [typeof(bool)] = typeof(BooleanGraphType),
        [typeof(DateTime)] = typeof(DateTimeGraphType),
#if NET6_0_OR_GREATER
        [typeof(DateOnly)] = typeof(DateOnlyGraphType),
        [typeof(TimeOnly)] = typeof(TimeOnlyGraphType),
#endif
        [typeof(DateTimeOffset)] = typeof(DateTimeOffsetGraphType),
        [typeof(TimeSpan)] = typeof(TimeSpanSecondsGraphType),
        [typeof(Guid)] = typeof(GuidGraphType),
        [typeof(short)] = typeof(ShortGraphType),
        [typeof(ushort)] = typeof(UShortGraphType),
        [typeof(ulong)] = typeof(ULongGraphType),
        [typeof(uint)] = typeof(UIntGraphType),
        [typeof(byte)] = typeof(ByteGraphType),
        [typeof(sbyte)] = typeof(SByteGraphType),
        [typeof(Uri)] = typeof(UriGraphType),
    };

    public static bool IsBuiltInScalar(Type clrType)
    {
        return BuiltInScalarMappings.ContainsKey(clrType);
    }
    
    public static Type GetGraphType(Type clrType, bool isInput = false, bool autoNonNull = false)
    {
        var type = clrType.GetFirstGenericArgumentIfNullable();

        var graphType = IsBuiltInScalar(type)
            ? BuiltInScalarMappings[type]
            : GetGraphTypeOfNonBuiltInType(type, isInput);

        if (graphType == typeof(StringGraphType))
        {
            return graphType;
        }
        
        return autoNonNull && isInput && !TypeHelper.IsNullable(clrType)
            ? typeof(NonNullGraphType<>).MakeGenericType(graphType)
            : graphType;
    }
    
    private static Type GetGraphTypeOfNonBuiltInType(Type type, bool isInput = false)
    {
        if (type.IsEnum)
        {
            return typeof(IntGraphType);
        }
        
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
        {
            return isInput ? typeof(GraphQLInputGenericType<IDictionary<string, object>>) : typeof(StringGraphType);
        }

        var graphType = Assembly.GetAssembly(typeof(ISchema)).GetTypes()
            .FirstOrDefault(t => t.Name == $"{type.Name}Type" && t.IsAssignableTo<IGraphType>());

        return graphType ?? (isInput
            ? typeof(GraphQLInputGenericType<>).MakeGenericType(type)
            : typeof(GraphQLGenericType<>).MakeGenericType(type));
    }
}