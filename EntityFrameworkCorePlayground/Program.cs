using FrameworkLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EntityFrameworkCorePlayground;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((host, services) =>
            {
                services
                    .AddSingleton<IEntityTypeProvider>(sp => new EntityTypeProvider(typeof(Program).Assembly))
                    .AddDbContextFactory<PlaygroundDbContext>(options => options.UseNpgsql("Host=localhost;Database=PLXSampleDev;Username=plx_system;Password=test"))
                    .Configure<DbContextConfiguration>(host.Configuration.GetSection("DbContextConfiguration"));
            })
            .Build();

        var factory = host.Services.GetRequiredService<IDbContextFactory<PlaygroundDbContext>>();
        using var context = await factory.CreateDbContextAsync();

        var listEntities = new List<TestEntity>
        {
            new TestEntity
            {
                Name = "Hello there",
                Description = "General Kenobi",
                Amount = 1.23m,
                IsRequired = true
            },
            new TestEntity
            {
                Name = "Hello there",
                Description = "General Kenobi",
                Amount = 1.23m,
                IsRequired = false
            }
        };

        var newEntities = new List<NewTestEntity>
        {
            new NewTestEntity
            {
                ProductCode = "Test Key",
                ProductDescription = "Test Description",
                QuantitySold = 1,
                UnitPrice = 1.23m,
                TotalPrice = 1.23m,
                TimeAdded = new DateTime(2022, 1, 1)
            },
            new NewTestEntity
            {
                ProductCode = "Test Key",
                ProductDescription = "Test Description 1",
                QuantitySold = 2,
                UnitPrice = 1.23m,
                TotalPrice = 2.46m,
                TimeAdded = new DateTime(2022, 1, 2)
            }
        };

        await context.Set<TestEntity>().AddRangeAsync(listEntities);
        await context.Set<NewTestEntity>().AddRangeAsync(newEntities);

        Console.WriteLine("BEFORE");
        foreach (var entity in newEntities)
        {
            Console.WriteLine($"{entity.ProductCode} {entity.ProductDescription} {entity.UnitPrice} {entity.QuantitySold} {entity.TotalPrice} {entity.TimeAdded}");
        }

        await context.SaveChangesAsync();

        Console.WriteLine("AFTER");
        foreach (var entity in newEntities)
        {
            Console.WriteLine($"{entity.ProductCode} {entity.ProductDescription} {entity.UnitPrice} {entity.QuantitySold} {entity.TotalPrice} {entity.TimeAdded}");
        }
    }
}


public class DbContextConfiguration
{
    public string DatabaseServer { get; set; }
}