namespace EasyAbp.Abp.GraphQL;

public class AbpGraphQLOptions
{
    public AbpGraphQLAppServiceSchemeConfigurations AppServiceSchemes { get; }

    public AbpGraphQLOptions()
    {
        AppServiceSchemes = new AbpGraphQLAppServiceSchemeConfigurations();
    }
}