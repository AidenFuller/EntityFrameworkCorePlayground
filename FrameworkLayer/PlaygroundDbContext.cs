using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkLayer
{
    public class PlaygroundDbContext : DbContext
    {
        private readonly IEntityTypeProvider _entityTypeProvider;

        public PlaygroundDbContext(DbContextOptions options, IEntityTypeProvider entityTypeProvider) : base(options)
        {
            _entityTypeProvider = entityTypeProvider ?? throw new ArgumentNullException(nameof(entityTypeProvider));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var type in _entityTypeProvider.GetEntityTypes())
            {
                // Register the model and build from config
                modelBuilder.Entity(type);
            }
        }
    }
}
