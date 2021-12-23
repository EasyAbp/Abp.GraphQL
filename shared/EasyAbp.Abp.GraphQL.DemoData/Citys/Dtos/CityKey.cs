using System;

namespace EasyAbp.Abp.GraphQL.Citys.Dtos;

[Serializable]
public class CityKey
{
    public string CountryName { get; set; }
    
    public string Name { get; set; }

    public CityKey()
    {
        
    }

    public CityKey(string countryName, string name)
    {
        CountryName = countryName;
        Name = name;
    }
}