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
using Volo.Abp.Security.Claims;

namespace EasyAbp.Abp.GraphQL;

[ExposeServices(typeof(AppServiceQuery<IAuthorAppService, AuthorDto, AuthorDto, int, PagedAndSortedResultRequestDto>))]
public class AuthorAppServiceQuery :
    AppServiceQuery<IAuthorAppService, AuthorDto, AuthorDto, int, PagedAndSortedResultRequestDto>
{
    public AuthorAppServiceQuery(IServiceProvider serviceProvider)
    {
        const string entityName = "Author";

        Name = entityName + "Query";

        FieldAsync<GraphQLGenericType<AuthorDto>>(entityName,
            arguments: new QueryArguments(
                new QueryArgument(typeof(NonNullGraphType<IntGraphType>)) { Name = "id" }),
            resolve: async context =>
            {
                using var scope = serviceProvider.CreateScope();
                var principalAccessor = scope.ServiceProvider.GetRequiredService<ICurrentPrincipalAccessor>();
                using var changePrincipal = principalAccessor.Change(context.User);

                var service = serviceProvider.GetRequiredService<IAuthorAppService>();

                return await service.GetAsync(context.GetArgument<int>("id"));
            }
        );

        FieldAsync<PagedResultDtoType<AuthorDto>>($"{entityName}List",
            arguments: new QueryArguments(
                new QueryArgument<GraphQLInputGenericType<PagedAndSortedResultRequestDto>>
                    { Name = "input", DefaultValue = Activator.CreateInstance<PagedAndSortedResultRequestDto>() }),
            resolve: async context =>
            {
                using var scope = serviceProvider.CreateScope();
                var principalAccessor = scope.ServiceProvider.GetRequiredService<ICurrentPrincipalAccessor>();
                using var changePrincipal = principalAccessor.Change(context.User);

                var service = serviceProvider.GetRequiredService<IAuthorAppService>();
                return await service.GetListAsync(context.GetArgument<PagedAndSortedResultRequestDto>("input"));
            }
        );

        // An extra field.
        Field<StringGraphType>("ping", resolve: _ => "pong");
    }
}