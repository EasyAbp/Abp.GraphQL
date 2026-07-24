using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Domain;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.GraphQL;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(AbpDddDomainModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpMapperlyModule)
)]
public class AbpGraphQLDemoDataModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<AbpGraphQLDemoDataModule>();
    }
}
