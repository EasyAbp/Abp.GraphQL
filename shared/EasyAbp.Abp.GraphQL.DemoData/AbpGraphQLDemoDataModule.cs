using Volo.Abp.Application;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.GraphQL;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(AbpDddDomainModule),
    typeof(AbpDddApplicationContractsModule)
)]
public class AbpGraphQLDemoDataModule : AbpModule
{
}