using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.GraphQL;

public class AbpGraphQLAppServiceSchemeConfigurations
{
    /// <summary>
    /// Scheme name to configuration mapping.
    /// </summary>
    private readonly Dictionary<string, AbpGraphQLEntitySchemeConfiguration> _scheme;

    public AbpGraphQLAppServiceSchemeConfigurations()
    {
        _scheme = new Dictionary<string, AbpGraphQLEntitySchemeConfiguration>();
    }

    /// <summary>
    /// Configure by an Application.Contracts assembly.
    /// </summary>
    /// <param name="schemeNamePrefix">For example set `Identity` for the identity module</param>
    /// <param name="contractsAssembly">An Application.Contracts assembly</param>
    /// <param name="configureAction"></param>
    /// <returns></returns>
    public AbpGraphQLAppServiceSchemeConfigurations Configure(
        [NotNull] Assembly contractsAssembly,
        [CanBeNull] string schemeNamePrefix = null,
        [CanBeNull] Action<AbpGraphQLEntitySchemeConfigurationBase> configureAction = null)
    {
        Check.NotNull(contractsAssembly, nameof(contractsAssembly));

        var appServiceTypes = contractsAssembly.DefinedTypes.Where(x =>
            x.IsInterface && x.IsAssignableToGenericType(typeof(IReadOnlyAppService<,,,>)));

        foreach (var appServiceType in appServiceTypes)
        {
            var readOnlyAppServiceType = appServiceType.GetInterfaces()
                .FirstOrDefault(x => x.IsAssignableToGenericType(typeof(IReadOnlyAppService<,,,>)));

            if (readOnlyAppServiceType == null)
            {
                continue;
            }
            
            var args = readOnlyAppServiceType.GetGenericArguments();

            if (args.Length != 4)
            {
                continue;
            }
            
            var getOutputDtoType = args[0];
            var getListOutputDtoType = args[1];
            var keyType = args[2];
            var getListInputType = args[3];
            
            var schemeName = schemeNamePrefix + getOutputDtoType.Name.RemovePostFix("Dto");

            configureAction ??= _ => { };

            configureAction(
                _scheme.GetOrAdd(
                    schemeName,
                    () => new AbpGraphQLEntitySchemeConfiguration
                    {
                        SchemaName = schemeName,
                        AppServiceInterfaceType = appServiceType,
                        GetOutputDtoType = getOutputDtoType,
                        GetListOutputDtoType = getListOutputDtoType,
                        KeyType = keyType,
                        GetListInputType = getListInputType
                    }
                )
            );
        }

        return this;
    }

    public AbpGraphQLAppServiceSchemeConfigurations Configure(
        [NotNull] string name,
        [NotNull] Action<AbpGraphQLEntitySchemeConfiguration> configureAction)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNull(configureAction, nameof(configureAction));

        configureAction(
            _scheme.GetOrAdd(
                name,
                () => new AbpGraphQLEntitySchemeConfiguration()
            )
        );

        return this;
    }

    [NotNull]
    public AbpGraphQLEntitySchemeConfiguration GetConfiguration([NotNull] string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        return _scheme[name];
    }

    [NotNull]
    public List<AbpGraphQLEntitySchemeConfiguration> GetConfigurations()
    {
        return _scheme.Values.ToList();
    }
}