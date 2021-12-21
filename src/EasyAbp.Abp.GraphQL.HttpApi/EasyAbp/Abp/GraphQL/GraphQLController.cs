using System.Threading.Tasks;
using EasyAbp.Abp.GraphQL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.Abp.GraphQL;

[RemoteService(Name = GraphQLRemoteServiceConsts.RemoteServiceName)]
[Route("/api/graphql")]
public class GraphQLController : GraphQLControllerBase, IGraphQLAppService
{
    private readonly IGraphQLAppService _service;

    public GraphQLController(IGraphQLAppService service)
    {
        _service = service;
    }
    
    [HttpPost]
    [Route("")]
    public virtual Task<string> ExecuteAsync(GraphQLExecutionInput input)
    {
        return _service.ExecuteAsync(input);
    }
}