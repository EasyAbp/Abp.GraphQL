using System;
using GraphQL;
using GraphQL.Types;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.GraphTypes;

public class PagedResultDtoType<T> : ObjectGraphType<PagedResultDto<T>>
{
    public PagedResultDtoType()
    {
        Name = MakeName();
        
        Field(x => x.TotalCount);
        Field(x => x.Items);
    }
    
    private static string MakeName()
    {
        var genericTypeName = GraphTypeMapper.BuiltInScalarMappings.ContainsKey(typeof(T))
            ? ((IGraphType)Activator.CreateInstance(GraphTypeMapper.BuiltInScalarMappings[typeof(T)])).Name
            : typeof(T).GetNamedType().Name;
        
        return $"PagedResultDto_{genericTypeName}";
    }
}