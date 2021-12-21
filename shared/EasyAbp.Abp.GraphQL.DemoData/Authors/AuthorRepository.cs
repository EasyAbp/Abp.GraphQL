using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.Abp.GraphQL.Authors.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;

namespace EasyAbp.Abp.GraphQL.Authors;

[ExposeServices(typeof(IRepository<AuthorDto, int>))]
public class AuthorRepository : IRepository<AuthorDto, int>, ITransientDependency
{
    private List<AuthorDto> DataList { get; }

    public AuthorRepository()
    {
        DataList = new List<AuthorDto>
        {
            new(1, "Author1"),
            new(2, "Author2"),
            new(3, "Author3"),
        };
    }

    public Task<List<AuthorDto>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(DataList);
    }

    public Task<long> GetCountAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult((long)DataList.Count);
    }

    public Task<List<AuthorDto>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, bool includeDetails = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(DataList.Skip(skipCount).Take(maxResultCount).ToList());
    }

    public IQueryable<AuthorDto> WithDetails()
    {
        throw new NotImplementedException();
    }

    public IQueryable<AuthorDto> WithDetails(params Expression<Func<AuthorDto, object>>[] propertySelectors)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<AuthorDto>> WithDetailsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<AuthorDto>> WithDetailsAsync(params Expression<Func<AuthorDto, object>>[] propertySelectors)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<AuthorDto>> GetQueryableAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<AuthorDto>> GetListAsync(Expression<Func<AuthorDto, bool>> predicate, bool includeDetails = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(DataList.Where(predicate.Compile()).ToList());
    }

    public IAsyncQueryableExecuter AsyncExecuter { get; }
    public Task<AuthorDto> GetAsync(int id, bool includeDetails = true, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(DataList.Single(x => x.Id == id));
    }

    public Task<AuthorDto> FindAsync(int id, bool includeDetails = true, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(DataList.Find(x => x.Id == id));
    }

    public Task<AuthorDto> InsertAsync(AuthorDto entity, bool autoSave = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task InsertManyAsync(IEnumerable<AuthorDto> entities, bool autoSave = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<AuthorDto> UpdateAsync(AuthorDto entity, bool autoSave = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task UpdateManyAsync(IEnumerable<AuthorDto> entities, bool autoSave = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(AuthorDto entity, bool autoSave = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task DeleteManyAsync(IEnumerable<AuthorDto> entities, bool autoSave = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<AuthorDto> FindAsync(Expression<Func<AuthorDto, bool>> predicate, bool includeDetails = true,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<AuthorDto> GetAsync(Expression<Func<AuthorDto, bool>> predicate, bool includeDetails = true,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Expression<Func<AuthorDto, bool>> predicate, bool autoSave = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id, bool autoSave = false, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task DeleteManyAsync(IEnumerable<int> ids, bool autoSave = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }
}