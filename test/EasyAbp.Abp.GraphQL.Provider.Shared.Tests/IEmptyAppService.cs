using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.GraphQL;

internal interface IEmptyAppService2 : IReadOnlyAppService<EmptyDto, Guid>;
internal interface IEmptyAppService3 : IReadOnlyAppService<EmptyDto, Guid, PagedAndSortedResultRequestDto>;
internal interface IEmptyAppService4 : IReadOnlyAppService<EmptyDto, EmptyDto, Guid, PagedAndSortedResultRequestDto>;
