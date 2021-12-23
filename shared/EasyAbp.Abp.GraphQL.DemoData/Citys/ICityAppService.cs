using EasyAbp.Abp.GraphQL.Citys.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.GraphQL.Citys;

public interface ICityAppService : IReadOnlyAppService<CityDto, CityDto, CityKey, PagedAndSortedResultRequestDto>
{
}