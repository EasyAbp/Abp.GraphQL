using System.Threading.Tasks;
using EasyAbp.Abp.GraphQL.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.GraphQL;

public interface IGraphQLAppService : IApplicationService
{
    Task<GraphQLExecutionOutput> ExecuteAsync(GraphQLExecutionInput input);
}