using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;

namespace EasyAbp.Abp.GraphQL.Books.Dtos;

[Serializable]
public class GetBookListInput : PagedAndSortedResultRequestDto, IHasExtraProperties
{
    [CanBeNull]
    public string Filter { get; set; }

    public long? Pages { get; set; }

    [JsonInclude]
    public ExtraPropertyDictionary ExtraProperties { get; set; }

    [JsonInclude]
    public ExtraPropertyDictionary ExtraProperties2 { get; set; }

    public Dictionary<string, object> ExtraProperties3 { get; set; }
}