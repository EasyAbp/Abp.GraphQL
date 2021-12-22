using GraphQL.Types;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;

public class EmptyQuery : ObjectGraphType
{
    public EmptyQuery()
    {
        Name = nameof(EmptyQuery);
        
        Field<StringGraphType>("ping", resolve: _ => "pong");
    }
}