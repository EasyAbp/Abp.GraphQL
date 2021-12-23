using System;
using System.Numerics;
using GraphQL.Language.AST;
using Volo.Abp.Reflection;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.AstValueNodes;

public static class AstNodeTypeHelper
{
    public static Type ToAstValueNodeType(Type type, bool useTypeInNullable)
    {
        var targetType = useTypeInNullable ? type.GetFirstGenericArgumentIfNullable() : type;
        
        if (targetType.Name == "List`1")
        {
            return typeof(ListValue);
        }

        if (targetType.IsEnum)
        {
            return typeof(EnumValue);
        }
        
        switch (targetType.Name)
        {
            case nameof(Enum): return typeof(EnumValue);
            case nameof(Byte): return typeof(ByteValue);
            case nameof(Int16): return typeof(ShortValue);
            case nameof(Int32): return typeof(IntValue);
            case nameof(Int64): return typeof(LongValue);
            case nameof(BigInteger): return typeof(BigIntValue);

            case nameof(Boolean): return typeof(BooleanValue);
            case nameof(DateTime): return typeof(DateTimeValue);
            case nameof(Double): return typeof(FloatValue);
            case nameof(Single): return typeof(FloatValue);
            case nameof(Decimal): return typeof(DecimalValue);
            case nameof(Guid): return typeof(GuidValue);

            case nameof(String): return typeof(StringValue);
            default: return typeof(ObjectValue);
        }
    }

}