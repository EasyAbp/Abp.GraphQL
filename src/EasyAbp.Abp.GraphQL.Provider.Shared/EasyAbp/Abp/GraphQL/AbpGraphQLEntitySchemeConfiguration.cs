using System;

namespace EasyAbp.Abp.GraphQL;

public class AbpGraphQLEntitySchemeConfiguration : AbpGraphQLEntitySchemeConfigurationBase
{
    public string SchemaName { get; set; }
    
    /// <summary>
    /// Should implement `IReadOnlyAppService`
    /// </summary>
    public Type AppServiceInterfaceType { get; set; }
    
    public Type GetOutputDtoType { get; set; }
    
    public Type GetListOutputDtoType { get; set; }
    
    public Type KeyType { get; set; }
    
    public Type GetListInputType { get; set; }
}