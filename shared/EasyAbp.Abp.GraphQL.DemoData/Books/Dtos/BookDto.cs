using System;
using System.Collections.Generic;
using EasyAbp.Abp.GraphQL.Authors.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.Abp.GraphQL.Books.Dtos;

public class BookDto : EntityDto<Guid>, IEntity<Guid>
{
    public string Name { get; set; }
        
    public List<string> Tags { get; set; }

    public AuthorDto Author { get; set; }
        
    public List<Sponsor> Sponsors { get; set; }

    public BookDto()
    {
            
    }

    public BookDto(Guid id, string name, List<string> tags, AuthorDto author, List<Sponsor> sponsors)
    {
        Id = id;
        Name = name;
        Tags = tags;
        Author = author;
        Sponsors = sponsors;
    }

    public object[] GetKeys()
    {
        return new object[] { Id };
    }
}