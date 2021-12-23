using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;

namespace EasyAbp.Abp.GraphQL.Books.Dtos;

[Serializable]
public class GetBookListInput : PagedAndSortedResultRequestDto, IHasExtraProperties
{
    public string Filter { get; set; }
    
    public ExtraPropertyDictionary ExtraProperties { get; set; }
    
    public ExtraPropertyDictionary ExtraProperties2 { get; set; }
    
    public Dictionary<string, object> ExtraProperties3 { get; set; }
}