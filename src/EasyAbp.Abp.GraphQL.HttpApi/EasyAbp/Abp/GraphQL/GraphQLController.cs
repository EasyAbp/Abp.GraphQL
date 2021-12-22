using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Abp.GraphQL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.Abp.GraphQL;

[AllowAnonymous]
[RemoteService(Name = GraphQLRemoteServiceConsts.RemoteServiceName)]
[Route("/graphql")]
public class GraphQLController : GraphQLControllerBase
{
    private readonly IGraphQLAppService _service;

    public GraphQLController(IGraphQLAppService service)
    {
        _service = service;
    }
    
    [HttpPost]
    [Route("")]
    public virtual async Task<GraphQLExecutionOutput> ExecuteAsync(GraphQLExecutionInput input)
    {
        return await _service.ExecuteAsync(input);
    }
}