using System;
using EasyAbp.Abp.GraphQL.Authors;
using EasyAbp.Abp.GraphQL.Authors.Dtos;
using EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;
using EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.GraphTypes;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.GraphQL;

[ExposeServices(typeof(AppServiceQuery<IAuthorAppService, AuthorDto, AuthorDto, int, PagedAndSortedResultRequestDto>))]
public class AuthorAppServiceQuery :
    AppServiceQuery<IAuthorAppService, AuthorDto, AuthorDto, int, PagedAndSortedResultRequestDto>
{
    public AuthorAppServiceQuery(IServiceProvider serviceProvider)
    {
        const string entityName = "Author";
            
        Name = entityName + "Query";

        var appService = serviceProvider.GetRequiredService<IAuthorAppService>();
            
        var readOnlyAppService =
            (IReadOnlyAppService<AuthorDto, AuthorDto, int, PagedAndSortedResultRequestDto>)appService;

        FieldAsync<GraphQLGenericType<AuthorDto>>(entityName,
            arguments: new QueryArguments(
                new QueryArgument(typeof(NonNullGraphType<IdGraphType>)) { Name = "id" }),
            resolve: async context =>
                await readOnlyAppService.GetAsync(context.GetArgument<int>("id"))
        );

        FieldAsync<PagedResultDtoType<AuthorDto>>($"{entityName}List",
            arguments: new QueryArguments(
                new QueryArgument<GraphQLInputGenericType<PagedAndSortedResultRequestDto>>
                    { Name = "input", DefaultValue = Activator.CreateInstance<PagedAndSortedResultRequestDto>() }),
            resolve: async context =>
                await readOnlyAppService.GetListAsync(context.GetArgument<PagedAndSortedResultRequestDto>("input"))
        );
            
        // An extra field.
        Field<StringGraphType>("ping", resolve: _ => "pong");
    }
}