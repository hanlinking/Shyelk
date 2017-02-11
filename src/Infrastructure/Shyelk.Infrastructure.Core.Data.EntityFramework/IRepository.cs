using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shyelk.Infrastructure.Core.Data.EntityFramework
{
    public interface IRepository<Tkey, TEntity>: System.IDisposable
    {
        ///<summary>
        ///根据Key获取实体
        ///</summary>
        ///<param name="key">实体的主键</param>
        ///<returns>实体</returns>
        TEntity Get(Tkey key);
        ///<summary>
        ///根据Key异步获取实体
        ///</summary>
        ///<param name="key">实体的主键</param>
        ///<param name="cancellationToken"></param>
        ///<returns>实体</returns>
        Task<TEntity> GetAsync(Tkey key, CancellationToken cancellationToken = default(CancellationToken));
        ///<summary>
        ///获取TEntity 查询的IQuerable<TEntity> 对象
        ///</summary>   
        IQueryable<TEntity> Query { get; }
    }
}
