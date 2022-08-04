using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using GraphQL;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;

public class GraphQLDotnetGraphQLQueryProvider : IGraphQLQueryProvider, ITransientDependency
{
    private readonly IGraphQLTextSerializer _serializer;
    private readonly IDocumentExecuter _executer;
    private readonly ISchemaContainer _schemaContainer;

    public GraphQLDotnetGraphQLQueryProvider(
        IGraphQLTextSerializer serializer,
        IDocumentExecuter executer,
        ISchemaContainer schemaContainer)
    {
        _serializer = serializer;
        _executer = executer;
        _schemaContainer = schemaContainer;
    }

    public virtual async Task<Dictionary<string, object>> ExecuteAsync(string operationName, string query,
        Dictionary<string, object> variables, string defaultSchemaName = null)
    {
        var schema = await _schemaContainer.GetOrDefaultAsync(operationName, defaultSchemaName);

        variables ??= new Dictionary<string, object>();

        foreach (var pair in variables)
        {
            if (pair.Value is JsonElement)
            {
                variables[pair.Key] = _serializer.Deserialize<Inputs>(pair.Value.ToString()) ?? Inputs.Empty;
            }
        }

        var gInputs = new Inputs(variables);

        var queryToExecute = query;

        var result = await _executer.ExecuteAsync(_ =>
        {
            _.Schema = schema;
            _.Query = queryToExecute;
            _.OperationName = operationName;
            _.Variables = gInputs;
#if DEBUG
            _.ThrowOnUnhandledException = true;
#endif
        });

        using var memoryStream = new MemoryStream();

        await _serializer.WriteAsync(memoryStream, result);

        memoryStream.Seek(0, SeekOrigin.Begin);

        return await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(memoryStream);
    }
}