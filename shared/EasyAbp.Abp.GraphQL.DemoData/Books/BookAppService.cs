using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.GraphQL.Books.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.Abp.GraphQL.Books;

public class BookAppService : ReadOnlyAppService<BookDto, BookDto, Guid, GetBookListInput>, IBookAppService
{
    public BookAppService(IReadOnlyRepository<BookDto, Guid> repository) : base(repository)
    {
    }

    public override async Task<PagedResultDto<BookDto>> GetListAsync(GetBookListInput input)
    {
        var query = (await Repository.GetListAsync())
            .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Filter))
            .WhereIf(input.Pages is not null, x => x.Pages == input.Pages);

        var count = query.Count();
        var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

        return new PagedResultDto<BookDto>(count, items);
    }
}