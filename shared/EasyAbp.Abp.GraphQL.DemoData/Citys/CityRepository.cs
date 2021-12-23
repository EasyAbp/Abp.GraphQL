using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.Abp.GraphQL.Citys.Dtos;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;

namespace EasyAbp.Abp.GraphQL.Citys;

[ExposeServices(typeof(CityRepository), typeof(IRepository<CityDto>))]
public class CityRepository : IRepository<CityDto>, ITransientDependency
{
    private List<CityDto> DataList { get; }

    public CityRepository()
    {
        var cityShenzhen = new CityDto(new CityKey("China", "Shenzhen"));

        cityShenzhen.SetProperty("hello", "world");
        cityShenzhen.SetProperty("friendIds", new List<int> { 1, 2, 3 });
        cityShenzhen.AreaNumberNameMapping = new Dictionary<int, string>
        {
            { 1, "Nanshan" },
            { 2, "Futian" }
        };
        
        DataList = new List<CityDto>
        {
            cityShenzhen,
            new(new CityKey("Turkey", "Istanbul")),
            new(new CityKey("U.S.A", "New York")),
        };
    }

    public Task<List<CityDto>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(DataList);
    }

    public Task<long> GetCountAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult((long)DataList.Count);
    }

    public Task<List<CityDto>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, bool includeDetails = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(DataList.Skip(skipCount).Take(maxResultCount).ToList());
    }

    public IQueryable<CityDto> WithDetails()
    {
        throw new NotImplementedException();
    }

    public IQueryable<CityDto> WithDetails(params Expression<Func<CityDto, object>>[] propertySelectors)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<CityDto>> WithDetailsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<CityDto>> WithDetailsAsync(params Expression<Func<CityDto, object>>[] propertySelectors)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<CityDto>> GetQueryableAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<CityDto>> GetListAsync(Expression<Func<CityDto, bool>> predicate, bool includeDetails = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(DataList.Where(predicate.Compile()).ToList());
    }

    public IAsyncQueryableExecuter AsyncExecuter { get; }

    public Task<CityDto> GetAsync(CityKey id, bool includeDetails = true, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(DataList.Single(x => x.CountryName == id.CountryName && x.Name == id.Name));
    }

    public Task<CityDto> FindAsync(CityKey id, bool includeDetails = true, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(DataList.Find(x => x.CountryName == id.CountryName && x.Name == id.Name));
    }

    public Task<CityDto> InsertAsync(CityDto entity, bool autoSave = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task InsertManyAsync(IEnumerable<CityDto> entities, bool autoSave = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<CityDto> UpdateAsync(CityDto entity, bool autoSave = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task UpdateManyAsync(IEnumerable<CityDto> entities, bool autoSave = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(CityDto entity, bool autoSave = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task DeleteManyAsync(IEnumerable<CityDto> entities, bool autoSave = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<CityDto> FindAsync(Expression<Func<CityDto, bool>> predicate, bool includeDetails = true,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<CityDto> GetAsync(Expression<Func<CityDto, bool>> predicate, bool includeDetails = true,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Expression<Func<CityDto, bool>> predicate, bool autoSave = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }
}