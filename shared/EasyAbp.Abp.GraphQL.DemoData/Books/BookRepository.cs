using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.Abp.GraphQL.Authors.Dtos;
using EasyAbp.Abp.GraphQL.Books.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;

namespace EasyAbp.Abp.GraphQL.Books;

[ExposeServices(typeof(IReadOnlyRepository<BookDto, Guid>))]
public class BookRepository : IReadOnlyRepository<BookDto, Guid>, ITransientDependency
{
    public bool? IsChangeTrackingEnabled { get; } = null;

    private List<BookDto> DataList { get; }

    public BookRepository()
    {
        DataList = new List<BookDto>
        {
            new(Guid.Parse("F98F7466-9385-4702-B27B-FA88804DD19D"), "Book1", new List<string>(),
                new AuthorDto(1, "Author1"), new List<Sponsor>()),
            new(Guid.Parse("CA2EBE5D-D0DC-4D63-A77A-46FF520AEC44"), "Book2", new List<string>{ "Children", "Adult" },
                new AuthorDto(1, "Author1"), new List<Sponsor>{ new("John"), new("Amy")}),
            new(Guid.Parse("9B0C1169-0DA6-4E01-9236-EC603908BAE9"), "Book3", new List<string>(),
                new AuthorDto(2, "Author2"), new List<Sponsor>()),
            new(Guid.Parse("9A06F713-F62A-4E1C-A6AE-AB2BEB3ADC08"), "Book4", new List<string>(),
                new AuthorDto(2, "Author2"), new List<Sponsor>()),
            new(Guid.Parse("07C3BB72-1CD3-4E0C-B38A-604865433FC2"), "Book5", new List<string>(),
                new AuthorDto(3, "Author3"), new List<Sponsor>()),
            new(Guid.Parse("DDC941A7-87E9-4EEF-AF94-755E8E4494DC"), "book", new List<string>(),
                new AuthorDto(3, "Author3"), new List<Sponsor>()),
        };
    }

    public Task<List<BookDto>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = new())
    {
        return Task.FromResult(DataList);
    }

    public Task<long> GetCountAsync(CancellationToken cancellationToken = new())
    {
        return Task.FromResult((long)DataList.Count);
    }

    public Task<List<BookDto>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, bool includeDetails = false,
        CancellationToken cancellationToken = new())
    {
        return Task.FromResult(DataList.Skip(skipCount).Take(maxResultCount).ToList());
    }

    public IQueryable<BookDto> WithDetails()
    {
        throw new NotImplementedException();
    }

    public IQueryable<BookDto> WithDetails(params Expression<Func<BookDto, object>>[] propertySelectors)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<BookDto>> WithDetailsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<BookDto>> WithDetailsAsync(params Expression<Func<BookDto, object>>[] propertySelectors)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<BookDto>> GetQueryableAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<BookDto>> GetListAsync(Expression<Func<BookDto, bool>> predicate, bool includeDetails = false,
        CancellationToken cancellationToken = new())
    {
        return Task.FromResult(DataList.Where(predicate.Compile()).ToList());
    }

    public IAsyncQueryableExecuter AsyncExecuter { get; }
    public Task<BookDto> GetAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = new())
    {
        return Task.FromResult(DataList.Single(x => x.Id == id));
    }

    public Task<BookDto> FindAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = new())
    {
        return Task.FromResult(DataList.Find(x => x.Id == id));
    }
}