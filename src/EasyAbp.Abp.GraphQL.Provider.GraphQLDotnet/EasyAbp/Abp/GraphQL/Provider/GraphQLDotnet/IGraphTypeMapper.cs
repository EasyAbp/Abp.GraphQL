using System;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;

public interface IGraphTypeMapper
{
    Type GetGraphType(Type clrType);
}