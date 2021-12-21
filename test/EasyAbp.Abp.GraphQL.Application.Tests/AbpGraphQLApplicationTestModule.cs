using Volo.Abp.Modularity;

namespace EasyAbp.Abp.GraphQL;

[DependsOn(
    typeof(AbpGraphQLApplicationModule),
    typeof(AbpGraphQLTestBaseModule)
)]
public class AbpGraphQLApplicationTestModule : AbpModule
{
}