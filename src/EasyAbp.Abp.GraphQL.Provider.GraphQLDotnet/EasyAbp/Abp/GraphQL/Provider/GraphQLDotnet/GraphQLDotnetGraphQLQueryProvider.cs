using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.SystemTextJson;
using GraphQL.Validation.Complexity;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;

public class GraphQLDotnetGraphQLQueryProvider : IGraphQLQueryProvider, ITransientDependency
{
    private readonly IDocumentWriter _writer;
    private readonly IDocumentExecuter _executer;
    private readonly ISchemaContainer _schemaContainer;

    public GraphQLDotnetGraphQLQueryProvider(
        IDocumentWriter writer,
        IDocumentExecuter executer,
        ISchemaContainer schemaContainer)
    {
        _writer = writer;
        _executer = executer;
        _schemaContainer = schemaContainer;
    }
    
    public virtual async Task<Dictionary<string, object>> ExecuteAsync(string operationName, string query, Dictionary<string, object> variables)
    {
        var schema = await _schemaContainer.GetOrDefaultAsync(operationName);
        
        variables ??= new Dictionary<string, object>();
        
        foreach (var pair in variables)
        {
            if (pair.Value is JsonElement)
            {
                variables[pair.Key] = pair.Value.ToString().ToInputs();
            }
        }
        
        var gInputs = new Inputs(variables);

        var queryToExecute = query;

        var result = await _executer.ExecuteAsync(_ =>
        {
            _.Schema = schema;
            _.Query = queryToExecute;
            _.OperationName = operationName;
            _.Inputs = gInputs;
#if DEBUG
            _.ThrowOnUnhandledException = true;
#endif
            _.ComplexityConfiguration = new ComplexityConfiguration { MaxDepth = 15 };

        });

        using var memoryStream = new MemoryStream();

        await _writer.WriteAsync(memoryStream, result);

        memoryStream.Seek(0, SeekOrigin.Begin);

        return await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(memoryStream);
    }
}