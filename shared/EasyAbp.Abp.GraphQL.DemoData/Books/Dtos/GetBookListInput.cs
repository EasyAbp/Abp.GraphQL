using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.Abp.GraphQL.Books.Dtos;

[Serializable]
public class GetBookListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}