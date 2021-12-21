using System;
using System.Collections.Generic;
using System.Numerics;
using GraphQL.Types;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Reflection;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;

public class GraphTypeMapper : IGraphTypeMapper, ITransientDependency
{
    private static readonly Dictionary<Type, Type> BuiltInScalarMappings = new()
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
        [typeof(Guid)] = typeof(IdGraphType),
        [typeof(short)] = typeof(ShortGraphType),
        [typeof(ushort)] = typeof(UShortGraphType),
        [typeof(ulong)] = typeof(ULongGraphType),
        [typeof(uint)] = typeof(UIntGraphType),
        [typeof(byte)] = typeof(ByteGraphType),
        [typeof(sbyte)] = typeof(SByteGraphType),
        [typeof(Uri)] = typeof(UriGraphType),
    };
    
    public virtual Type GetGraphType(Type clrType)
    {
        var type = clrType.GetFirstGenericArgumentIfNullable();

        var graphType = BuiltInScalarMappings.ContainsKey(type)
            ? BuiltInScalarMappings[type]
            : GetGraphTypeOfNonPrimitiveType(clrType);

        return clrType.GetGenericTypeDefinition() == typeof(Nullable<>)
            ? graphType
            : typeof(NonNullGraphType).MakeGenericType(graphType);
    }
    
    protected virtual Type GetGraphTypeOfNonPrimitiveType(Type entityPropertyType)
    {
        throw new NotImplementedException();
    }
}