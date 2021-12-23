using System;
using EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.GraphTypes;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;

public class AppServiceQuery<TAppService, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput> : ObjectGraphType, ITransientDependency
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

        var appService = serviceProvider.GetRequiredService<TAppService>();
            
        var readOnlyAppService =
            (IReadOnlyAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>)appService;

        FieldAsync<GraphQLGenericType<TGetOutputDto>>(entityName,
            arguments: new QueryArguments(
                new QueryArgument(typeof(NonNullGraphType<IdGraphType>)) { Name = "id" }),
            resolve: async context =>
                await readOnlyAppService.GetAsync(context.GetArgument<TKey>("id"))
        );

        FieldAsync<PagedResultDtoType<TGetListOutputDto>>($"{entityName}List",
            arguments: new QueryArguments(
                new QueryArgument<GraphQLInputGenericType<TGetListInput>>
                    { Name = "input", DefaultValue = Activator.CreateInstance<TGetListInput>() }),
            resolve: async context =>
                await readOnlyAppService.GetListAsync(context.GetArgument<TGetListInput>("input"))
        );
    }
}