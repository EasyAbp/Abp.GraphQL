using System;
using System.Collections.Generic;

namespace EasyAbp.Abp.GraphQL.Dtos;

[Serializable]
public class GraphQLExecutionOutput : Dictionary<string, object>
{
    public GraphQLExecutionOutput()
    {
        
    }
    
    public GraphQLExecutionOutput(IDictionary<string, object> dictionary) : base(dictionary)
    {
    }
}