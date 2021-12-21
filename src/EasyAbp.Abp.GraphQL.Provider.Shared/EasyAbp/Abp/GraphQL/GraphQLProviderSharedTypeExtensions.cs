using System;
using System.Linq;

namespace EasyAbp.Abp.GraphQL;

public static class GraphQLProviderSharedTypeExtensions
{
    public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
    {
        var interfaceTypes = givenType.GetInterfaces();

        if (interfaceTypes.Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == genericType))
        {
            return true;
        }

        if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
        {
            return true;
        }

        var baseType = givenType.BaseType;

        return baseType != null && IsAssignableToGenericType(baseType, genericType);
    }
}