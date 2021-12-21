using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EasyAbp.Abp.GraphQL.EntityFrameworkCore;

public class GraphQLHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<GraphQLHttpApiHostMigrationsDbContext>
{
    public GraphQLHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<GraphQLHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("GraphQL"));

        return new GraphQLHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}