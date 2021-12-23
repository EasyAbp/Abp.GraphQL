using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL;
using GraphQL.Language.AST;
using GraphQL.Types;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.GraphTypes;

public class DictionaryGraphType<TKey, TValue> : ScalarGraphType
{
    public DictionaryGraphType()
    {
        Name = MakeName();
    }
    
    public override object ParseValue(object value)
    {
        if (value == null)
        {
            return new Dictionary<TKey, TValue>();
        }

        return ((IEnumerable<KeyValuePair<TKey, TValue>>)value).ToDictionary(x => x.Key, x => x.Value);
    }

    public override object ParseLiteral(IValue value) => value switch
    {
        ObjectValue o => ParseValue(o.Value),
        NullValue _ => ParseValue(null),
        _ => ThrowLiteralConversionError(value)
    };

    public override bool IsValidDefault(object value)
    {
        if (value == null)
        {
            return false;
        }

        if (value.GetType().IsAssignableToGenericType(typeof(IDictionary<,>)))
        {
            return true;
        }

        return false;
    }

    public override IValue ToAST(object value)
    {
        return new NullValue();
    }
    
    private static string MakeName()
    {
        var keyName = GraphTypeMapper.BuiltInScalarMappings.ContainsKey(typeof(TKey))
            ? ((IGraphType)Activator.CreateInstance(GraphTypeMapper.BuiltInScalarMappings[typeof(TKey)])).Name
            : typeof(TKey).GetNamedType().Name;
        
        var valueName = GraphTypeMapper.BuiltInScalarMappings.ContainsKey(typeof(TValue))
            ? ((IGraphType)Activator.CreateInstance(GraphTypeMapper.BuiltInScalarMappings[typeof(TValue)])).Name
            : typeof(TValue).GetNamedType().Name;
        
        return $"Dictionary_{keyName}_{valueName}";
    }
}