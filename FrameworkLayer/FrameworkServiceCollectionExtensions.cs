using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkLayer
{
    public static class FrameworkServiceCollectionExtensions
    {
        public static IServiceCollection AddFrameworkEntities(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
        {
            var databaseProvider = configuration.GetSection("DatabaseProvider").Value;
            var connectionString = configuration.GetConnectionString(databaseProvider);

            return services
                .AddSingleton<IEntityTypeProvider>(sp => new EntityTypeProvider(assemblies))
                .AddDbContextFactory<PlaygroundDbContext>(options => {
                    _ = databaseProvider switch
                    {
                        "PostgreSQL" => options.UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(PlaygroundDbContext).Assembly.FullName)),
                        "SQLite" => options.UseSqlite(connectionString, x => x.MigrationsAssembly(typeof(PlaygroundDbContext).Assembly.FullName)),
                        _ => throw new ArgumentException("No DatabaseProvider value was specified in config")
                    };
                });
        }
    }
}
