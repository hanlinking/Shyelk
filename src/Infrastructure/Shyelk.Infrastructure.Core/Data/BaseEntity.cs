using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shyelk.Infrastructure.Core.Converter;

namespace Shyelk.Infrastructure.Core.Data
{

    public abstract class BaseEntity<TKey>
    {
        public virtual TKey Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
    }
}
