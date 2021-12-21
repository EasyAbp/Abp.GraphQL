using System;
using EasyAbp.Abp.GraphQL.Books.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.GraphQL.Books;

public interface IBookAppService : IReadOnlyAppService<BookDto, BookDto, Guid, GetBookListInput>
{
}