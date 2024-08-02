using System;
using EasyAbp.Abp.GraphQL.Authors;
using EasyAbp.Abp.GraphQL.Authors.Dtos;
using EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;
using EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.GraphTypes;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.GraphQL;

[ExposeServices(typeof(AppServiceQuery<IAuthorAppService, AuthorDto, AuthorDto, int, PagedAndSortedResultRequestDto>))]
public class AuthorAppServiceQuery :
    AppServiceQuery<IAuthorAppService, AuthorDto, AuthorDto, int, PagedAndSortedResultRequestDto>, IObjectGraphType
{
    public AuthorAppServiceQuery(IServiceProvider serviceProvider)
    {
        const string entityName = "Author";

        Name = entityName + "Query";

        Field<GraphQLGenericType<AuthorDto>>(entityName)
            .Arguments(new QueryArguments(
                new QueryArgument(typeof(NonNullGraphType<IntGraphType>)) { Name = "id" }))
            .ResolveAsync(async context =>
            {
                using var scope = serviceProvider.CreateScope();

                var service = scope.ServiceProvider.GetRequiredService<IAuthorAppService>();

                return await service.GetAsync(context.GetArgument<int>("id"));
            });

        Field<PagedResultDtoType<AuthorDto>>($"{entityName}List")
            .Arguments(new QueryArguments(
                new QueryArgument<GraphQLInputGenericType<PagedAndSortedResultRequestDto>>
                    { Name = "input", DefaultValue = Activator.CreateInstance<PagedAndSortedResultRequestDto>() }))
            .ResolveAsync(async context =>
            {
                using var scope = serviceProvider.CreateScope();

                var service = scope.ServiceProvider.GetRequiredService<IAuthorAppService>();

                return await service.GetListAsync(context.GetArgument<PagedAndSortedResultRequestDto>("input"));
            });

        // An extra field.
        Field<StringGraphType>("ping", resolve: _ => "pong");
    }
}