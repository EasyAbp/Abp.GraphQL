using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.Abp.GraphQL.EntityFrameworkCore;

public class GraphQLHttpApiHostMigrationsDbContext : AbpDbContext<GraphQLHttpApiHostMigrationsDbContext>
{
    public GraphQLHttpApiHostMigrationsDbContext(DbContextOptions<GraphQLHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }
}