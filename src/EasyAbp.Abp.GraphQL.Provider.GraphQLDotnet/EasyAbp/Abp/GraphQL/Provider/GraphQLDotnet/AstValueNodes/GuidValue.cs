using System;
using GraphQLParser;
using GraphQLParser.AST;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.AstValueNodes;

/// <summary>
/// Represents a <see cref="Guid"/> value within a document.
/// </summary>
public class GuidValue : GraphQLValue, IHasValueNode
{
    public override ASTNodeKind Kind => ASTNodeKind.StringValue;

    public ROM Value { get; }

    /// <summary>
    /// Initializes a new instance with the specified value.
    /// </summary>
    public GuidValue(Guid value)
    {
        Value = value.ToString();
    }
}