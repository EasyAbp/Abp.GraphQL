using EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.GraphQL;

[DependsOn(
    typeof(AbpGraphQLApplicationTestModule),
    typeof(AbpAbpGraphQLProviderGraphQLDotnetModule)
)]
public class GraphQLProviderGraphQLDotnetTestModule : AbpModule
{
}