using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.Abp.GraphQL.Citys.Dtos;

public class CityDto : ExtensibleEntityDto, IEntity
{
    public string CountryName { get; set; }
    
    public string Name { get; set; }
    
    public Dictionary<int, string> AreaNumberNameMapping { get; set; }

    public CityDto()
    {
            
    }

    public CityDto(CityKey id)
    {
        CountryName = id.CountryName;
        Name = id.Name;
    }

    public object[] GetKeys()
    {
        return new object[] { CountryName, Name };
    }
}