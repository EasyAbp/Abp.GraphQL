using GraphQL.Language.AST;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.AstValueNodes;

/// <summary>
/// Represents a <see cref="short"/> value within a document.
/// </summary>
public class ShortValue : ValueNode<short>
{
    /// <summary>
    /// Initializes a new instance with the specified value.
    /// </summary>
    public ShortValue(short value) : base(value)
    {
    }
}