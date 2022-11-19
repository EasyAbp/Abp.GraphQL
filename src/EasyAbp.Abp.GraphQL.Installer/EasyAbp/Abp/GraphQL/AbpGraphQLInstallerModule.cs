using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.Abp.GraphQL;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
)]
public class AbpGraphQLInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpGraphQLInstallerModule>();
        });
    }
}