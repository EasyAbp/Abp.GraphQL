using System;
using EasyAbp.Abp.GraphQL.Authors.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.GraphQL.Authors;

public interface IAuthorAppService : IReadOnlyAppService<AuthorDto, AuthorDto, int, PagedAndSortedResultRequestDto>
{
}