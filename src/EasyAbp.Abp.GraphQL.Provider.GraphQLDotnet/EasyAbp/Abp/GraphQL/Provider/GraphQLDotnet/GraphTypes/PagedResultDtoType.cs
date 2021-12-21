using GraphQL.Types;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.GraphTypes;

public class PagedResultDtoType<T> : ObjectGraphType<PagedResultDto<T>>
{
    public PagedResultDtoType()
    {
        Field(x => x.TotalCount);
        Field(x => x.Items);
    }
}