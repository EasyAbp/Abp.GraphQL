using System.Globalization;
using GraphQLParser;
using GraphQLParser.AST;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.AstValueNodes;

/// <summary>
/// Represents a <see cref="short"/> value within a document.
/// </summary>
public class ShortValue : GraphQLValue, IHasValueNode
{
    public override ASTNodeKind Kind => ASTNodeKind.IntValue;

    public ROM Value { get; }

    /// <summary>
    /// Initializes a new instance with the specified value.
    /// </summary>
    public ShortValue(short value)
    {
        Value = value.ToString(CultureInfo.InvariantCulture);
    }
}