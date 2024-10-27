using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.AstValueNodes;
using GraphQL;
using GraphQL.Types;
using GraphQLParser.AST;
using Volo.Abp.Data;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.GraphTypes;

/// <summary>
/// https://github.com/fenomeno83/graphql-dotnet-auto-types
/// </summary>
public class GraphQLInputGenericType<T> : InputObjectGraphType<T> where T : class
{
    private Dictionary<string, Type> PropertiesAstNodeType { get; } = new();

    public GraphQLInputGenericType()
    {
        var genericType = typeof(T);

        Name = MakeName(typeof(T));

        var propsInfo = genericType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        if (propsInfo == null || propsInfo.Length == 0)
            throw new GraphQLSchemaException(genericType.Name, $"Unable to create generic GraphQL type from type {genericType.Name} because it has no properties");

        var targetType =
            genericType.IsAssignableToGenericType(typeof(NonNullGraphType<>)) ||
            genericType.IsAssignableToGenericType(typeof(ListGraphType<>))
                ? genericType.GetGenericArguments()[0]
                : genericType;
        
        if (GraphTypeMapper.IsBuiltInScalar(targetType))
        {
            return;
        }
        
        targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .ToList()
            .ForEach(pi =>
            {
                EmitField(pi);
                PropertiesAstNodeType[pi.Name] = AstNodeTypeHelper.ToAstValueNodeType(pi.PropertyType, true);
            });
    }

    private static string MakeName(Type type)
    {
        var name = type.GetNamedType().Name;
        
        return name.EndsWith("Input") ? name : $"{name}Input";
    }

