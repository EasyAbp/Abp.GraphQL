using GraphQL.Types;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.GraphQL;

public class HelloWorldSchema : Schema, ITransientDependency
{
    public HelloWorldSchema(HelloWorldQuery query)
    {
        Query = query;
    }
}