using System.Globalization;
using GraphQLParser;
using GraphQLParser.AST;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.AstValueNodes;

/// <summary>
/// Represents a <see cref="byte"/> value within a document.
/// </summary>
public class ByteValue : GraphQLValue, IHasValueNode
{
    public override ASTNodeKind Kind => ASTNodeKind.IntValue;

    public ROM Value { get; }

    /// <summary>
    /// Initializes a new instance with the specified value.
    /// </summary>
    public ByteValue(byte value)
    {
        Value = value.ToString(CultureInfo.InvariantCulture);
    }
}