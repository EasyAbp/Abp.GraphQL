using GraphQL.Types;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.GraphQL;

[ExposeServices(typeof(HelloWorldQuery))]
public class HelloWorldQuery : ObjectGraphType, ITransientDependency
{
    public HelloWorldQuery()
    {
        Name = "HelloWorldQuery";
            
        Field<StringGraphType>("ping", resolve: _ => "pong");
    }
}