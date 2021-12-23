using System;
using GraphQL.Language.AST;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.AstValueNodes;

/// <summary>
/// Represents a <see cref="DateTime"/> value within a document.
/// </summary>
public class DateTimeValue : ValueNode<DateTime>
{
    /// <summary>
    /// Initializes a new instance with the specified value.
    /// </summary>
    public DateTimeValue(DateTime value) : base(value)
    {
    }
}