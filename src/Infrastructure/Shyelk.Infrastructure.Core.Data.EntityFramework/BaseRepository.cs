using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Shyelk.Infrastructure.Core.Data.EntityFramework
{
    public abstract class BaseRepository<Tkey, TEntity> : IRepository<Tkey, TEntity>
    where Tkey : class
    where TEntity : BaseEntity<Tkey>
    {
        protected readonly SEDbContext _dbContext;
        private readonly DbSet<TEntity> _DbSet;
        public BaseRepository(SEDbContext dbContext)
        {

            _dbContext = dbContext;
        }
        protected virtual DbSet<TEntity> DbSet { get { return _DbSet ?? _dbContext.Set<TEntity>(); } }

        public virtual IQueryable<TEntity> Query { get { return DbSet as IQueryable<TEntity>; } }

        public virtual TEntity Get(Tkey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return DbSet.SingleOrDefault(s => s.Id == key);
        }

        public virtual Task<TEntity> GetAsync(Tkey key, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return DbSet.SingleOrDefaultAsync(s => s.Id == key, cancellationToken);
        }

    }
}
