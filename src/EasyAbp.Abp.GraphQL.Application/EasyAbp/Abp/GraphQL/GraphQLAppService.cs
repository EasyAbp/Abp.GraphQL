using System.Threading.Tasks;
using EasyAbp.Abp.GraphQL.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace EasyAbp.Abp.GraphQL;

[AllowAnonymous]
public class GraphQLAppService : GraphQLAppServiceBase, IGraphQLAppService
{
    private readonly IGraphQLQueryProvider _queryProvider;

    public GraphQLAppService(IGraphQLQueryProvider queryProvider)
    {
        _queryProvider = queryProvider;
    }
        
    public virtual async Task<GraphQLExecutionOutput> ExecuteAsync(GraphQLExecutionInput input)
    {
        return new GraphQLExecutionOutput(
            await _queryProvider.ExecuteAsync(input.OperationName, input.Query, input.Variables));
    }
}