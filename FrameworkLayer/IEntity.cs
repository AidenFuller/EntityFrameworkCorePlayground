using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkLayer
{
    public interface IEntity
    {
    }

    public abstract class IdentifiableEntity : IEntity
    {
        public int Id { get; set; }
    }
}
