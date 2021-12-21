using System;
using EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.GraphTypes;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet;

public class AppServiceSchema<TAppService, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput> : Schema
    where TAppService : IReadOnlyAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>
    where TGetListInput : class
    where TGetOutputDto : class
    where TGetListOutputDto : class
{
    public AppServiceSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<AppServiceQuery<TAppService, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>>();
            
        this.RegisterTypeMapping<TGetOutputDto, GraphQLGenericType<TGetOutputDto>>();
        this.RegisterTypeMapping<TGetListOutputDto, GraphQLGenericType<TGetListOutputDto>>();
    }
}