using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.GraphQL.Authors.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.Abp.GraphQL.Authors;

public class AuthorAppService : CrudAppService<AuthorDto, AuthorDto, int, PagedAndSortedResultRequestDto>, IAuthorAppService
{
    public AuthorAppService(IRepository<AuthorDto, int> repository) : base(repository)
    {
    }

    public override async Task<PagedResultDto<AuthorDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        IEnumerable<AuthorDto> query = (await Repository.GetListAsync());

        var count = query.Count();
        var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            
        return new PagedResultDto<AuthorDto>(count, items);
    }
}