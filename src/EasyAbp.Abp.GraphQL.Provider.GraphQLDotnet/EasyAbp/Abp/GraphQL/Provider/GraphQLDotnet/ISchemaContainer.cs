using System.Threading.Tasks;
using GraphQL.Types;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;

public interface ISchemaContainer
{
    Task<ISchema> GetOrDefaultAsync(string schemaName, string defaultSchemaName = null);
}