    private void EmitField(PropertyInfo propertyInfo)
    {
        var isDictionary = propertyInfo.PropertyType.IsAssignableToGenericType(typeof(IDictionary<,>));
        var typeName = propertyInfo.PropertyType.Name;
        if (isDictionary || propertyInfo.PropertyType.Namespace != null && !propertyInfo.PropertyType.Namespace.StartsWith("System"))
        {
            if (propertyInfo.PropertyType.IsEnum)
                Field(GraphTypeMapper.GetGraphType(propertyInfo.PropertyType, isInput: true), propertyInfo.Name);
            else
            {
                var gqlType = Assembly.GetAssembly(typeof(ISchema)).GetTypes().FirstOrDefault(t => t.Name == $"{typeName}Type" && t.IsAssignableTo<IGraphType>());

                gqlType ??= isDictionary
                    ? propertyInfo.PropertyType.IsAssignableTo<ExtraPropertyDictionary>()
                        ? typeof(AbpExtraPropertyGraphType)
                        : MakeDictionaryType(propertyInfo)
                    : typeof(GraphQLInputGenericType<>).MakeGenericType(propertyInfo.PropertyType);
                
                Field(gqlType, propertyInfo.Name);
            }
        }
        else
        {
            switch (typeName)
            {
                case "List`1":
                {
                    var gtn = propertyInfo.PropertyType.GetGenericArguments().First();
                    var gqlListType = GraphTypeMapper.GetGraphType(gtn, isInput: true);
                    var listType = typeof(ListGraphType<>).MakeGenericType(gqlListType);
                    Field(listType, propertyInfo.Name);
                    break;
                }
                case nameof(Int32): Field(GraphTypeMapper.GetGraphType(propertyInfo.PropertyType, isInput: true), propertyInfo.Name); break;
                case nameof(Int64): Field(GraphTypeMapper.GetGraphType(propertyInfo.PropertyType, isInput: true), propertyInfo.Name); break;
                case nameof(Int16): Field(GraphTypeMapper.GetGraphType(propertyInfo.PropertyType, isInput: true), propertyInfo.Name); break;
                case nameof(Single): Field(GraphTypeMapper.GetGraphType(propertyInfo.PropertyType, isInput: true), propertyInfo.Name); break;
                case nameof(Double): Field(GraphTypeMapper.GetGraphType(propertyInfo.PropertyType, isInput: true), propertyInfo.Name); break;
                case nameof(Decimal): Field(GraphTypeMapper.GetGraphType(propertyInfo.PropertyType, isInput: true), propertyInfo.Name); break;
                case nameof(Boolean): Field(GraphTypeMapper.GetGraphType(propertyInfo.PropertyType, isInput: true), propertyInfo.Name); break;
                case nameof(Byte):
                    Field(GraphTypeMapper.GetGraphType(propertyInfo.PropertyType, isInput: true), propertyInfo.Name, resolve: context =>
                    {
                        return Convert.ToInt32(propertyInfo.GetValue(context.Source));
                    }); break;
                case nameof(DateTime): Field(GraphTypeMapper.GetGraphType(propertyInfo.PropertyType, isInput: true), propertyInfo.Name, resolve: context => propertyInfo.GetValue(context.Source)); break;
                case "Nullable`1":
                {
                    var underlyingType = Nullable.GetUnderlyingType(propertyInfo.PropertyType);
                    if (underlyingType.IsEnum)
                    {
                        Field(GraphTypeMapper.GetGraphType(underlyingType, isInput: true), propertyInfo.Name);
                    }
                    else
                    {
                        switch (underlyingType.Name)
                        {
                            case nameof(Int32):
                                Field(GraphTypeMapper.GetGraphType(underlyingType, isInput: true), propertyInfo.Name); break;
                            case nameof(Byte):
                                Field(GraphTypeMapper.GetGraphType(underlyingType, isInput: true), propertyInfo.Name); break;
                            case nameof(Int16):
                                Field(GraphTypeMapper.GetGraphType(underlyingType, isInput: true), propertyInfo.Name); break;
                            case nameof(Int64):
                                Field(GraphTypeMapper.GetGraphType(underlyingType, isInput: true), propertyInfo.Name); break;
                            case nameof(Double):
                                Field(GraphTypeMapper.GetGraphType(underlyingType, isInput: true), propertyInfo.Name); break;
                            case nameof(Single):
                                Field(GraphTypeMapper.GetGraphType(underlyingType, isInput: true), propertyInfo.Name); break;
                            case nameof(Boolean):
                                Field(GraphTypeMapper.GetGraphType(underlyingType, isInput: true), propertyInfo.Name); break;
                            case nameof(Decimal):
                                Field(GraphTypeMapper.GetGraphType(underlyingType, isInput: true), propertyInfo.Name); break;
                            case nameof(DateTime):
                                Field(GraphTypeMapper.GetGraphType(underlyingType, isInput: true), propertyInfo.Name); break;
                        }
                    }
                }
                    break;
                case nameof(String):
                default: Field(GraphTypeMapper.GetGraphType(propertyInfo.PropertyType, isInput: true), propertyInfo.Name); break;
            }
        }
    }
    
    public override GraphQLValue ToAST(object value)
    {
        if (value == null)
        {
            return new GraphQLNullValue();
        }
        
        var fields = new List<GraphQLObjectField>();
        
        foreach (var propertyInfo in value.GetType().GetProperties())
        {
            var propertyValue = propertyInfo.GetValue(value);

            if (propertyValue is not null)
            {
                fields.Add(new GraphQLObjectField(
                    new GraphQLName(propertyInfo.Name),
                    (GraphQLValue)Activator.CreateInstance(PropertiesAstNodeType[propertyInfo.Name], propertyValue)!
                ));
            }
            else
            {
                fields.Add(new GraphQLObjectField(
                    new GraphQLName(propertyInfo.Name),
                    new GraphQLNullValue()
                ));
            }
        }

        return new GraphQLObjectValue
        {
            Fields = fields
        };
    }
    
    private Type MakeDictionaryType(PropertyInfo propertyInfo)
    {
        var dictType = propertyInfo.PropertyType.GetGenericTypeAssignableTo(typeof(IDictionary<,>));

        var args = dictType.GetGenericArguments();

        return typeof(DictionaryGraphType<,>).MakeGenericType(args[0], args[1]);
    }
}