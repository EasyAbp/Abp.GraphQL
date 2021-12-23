using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Abp.GraphQL.Authors;
using EasyAbp.Abp.GraphQL.Authors.Dtos;
using EasyAbp.Abp.GraphQL.Books;
using EasyAbp.Abp.GraphQL.Books.Dtos;
using EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;
using GraphQL.SystemTextJson;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Json;
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

        var authorSchema = await schemaContainer.GetOrDefaultAsync("HelloWorld");
        
        authorSchema.Query.Name.ShouldBe("HelloWorldQuery");
        authorSchema.Query.HasField("ping").ShouldBeTrue();

        (await queryProvider.ExecuteAsync("HelloWorld", query, new Dictionary<string, object>()))
            .ShouldBeCrossPlatJson(result);
    }
    
    [Fact]
    public async Task Should_Replace_AppServiceQuery_For_Author()
    {
        var schemaContainer = ServiceProvider.GetRequiredService<ISchemaContainer>();

        var authorSchema = await schemaContainer.GetOrDefaultAsync("Author");

        authorSchema.Query.GetType().ShouldBe(typeof(AuthorAppServiceQuery));
        authorSchema.Query.GetType().ShouldNotBe(typeof(AppServiceQuery<IAuthorAppService, AuthorDto, AuthorDto, int, PagedAndSortedResultRequestDto>));
        authorSchema.Query.HasField("ping").ShouldBeTrue();
        
        var bookSchema = await schemaContainer.GetOrDefaultAsync("Book");

        bookSchema.Query.GetType().ShouldBe(typeof(AppServiceQuery<IBookAppService, BookDto, BookDto, Guid, GetBookListInput>));
        authorSchema.Query.HasField("ping").ShouldBeTrue();
    }
    
    [Fact]
    public async Task Should_Get_Introspection_Query_Result()
    {
        var queryProvider = ServiceProvider.GetRequiredService<GraphQLDotnetGraphQLQueryProvider>();
        var jsonSerializer = ServiceProvider.GetRequiredService<IJsonSerializer>();

        var query = @"
        query IntrospectionQuery {
            __schema {
                queryType {
                    name
                }
                mutationType {
                    name
                }
                subscriptionType {
                    name
                }
                types {
                    ...FullType
                }
                directives {
                    name
                    description
                    locations
                    args {
                        ...InputValue
                    }
                }
            }
        }
        fragment FullType on __Type {
            kind
            name
            description
            fields(includeDeprecated: true) {
                name
                description
                args {
                    ...InputValue
                }
                type {
                    ...TypeRef
                }
                isDeprecated
                deprecationReason
            }
            inputFields {
                ...InputValue
            }
            interfaces {
                ...TypeRef
            }
            enumValues(includeDeprecated: true) {
                name
                description
                isDeprecated
                deprecationReason
            }
            possibleTypes {
                ...TypeRef
            }
        }
        fragment InputValue on __InputValue {
            name
            description
            type {
                ...TypeRef
            }
            defaultValue
        }
        fragment TypeRef on __Type {
            kind
            name
            ofType {
                kind
                name
                ofType {
                    kind
                    name
                    ofType {
                        kind
                        name
                        ofType {
                            kind
                            name
                            ofType {
                                kind
                                name
                                ofType {
                                    kind
                                    name
                                    ofType {
                                        kind
                                        name
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }";

        var result = await queryProvider.ExecuteAsync("IntrospectionQuery", query, new Dictionary<string, object>());

        var resultToInputs = jsonSerializer.Serialize(result).ToInputs();
        
        resultToInputs.ShouldContainKey("data");
        resultToInputs["data"].ShouldNotBeNull();
        resultToInputs.ShouldNotContainKey("errors");
    }
}
