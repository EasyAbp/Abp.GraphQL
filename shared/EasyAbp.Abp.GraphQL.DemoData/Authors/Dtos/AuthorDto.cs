using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.Abp.GraphQL.Authors.Dtos;

public class AuthorDto : EntityDto<int>, IEntity<int>
{
    public string Name { get; set; }

    public AuthorDto()
    {
            
    }

    public AuthorDto(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public object[] GetKeys()
    {
        return new object[] { Id };
    }
}