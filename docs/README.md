# Abp.GraphQL

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FAbp.GraphQL%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.Abp.GraphQL.Provider.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.GraphQL.Provider.Shared)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.Abp.GraphQL.Provider.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.GraphQL.Provider.Shared)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/Abp.GraphQL?style=social)](https://www.github.com/EasyAbp/Abp.GraphQL)
![Discord Shield](https://discordapp.com/api/guilds/924946213713895445/widget.png?style=shield)

An ABP module that allows using application services by GraphQL. It also accepted custom schemes and types you defined.

![UI](/docs/images/UI.apng)

## Installation

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

    * EasyAbp.Abp.GraphQL.Application
    * EasyAbp.Abp.GraphQL.Application.Contracts
    * EasyAbp.Abp.GraphQL.HttpApi
    * EasyAbp.Abp.GraphQL.HttpApi.Client
    * EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet (install to the Application layer)
    * EasyAbp.Abp.GraphQL.Web.Altair (optional)
    * EasyAbp.Abp.GraphQL.Web.GraphiQL (optional)
    * EasyAbp.Abp.GraphQL.Web.Playground (optional)
    * EasyAbp.Abp.GraphQL.Web.Voyager (optional)

2. Add `DependsOn(typeof(Abp.GraphQLXxxModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-module-dependencies))

## Usage

1. Configure the module to auto lookup AppServices.
    ```c#
    Configure<AbpGraphQLOptions>(options =>
    {
        // Find entities: Book, Author, City...
        options.AppServiceSchemes.Configure(
            typeof(MyProjectApplicationContractsModule).Assembly);

        // Find entities: IdentityUser, IdentityRole
        options.AppServiceSchemes.Configure(
            typeof(AbpIdentityApplicationContractsModule).Assembly);
    });
    ```
1. Configure the GraphQL UIs (if you just installed them).
    ```c#
    Configure<AbpAntiForgeryOptions>(options =>
    {
        // PR need: inject the RequestVerificationToken header to UI's AJAX request.
        options.AutoValidateFilter = type => type.Namespace != null &&
            !type.Namespace.StartsWith("EasyAbp.Abp.GraphQL");
    });

    Configure<AbpGraphiQLOptions>(options =>
    {
        // options.UiBasicPath = "/myPath";
    });
    ```

1. Now you can query your entities with GraphQL.
   ```graphql
   query {
      book(id: "CA2EBE5D-D0DC-4D63-A77A-46FF520AEC44") {
         name
         author {
            id
            name
         }
      }
   }
   ```

## Q&A

The following contents are for the [graphql-dotnet provider](https://github.com/EasyAbp/Abp.GraphQL/tree/main/src/EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet), please go to graphql-dotnet's [GitHub repo](https://github.com/graphql-dotnet/graphql-dotnet) or [docs site](https://graphql-dotnet.github.io/) for more information.

### How to customize an auto-created AppService scheme?

You can replace the AppServiceQuery class for an entity you want to customize, see [the demo](https://github.com/EasyAbp/Abp.GraphQL/blob/main/test/EasyAbp.Abp.GraphQL.Provider.GraphQLDotnet.Tests/AuthorAppServiceQuery.cs#L15).

### How to create a schema myself?

1. Create your schema.
   ```c#
   public class MyCustomSchema : Schema, ITransientDependency
   {
       public MySchema(IServiceProvider serviceProvider) : base(serviceProvider)
       {
           Query = serviceProvider.GetRequiredService<MyCustomQuery>();
       }
   }
   ```
2. Configure to map the `/MyCustom` path to MyCustomSchema for UIs (if you want).
   ```c#
   Configure<AbpEndpointRouterOptions>(options =>
   {
       options.EndpointConfigureActions.Add(builderContext =>
       {
           var uiOptions =
               builderContext.ScopeServiceProvider.GetRequiredService<IOptions<AbpGraphiQLOptions>>().Value;
   
            var schemaUiOption = (GraphiQLOptions)uiOptions.Clone();
            schemaUiOption.GraphQLEndPoint = schemaUiOption.GraphQLEndPoint.Value.EnsureEndsWith('/') + "MyCustom";
            schemaUiOption.SubscriptionsEndPoint = schemaUiOption.SubscriptionsEndPoint.Value.EnsureEndsWith('/') + "MyCustom";
   
            builderContext.Endpoints.MapGraphQLGraphiQL(schemaUiOption,
                uiOptions.UiBasicPath.RemovePreFix("/").EnsureEndsWith('/') + "MyCustom");
       });
   });
   ```

## Road map

- [x] Support Query.
- [ ] Support Mutation.
- [ ] Support Subscription.
- [ ] Improve UI modules.
