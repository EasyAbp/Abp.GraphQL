using EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.GraphTypes;
using GraphQL;
using GraphQL.SystemTextJson;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;

[DependsOn(
    typeof(AbpGraphQLApplicationContractsModule),
    typeof(AbpGraphQLProviderSharedModule)
)]
public class AbpAbpGraphQLProviderGraphQLDotnetModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.TryAddTransient(typeof(IDocumentWriter), typeof(DocumentWriter));
        context.Services.TryAddTransient(typeof(IDocumentExecuter), typeof(DocumentExecuter));
            
        context.Services.TryAddTransient(typeof(GraphQLGenericType<>));
        context.Services.TryAddTransient(typeof(GraphQLInputGenericType<>));
            
        context.Services.TryAddTransient(typeof(ListResultDtoType<>));
        context.Services.TryAddTransient(typeof(PagedResultDtoType<>));
            
        context.Services.TryAddTransient(typeof(AppServiceQuery<,,,,>));
    }
}