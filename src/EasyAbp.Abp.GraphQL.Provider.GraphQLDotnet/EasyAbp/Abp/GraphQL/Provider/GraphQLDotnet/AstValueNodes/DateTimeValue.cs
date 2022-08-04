using System;
using System.Globalization;
using GraphQLParser;
using GraphQLParser.AST;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.AstValueNodes;

/// <summary>
/// Represents a <see cref="DateTime"/> value within a document.
/// </summary>
public class DateTimeValue : GraphQLValue, IHasValueNode
{
    public override ASTNodeKind Kind => ASTNodeKind.StringValue;

    public ROM Value { get; }

    /// <summary>
    /// Initializes a new instance with the specified value.
    /// </summary>
    public DateTimeValue(DateTime value)
    {
        Value = value.ToString(CultureInfo.InvariantCulture);
    }
}