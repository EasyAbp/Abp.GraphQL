using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace EasyAbp.Abp.GraphQL;

[DependsOn(
    typeof(AbpGraphQLApplicationContractsModule),
    typeof(AbpGraphQLProviderSharedModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpMapperlyModule)
)]
public class AbpGraphQLApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<AbpGraphQLApplicationModule>();
    }
}
