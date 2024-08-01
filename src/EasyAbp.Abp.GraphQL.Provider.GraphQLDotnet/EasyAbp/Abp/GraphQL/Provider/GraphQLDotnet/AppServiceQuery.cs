using System;
using EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.GraphTypes;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Claims;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;

public class AppServiceQuery<TAppService, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput> : ObjectGraphType,
    ITransientDependency
    where TGetListInput : class
    where TGetOutputDto : class
{
    protected AppServiceQuery()
    {
    }

    public AppServiceQuery(IServiceProvider serviceProvider)
    {
        var entityName = typeof(TGetOutputDto).Name.RemovePostFix("Dto");

        Name = entityName + "Query";

        FieldAsync<GraphQLGenericType<TGetOutputDto>>(entityName,
            arguments: new QueryArguments(new QueryArgument(MakeGetInputType()) { Name = "id" }),
            resolve: async context =>
            {
                using var scope = serviceProvider.CreateScope();
                var principalAccessor = scope.ServiceProvider.GetRequiredService<ICurrentPrincipalAccessor>();
                using var changePrincipal = principalAccessor.Change(context.User);

                var appService = serviceProvider.GetRequiredService<TAppService>();

                var readOnlyAppService =
                    (IReadOnlyAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>)appService;

                return await readOnlyAppService.GetAsync(context.GetArgument<TKey>("id"));
            }
        );

        FieldAsync<PagedResultDtoType<TGetListOutputDto>>($"{entityName}List",
            arguments: new QueryArguments(
                new QueryArgument<GraphQLInputGenericType<TGetListInput>>
                    { Name = "input", DefaultValue = Activator.CreateInstance<TGetListInput>() }),
            resolve: async context =>
            {
                using var scope = serviceProvider.CreateScope();
                var principalAccessor = scope.ServiceProvider.GetRequiredService<ICurrentPrincipalAccessor>();
                using var changePrincipal = principalAccessor.Change(context.User);

                var appService = serviceProvider.GetRequiredService<TAppService>();

                var readOnlyAppService =
                    (IReadOnlyAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>)appService;

                return await readOnlyAppService.GetListAsync(context.GetArgument<TGetListInput>("input"));
            }
        );
    }

    private static Type MakeGetInputType()
    {
        return GraphTypeMapper.GetGraphType(typeof(TKey), true, true);
    }
}