using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkLayer
{
    public interface IEntityTypeProvider
    {
        IEnumerable<Type> GetEntityTypes();
    }

    public class EntityTypeProvider : IEntityTypeProvider
    {
        private readonly IEnumerable<Type> _types;

        public EntityTypeProvider(params Assembly[] assemblies)
        {
            _types = assemblies
                .SelectMany(a => a.GetExportedTypes())
                .Where(t => !t.IsAbstract && t.IsClass)
                .Where(t => typeof(IEntity).IsAssignableFrom(t))
                .ToList();
        }

        public IEnumerable<Type> GetEntityTypes() => _types;
    }
}
