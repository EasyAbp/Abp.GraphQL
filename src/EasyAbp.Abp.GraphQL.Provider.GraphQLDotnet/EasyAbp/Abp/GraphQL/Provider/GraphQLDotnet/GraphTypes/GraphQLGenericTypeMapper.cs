using System;
using System.Linq;
using System.Reflection;
using GraphQL.Types;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.GraphTypes;

/// <summary>
/// https://github.com/fenomeno83/graphql-dotnet-auto-types
/// </summary>
public class GraphQLGenericTypeMapper
{
    public Type ResolveGraphType(Type type, bool isInput = false)
    {
        if (!type.Namespace.StartsWith("System"))
        {
            if (type.IsEnum)
            {
                return typeof(IntGraphType);
            }
            else
            {
                var graphType = Assembly.GetAssembly(typeof(ISchema)).GetTypes().FirstOrDefault(t => t.Name == $"{type.Name}Type" && t.IsAssignableTo<IGraphType>());

                return graphType ?? typeof(GraphQLGenericType<>).MakeGenericType(type);
            }
        }
        else
        {
            switch (type.Name)
            {
                case nameof(Int32):
                case nameof(Int64):
                case nameof(Enum):
                case nameof(Int16):
                case nameof(Byte): return typeof(IntGraphType);

                case nameof(Boolean): return typeof(BooleanGraphType);
                case nameof(DateTime): return typeof(DateTimeGraphType);
                case nameof(Double): return typeof(FloatGraphType);
                case nameof(Single): return typeof(FloatGraphType);
                case nameof(Decimal): return typeof(DecimalGraphType);
                case nameof(Guid): return typeof(IdGraphType);

                case nameof(String):
                default: return typeof(StringGraphType);;
            }
        }
    }
}