using System;
using GraphQL.Language.AST;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.AstValueNodes;

/// <summary>
/// Represents a <see cref="Guid"/> value within a document.
/// </summary>
public class GuidValue : ValueNode<Guid>
{
    /// <summary>
    /// Initializes a new instance with the specified value.
    /// </summary>
    public GuidValue(Guid value) : base(value)
    {
    }
}