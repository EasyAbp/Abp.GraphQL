using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.GraphQL.Citys.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.GraphQL.Citys;

public class CityAppService : AbstractKeyCrudAppService<CityDto, CityDto, CityKey, PagedAndSortedResultRequestDto>, ICityAppService
{
    private readonly CityRepository _repository;

    public CityAppService(CityRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public override async Task<PagedResultDto<CityDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        IEnumerable<CityDto> query = (await Repository.GetListAsync());

        var count = query.Count();
        var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            
        return new PagedResultDto<CityDto>(count, items);
    }

    protected override Task<CityDto> GetEntityByIdAsync(CityKey id)
    {
        return _repository.GetAsync(id);
    }

    protected override Task DeleteByIdAsync(CityKey id)
    {
        throw new System.NotImplementedException();
    }
}