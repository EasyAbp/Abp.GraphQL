using System;
using EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.GraphTypes;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

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

        Field<GraphQLGenericType<TGetOutputDto>>(entityName)
            .Arguments(new QueryArguments(new QueryArgument(MakeGetInputType()) { Name = "id" }))
            .ResolveAsync(async context =>
            {
                using var scope = serviceProvider.CreateScope();

                var service =
                    (IReadOnlyAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>)scope.ServiceProvider
                        .GetRequiredService<TAppService>();

                return await service.GetAsync(context.GetArgument<TKey>("id"));
            });

        Field<PagedResultDtoType<TGetListOutputDto>>($"{entityName}List")
            .Arguments(new QueryArguments(
                new QueryArgument<GraphQLInputGenericType<TGetListInput>>
                    { Name = "input", DefaultValue = Activator.CreateInstance<TGetListInput>() }))
            .ResolveAsync(async context =>
            {
                using var scope = serviceProvider.CreateScope();

                var service =
                    (IReadOnlyAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>)scope.ServiceProvider
                        .GetRequiredService<TAppService>();

                return await service.GetListAsync(context.GetArgument<TGetListInput>("input"));
            });
    }

    private static Type MakeGetInputType()
    {
        return GraphTypeMapper.GetGraphType(typeof(TKey), true, true);
    }
}