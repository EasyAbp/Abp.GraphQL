using EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.GraphQL;

[DependsOn(
    typeof(AbpGraphQLApplicationTestModule),
    typeof(AbpAbpGraphQLProviderGraphQLDotnetModule),
    typeof(GraphQLProviderSharedTestModule)
)]
public class GraphQLProviderGraphQLDotnetTestModule : AbpModule
{
}