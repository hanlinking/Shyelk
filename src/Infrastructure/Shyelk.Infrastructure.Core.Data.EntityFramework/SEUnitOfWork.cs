using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Shyelk.Infrastructure.Core.Data.EntityFramework
{
    public class SEUnitOfWork : IUnitOfWork
    {
        private readonly SEDbContext _dbContext;
        public SEUnitOfWork(string connectionName)
        {
            _dbContext = SEDbContextManager.GetDbContext(connectionName);
        }
        public SEUnitOfWork()
        {
            _dbContext = SEDbContextManager.GetDbContext();
        }
        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }
    }
}
