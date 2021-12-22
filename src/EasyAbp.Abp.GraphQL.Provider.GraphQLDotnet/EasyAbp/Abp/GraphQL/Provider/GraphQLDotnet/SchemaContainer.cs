using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;

public class SchemaContainer : ISchemaContainer, ISingletonDependency
{
    protected ISchema DefaultSchema { get; set; }
    
    protected Dictionary<string, ISchema> Schemas { get; set; }

    public SchemaContainer(
        IOptions<AbpGraphQLOptions> options,
        IServiceProvider serviceProvider,
        IEnumerable<ISchema> customSchemas)
    {
        DefaultSchema = new Schema();
        DefaultSchema.Query = new EmptyQuery();

        Schemas = customSchemas.ToDictionary(
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
    }

    public virtual Task<ISchema> GetOrDefaultAsync(string schemaName, string specifiedDefaultSchemaName = null)
    {
        return Task.FromResult(!schemaName.IsNullOrEmpty() && Schemas.ContainsKey(schemaName)
            ? Schemas[schemaName]
            : specifiedDefaultSchemaName != null ? Schemas[specifiedDefaultSchemaName] : DefaultSchema);
    }
}