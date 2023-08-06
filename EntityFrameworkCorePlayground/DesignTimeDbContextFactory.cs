using FrameworkLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCorePlayground
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PlaygroundDbContext>
    {
        public PlaygroundDbContext CreateDbContext(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddFrameworkEntities(_.Configuration, typeof(DesignTimeDbContextFactory).Assembly);
                })
                .Build();

            return host.Services.GetRequiredService<IDbContextFactory<PlaygroundDbContext>>().CreateDbContext();
        }
    }
}
