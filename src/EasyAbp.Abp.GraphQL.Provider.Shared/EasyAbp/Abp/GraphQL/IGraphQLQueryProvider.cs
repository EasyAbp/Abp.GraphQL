using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.Abp.GraphQL;

public interface IGraphQLQueryProvider
{
    /// <summary>
    /// Execute the GraphQL query.
    /// </summary>
    /// <param name="operationName">Operation name to execute. For example: Book</param>
    /// <param name="query">The query string. For example: query Book { book { name } }</param>
    /// <param name="variables">Variables dictionary. For example: new({ {"filter", "Adult"} })</param>
    /// <returns>A dictionary result.</returns>
    Task<Dictionary<string, object>> ExecuteAsync(string operationName, string query, Dictionary<string, object> variables);
}