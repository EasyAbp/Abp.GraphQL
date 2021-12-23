using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Abp.GraphQL.Books.Dtos;
using EasyAbp.Abp.GraphQL.Citys.Dtos;
using EasyAbp.Abp.GraphQL.Dtos;
using GraphQL.SystemTextJson;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Json;
using Xunit;

namespace EasyAbp.Abp.GraphQL.Tests;

public class GraphQLAppServiceTests : GraphQLProviderGraphQLDotnetTestBase
{
    [Fact]
    public async Task Should_Get_A_Book_Without_OperationName()
    {
        var graphQlAppService = ServiceProvider.GetRequiredService<IGraphQLAppService>();

        var result = await graphQlAppService.ExecuteAsync(new GraphQLExecutionInput
        {
            Query = @"
            query {
                book(id: ""CA2EBE5D-D0DC-4D63-A77A-46FF520AEC44"") {
                    name
                }
            }"
        }, "Book");
        
        result.ShouldBeCrossPlatJson(@"{
            ""data"": {
                ""book"": {
                    ""name"": ""Book2""
                }
            }
        }");
    }
    
    [Fact]
    public async Task Should_Get_An_Author_With_Integer_Id()
    {
        var graphQlAppService = ServiceProvider.GetRequiredService<IGraphQLAppService>();

        var result = await graphQlAppService.ExecuteAsync(new GraphQLExecutionInput
        {
            OperationName = "Author",
            Query = @"
            query Author($id: Int!) {
                author(id: $id) {
                    name
                }
            }",
            Variables = new Dictionary<string, object>(new List<KeyValuePair<string, object>>
            {
                new("id", 1)
            })
        });
        
        result.ShouldBeCrossPlatJson(@"{
            ""data"": {
                ""author"": {
                    ""name"": ""Author1""
                }
            }
        }");
    }
    
    [Fact]
    public async Task Should_Get_A_City_With_Custom_Type_Id()
    {
        var graphQlAppService = ServiceProvider.GetRequiredService<IGraphQLAppService>();
        var jsonSerializer = ServiceProvider.GetRequiredService<IJsonSerializer>();

        var result = await graphQlAppService.ExecuteAsync(new GraphQLExecutionInput
        {
            OperationName = "City",
            Query = @"
            query City($id: CityKeyInput!) {
                city(id: $id) {
                    name
                }
            }",
            Variables = new Dictionary<string, object>(new List<KeyValuePair<string, object>>
            {
                new("id", jsonSerializer.Serialize(new CityKey("China", "Shenzhen")).ToInputs())
            })
        });
        
        result.ShouldBeCrossPlatJson(@"{
            ""data"": {
                ""city"": {
                    ""name"": ""Shenzhen""
                }
            }
        }");
    }
    
    [Fact]
    public async Task Should_Get_A_Book_With_Author()
    {
        var graphQlAppService = ServiceProvider.GetRequiredService<IGraphQLAppService>();

        var result = await graphQlAppService.ExecuteAsync(new GraphQLExecutionInput
        {
            OperationName = "Book",
            Query = @"
            query Book($id: Guid!) {
                book(id: $id) {
                    name,
                    author {
                        name
                    }
                }
            }",
            Variables = new Dictionary<string, object>(new List<KeyValuePair<string, object>>
            {
                new("id", "CA2EBE5D-D0DC-4D63-A77A-46FF520AEC44")
            })
        });
        
        result.ShouldBeCrossPlatJson(@"{
            ""data"": {
                ""book"": {
                    ""name"": ""Book2"",
                    ""author"": {
                        ""name"": ""Author1""
                    }
                }
            }
        }");
    }
    
    [Fact]
    public async Task Should_Get_A_Book_With_String_Tag_List()
    {
        var graphQlAppService = ServiceProvider.GetRequiredService<IGraphQLAppService>();

        var result = await graphQlAppService.ExecuteAsync(new GraphQLExecutionInput
        {
            OperationName = "Book",
            Query = @"
            query Book($id: Guid!) {
                book(id: $id) {
                    name,
                    tags
                }
            }",
            Variables = new Dictionary<string, object>(new List<KeyValuePair<string, object>>
            {
                new("id", "CA2EBE5D-D0DC-4D63-A77A-46FF520AEC44")
            })
        });
        
        result.ShouldBeCrossPlatJson(@"{
            ""data"": {
                ""book"": {
                    ""name"": ""Book2"",
                    ""tags"": [
                        ""Children"",
                        ""Adult""
                    ]
                }
            }
        }");
    }
    
