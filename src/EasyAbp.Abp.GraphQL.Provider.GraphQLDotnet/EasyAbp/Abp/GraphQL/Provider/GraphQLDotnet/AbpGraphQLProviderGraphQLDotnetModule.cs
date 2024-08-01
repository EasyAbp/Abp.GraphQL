using EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.GraphTypes;
using GraphQL;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;

[DependsOn(
    typeof(AbpGraphQLApplicationContractsModule),
    typeof(AbpGraphQLProviderSharedModule)
)]
public class AbpGraphQLProviderGraphQLDotnetModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddGraphQL(builder => builder.AddSystemTextJson());

        context.Services.TryAddTransient(typeof(GraphQLGenericType<>));
        context.Services.TryAddTransient(typeof(GraphQLInputGenericType<>));

        context.Services.TryAddTransient(typeof(ListResultDtoType<>));
        context.Services.TryAddTransient(typeof(PagedResultDtoType<>));
        context.Services.TryAddTransient(typeof(AbpExtraPropertyGraphType));
        context.Services.TryAddTransient(typeof(DictionaryGraphType<,>));

        context.Services.TryAddTransient(typeof(AppServiceQuery<,,,,>));
    }
}