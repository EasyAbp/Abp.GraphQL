using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;
using GraphQLParser.AST;
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

    public override object ParseLiteral(GraphQLValue value) => value switch
    {
        GraphQLObjectValue o => ParseValue(o),
        GraphQLNullValue _ => ParseValue(null),
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

    public override GraphQLValue ToAST(object value)
    {
        return new GraphQLNullValue();
    }
}