    [Fact]
    public async Task Should_Get_A_Book_With_Sponsor_List()
    {
        var graphQlAppService = ServiceProvider.GetRequiredService<IGraphQLAppService>();

        var result = await graphQlAppService.ExecuteAsync(new GraphQLExecutionInput
        {
            OperationName = "Book",
            Query = @"
            query Book($id: Guid!) {
                book(id: $id) {
                    name,
                    sponsors {
                        name
                    }
                }
            }",
            Variables = new Dictionary<string, object>(new List<KeyValuePair<string, object>>
            {
                new("id", "CA2EBE5D-D0DC-4D63-A77A-46FF520AEC44")
            })
        });
        
        result.ShouldBeCrossPlatJson(@"{
            ""data"": {
                ""book"": {
                    ""name"": ""Book2"",
                    ""sponsors"": [
                        {
                            ""name"": ""John""
                        },
                        {
                            ""name"": ""Amy""
                        }
                    ]
                }
            }
        }");
    }
    
    [Fact]
    public async Task Should_Get_Book_List_Without_Input()
    {
        var graphQlAppService = ServiceProvider.GetRequiredService<IGraphQLAppService>();
        var jsonSerializer = ServiceProvider.GetRequiredService<IJsonSerializer>();

        var result = await graphQlAppService.ExecuteAsync(new GraphQLExecutionInput
        {
            OperationName = "Book",
            Query = @"
            query Book {
                bookList {
                    totalCount,
                    items {
                        id,
                        name,
                        sponsors {
                            name
                        }
                    }
                }
            }"
        });
        
        result.ShouldBeCrossPlatJson(@"{
            ""data"": {
                ""bookList"": {
                    ""totalCount"": 6,
                    ""items"": [
                        {
                            ""id"": ""f98f7466-9385-4702-b27b-fa88804dd19d"",
                            ""name"": ""Book1"",
                            ""sponsors"": []
                        },
                        {
                            ""id"": ""ca2ebe5d-d0dc-4d63-a77a-46ff520aec44"",
                            ""name"": ""Book2"",
                            ""sponsors"": [
                                {
                                    ""name"": ""John""
                                },
                                {
                                    ""name"": ""Amy""
                                }
                            ]
                        },
                        {
                            ""id"": ""9b0c1169-0da6-4e01-9236-ec603908bae9"",
                            ""name"": ""Book3"",
                            ""sponsors"": []
                        },
                        {
                            ""id"": ""9a06f713-f62a-4e1c-a6ae-ab2beb3adc08"",
                            ""name"": ""Book4"",
                            ""sponsors"": []
                        },
                        {
                            ""id"": ""07c3bb72-1cd3-4e0c-b38a-604865433fc2"",
                            ""name"": ""Book5"",
                            ""sponsors"": []
                        },
                        {
                            ""id"": ""ddc941a7-87e9-4eef-af94-755e8e4494dc"",
                            ""name"": ""book"",
                            ""sponsors"": []
                        }
                    ]
                }
            }
        }");
    }
    
    [Fact]
    public async Task Should_Get_Book_List()
    {
        var graphQlAppService = ServiceProvider.GetRequiredService<IGraphQLAppService>();
        var jsonSerializer = ServiceProvider.GetRequiredService<IJsonSerializer>();

        var result = await graphQlAppService.ExecuteAsync(new GraphQLExecutionInput
        {
            OperationName = "Book",
            Query = @"
            query Book($input: GetBookListInput) {
                bookList(input: $input) {
                    totalCount,
                    items {
                        id,
                        name,
                        sponsors {
                            name
                        }
                    }
                }
            }",
            Variables = new Dictionary<string, object>(new List<KeyValuePair<string, object>>
            {
                new("input", jsonSerializer.Serialize(new GetBookListInput
                {
                    Filter = "Book"
                }).ToInputs())
            })
        });
        
        result.ShouldBeCrossPlatJson(@"{
            ""data"": {
                ""bookList"": {
                    ""totalCount"": 5,
                    ""items"": [
                        {
                            ""id"": ""f98f7466-9385-4702-b27b-fa88804dd19d"",
                            ""name"": ""Book1"",
                            ""sponsors"": []
                        },
                        {
                            ""id"": ""ca2ebe5d-d0dc-4d63-a77a-46ff520aec44"",
                            ""name"": ""Book2"",
                            ""sponsors"": [
                                {
                                    ""name"": ""John""
                                },
                                {
                                    ""name"": ""Amy""
                                }
                            ]
                        },
                        {
                            ""id"": ""9b0c1169-0da6-4e01-9236-ec603908bae9"",
                            ""name"": ""Book3"",
                            ""sponsors"": []
                        },
                        {
                            ""id"": ""9a06f713-f62a-4e1c-a6ae-ab2beb3adc08"",
                            ""name"": ""Book4"",
                            ""sponsors"": []
                        },
                        {
                            ""id"": ""07c3bb72-1cd3-4e0c-b38a-604865433fc2"",
                            ""name"": ""Book5"",
                            ""sponsors"": []
                        }
                    ]
                }
            }
        }");
    }
}
