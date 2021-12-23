﻿using System;
using GraphQL.Server.Ui.Altair;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.GraphQL.Web
{
    [DependsOn(
        typeof(AbpAspNetCoreModule),
        typeof(AbpGraphQLProviderSharedModule)
    )]
    public class AbpGraphQLWebAltairModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpEndpointRouterOptions>(options =>
            {
                options.EndpointConfigureActions.Add(builderContext =>
                {
                    var uiOptions =
                        builderContext.ScopeServiceProvider.GetRequiredService<IOptions<AbpAltairOptions>>().Value;

                    var schemeConfigurations = builderContext.ScopeServiceProvider
                        .GetRequiredService<IOptions<AbpGraphQLOptions>>().Value.AppServiceSchemes;

                    builderContext.Endpoints.MapGraphQLAltair(uiOptions,
                        uiOptions.UiBasicPath.RemovePostFix("/").RemovePostFix("/"));
                    
                    foreach (var schema in schemeConfigurations.GetConfigurations())
                    {
                        var schemaUiOption = (AltairOptions)uiOptions.Clone();
                        schemaUiOption.GraphQLEndPoint = schemaUiOption.GraphQLEndPoint.Value.EnsureEndsWith('/') + schema.SchemaName;
                        schemaUiOption.SubscriptionsEndPoint = schemaUiOption.SubscriptionsEndPoint.Value.EnsureEndsWith('/') + schema.SchemaName;

                        builderContext.Endpoints.MapGraphQLAltair(schemaUiOption,
                            uiOptions.UiBasicPath.RemovePreFix("/").EnsureEndsWith('/') + schema.SchemaName);
                    }
                });
            });
        }
    }
}
