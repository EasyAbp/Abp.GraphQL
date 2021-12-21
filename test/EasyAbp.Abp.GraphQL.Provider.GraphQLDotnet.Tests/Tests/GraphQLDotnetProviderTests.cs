using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Abp.GraphQL.Authors;
using EasyAbp.Abp.GraphQL.Authors.Dtos;
using EasyAbp.Abp.GraphQL.Books;
using EasyAbp.Abp.GraphQL.Books.Dtos;
using EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Xunit;

namespace EasyAbp.Abp.GraphQL.Tests;

public class GraphQLDotnetProviderTests : GraphQLProviderGraphQLDotnetTestBase
{
    [Fact]
    public async Task Should_Support_Custom_Schemes()
    {
        const string query = @"
            query HelloWorld{
              ping
            }
        ";
        const string result = @"
            {
                ""data"": {
                    ""ping"": ""pong""
                }
            }
        ";
        
        var queryProvider = ServiceProvider.GetRequiredService<GraphQLDotnetGraphQLQueryProvider>();
        var schemaContainer = ServiceProvider.GetRequiredService<ISchemaContainer>();

        var authorSchema = await schemaContainer.GetAsync("HelloWorld");
        
        authorSchema.Query.HasField("ping").ShouldBeTrue();

        (await queryProvider.ExecuteAsync("HelloWorld", query, new Dictionary<string, object>()))
            .ShouldBeCrossPlatJson(result);
    }
    
    [Fact]
    public async Task Should_Replace_AppServiceQuery_For_Author()
    {
        var schemaContainer = ServiceProvider.GetRequiredService<ISchemaContainer>();

        var authorSchema = await schemaContainer.GetAsync("Author");

        authorSchema.Query.GetType().ShouldBe(typeof(AuthorAppServiceQuery));
        authorSchema.Query.GetType().ShouldNotBe(typeof(AppServiceQuery<IAuthorAppService, AuthorDto, AuthorDto, int, PagedAndSortedResultRequestDto>));
        authorSchema.Query.HasField("ping").ShouldBeTrue();
        
        var bookSchema = await schemaContainer.GetAsync("Book");

        bookSchema.Query.GetType().ShouldBe(typeof(AppServiceQuery<IBookAppService, BookDto, BookDto, Guid, GetBookListInput>));
        authorSchema.Query.HasField("ping").ShouldBeTrue();
    }
}
