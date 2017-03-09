using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;

namespace Shyelk.Infrastructure.Core.Data.EntityFramework
{
    public interface IRepository<Tkey, TEntity>: System.IDisposable
    {
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Add(TEntity entity);
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
        /// <summary>
        /// 异步持久化到数据库
        /// </summary>
        /// <returns>数量</returns>
        Task<int> SaveChangesAsync();
        /// <summary>
        /// 持久化数据库
        /// </summary>
        /// <returns>数量</returns>
        int SaveChanges();
        /// <summary>
        /// 更新实体到数据库
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="param">部分要更新的属性名称(该参数不传值，则默认更新所有字段)</param>
        void Update(TEntity entity,params string[] properties);
        /// <summary>
        /// 按条件更新数据
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="expression">需要更新的属性表达式</param>
        void Update(Expression<Func<TEntity,bool>> filter,Expression<Func<TEntity,TEntity>> expression);
    }
}
