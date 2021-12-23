using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Language.AST;
using GraphQL.Types;
using Volo.Abp.Data;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.GraphTypes;

public class AbpExtraPropertyGraphType : ScalarGraphType
{
    public override object ParseValue(object value)
    {
        if (value == null)
        {
            return new ExtraPropertyDictionary();
        }

        var dictionary = ((IEnumerable<KeyValuePair<string, object>>)value).ToDictionary(x => x.Key, x => x.Value);
        
        return new ExtraPropertyDictionary(dictionary);
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

        if (value.GetType().IsAssignableTo(typeof(ExtraPropertyDictionary)))
        {
            return true;
        }

        return false;
    }

    public override IValue ToAST(object value)
    {
        return new NullValue();
    }
}