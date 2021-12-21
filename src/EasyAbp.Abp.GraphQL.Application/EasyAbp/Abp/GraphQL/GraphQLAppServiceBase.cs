using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.GraphQL;

public abstract class GraphQLAppServiceBase : ApplicationService
{
    protected GraphQLAppServiceBase()
    {
        ObjectMapperContext = typeof(AbpGraphQLApplicationModule);
    }
}