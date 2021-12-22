using System.Threading.Tasks;
using EasyAbp.Abp.GraphQL.Dtos;
using JetBrains.Annotations;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.GraphQL;

public interface IGraphQLAppService : IApplicationService
{
    Task<GraphQLExecutionOutput> ExecuteAsync(GraphQLExecutionInput input,
        [CanBeNull] string defaultSchemaName = null);
}