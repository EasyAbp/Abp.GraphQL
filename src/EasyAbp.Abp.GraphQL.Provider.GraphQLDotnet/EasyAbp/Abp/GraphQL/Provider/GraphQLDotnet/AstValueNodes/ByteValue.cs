using GraphQL.Language.AST;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.AstValueNodes;

/// <summary>
/// Represents a <see cref="byte"/> value within a document.
/// </summary>
public class ByteValue : ValueNode<byte>
{
    /// <summary>
    /// Initializes a new instance with the specified value.
    /// </summary>
    public ByteValue(byte value) : base(value)
    {
    }
}