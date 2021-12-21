using System;
using System.Collections.Generic;

namespace EasyAbp.Abp.GraphQL.Dtos;

[Serializable]
public class GraphQLExecutionInput
{
    public string OperationName { get; set; }
        
    public string Query { get; set; }
        
    public Dictionary<string, object> Variables { get; set; }
}