using Volo.Abp.Modularity;

namespace EasyAbp.Abp.GraphQL;

[DependsOn(
    typeof(AbpGraphQLTestBaseModule),
    typeof(AbpGraphQLProviderSharedModule)
)]
public class GraphQLProviderSharedTestModule : AbpModule
{
}