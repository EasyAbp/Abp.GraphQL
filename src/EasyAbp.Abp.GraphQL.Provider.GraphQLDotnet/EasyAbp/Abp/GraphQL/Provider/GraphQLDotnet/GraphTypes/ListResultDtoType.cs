using GraphQL.Types;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.GraphTypes;

public class ListResultDtoType<T> : ObjectGraphType<ListResultDto<T>>
{
    public ListResultDtoType()
    {
        Field(x => x.Items);
    }
}