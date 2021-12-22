using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;

public class SchemaContainer : ISchemaContainer, ISingletonDependency
{
    protected Dictionary<string, ISchema> Schemas { get; set; }

    public SchemaContainer(
        IOptions<AbpGraphQLOptions> options,
        IServiceProvider serviceProvider,
        IEnumerable<ISchema> schemas)
    {
        Schemas = schemas.ToDictionary(
            keySelector: schema => schema.GetType().Name.RemovePostFix("Schema"),
            elementSelector: schema => schema);

        foreach (var configuration in options.Value.AppServiceSchemes.GetConfigurations())
        {
            var schemaType = typeof(AppServiceSchema<,,,,>).MakeGenericType(configuration.AppServiceInterfaceType,
                configuration.GetOutputDtoType, configuration.GetListOutputDtoType, configuration.KeyType,
                configuration.GetListInputType);
            
            var schema = Activator.CreateInstance(schemaType, serviceProvider);

            Schemas.Add(configuration.SchemaName, (ISchema)schema);
        }

        var introspectionSchema = new Schema(serviceProvider);
        introspectionSchema.Query = new EmptyQuery();
        Schemas.Add("IntrospectionQuery", introspectionSchema);
    }

    public virtual Task<ISchema> GetOrDefaultAsync(string schemaName)
    {
        return Task.FromResult(schemaName != null && Schemas.ContainsKey(schemaName)
            ? Schemas[schemaName]
            : Schemas["IntrospectionQuery"]);
    }
}