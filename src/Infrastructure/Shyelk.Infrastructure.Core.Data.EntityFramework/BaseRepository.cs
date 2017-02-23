using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public BaseRepository(string name)
        {
            _dbContext = SEDbContextManager.GetDbContext(name);
        }
        public BaseRepository(){
            _dbContext=SEDbContextManager.GetDbContext();
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

       public void Dispose()
        {
            if (_dbContext!=null)
            {
                _dbContext.Dispose();
            }
        }

        public Task<int> SaveChangesAsync()
        {
           return _dbContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public void Add(TEntity entity)
        {
            _dbContext.Add(entity);
        }

        public void Update(TEntity entity, params string[] properties)
        {
            if (entity==null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (properties==null||properties.Count()==0)
            {
                try
                {
                    DbSet.Update(entity);
                }
                catch (System.Exception ex)
                {                    
                    throw ex;
                }
            }
            else
            {
               var entry= DbSet.Attach(entity);
               foreach (var property in properties)
               {
                 entry.Property(property).IsModified=true;
               }   
            }
        }
    }
}
