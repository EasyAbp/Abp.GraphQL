using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.GraphQL;

[DependsOn(
    typeof(AbpDddApplicationContractsModule)
)]
public class AbpGraphQLProviderSharedModule : AbpModule
{
}