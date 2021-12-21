using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.Abp.GraphQL;

public interface IGraphQLQueryProvider
{
    Task<string> ExecuteAsync(string operationName, string query, Dictionary<string, object> variables);
}