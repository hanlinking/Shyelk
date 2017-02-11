using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Shyelk.Infrastructure.Core.Data.EntityFramework
{
    public interface IUnitOfWork: System.IDisposable
    {
         int SaveChanges();
         Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
         IDbContextTransaction BeginTransaction();
         Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));
         void CommitTransaction();
    }
}