using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
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
    
    public virtual async Task<string> ExecuteAsync(string operationName, string query, Dictionary<string, object> variables)
    {
        var schema = await _schemaContainer.GetAsync(operationName);
            
        var gInputs = variables.ToInputs();
        var queryToExecute = query;

        var result = await _executer.ExecuteAsync(_ =>
        {
            _.Schema = schema;
            _.Query = queryToExecute;
            _.OperationName = operationName;
            _.Inputs = gInputs;

            _.ComplexityConfiguration = new ComplexityConfiguration { MaxDepth = 15 };

        });

        if (result.Errors?.Count > 0)
        {
            throw result.Errors.First();
        }

        return await _writer.WriteToStringAsync(result);
    }
}