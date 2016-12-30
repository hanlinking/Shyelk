using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shyelk.Infrastructure.Core.Data.EntityFramework
{

    public abstract class BaseEntity<TKey>
    {
        public virtual TKey Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
    }
    public abstract class GenericEntity<TKey> : BaseEntity<TKey>
    {
        ///<summary>
        ///系统状态,True为正常，false为删除
        ///</summary>
        public virtual bool Sys_Status { get; set; } = true;
        ///<summary>
        ///修改人(用户名)
        ///</summary>
        public virtual string Sys_Modifier { get; set; }
        ///<summary>
        ///修改时间(UTC)
        ///</summary>
        public virtual DateTime? Sys_ModifyTimeUtc { get; set; }
        ///<summary>
        /// 修改时间(服务器时间)
        ///</summary>
        public virtual DateTime? Sys_ModifyTime { get; set; }
        ///<summary>
        ///创建人(用户名)
        ///</summary>
        public virtual string Sys_Creator { get; set; } = "Administrator";
        ///<summary>
        ///创建时间(UTC)
        ///</summary>
        public virtual DateTime Sys_CreateTimeUtc { get; set; } = DateTime.UtcNow;
        ///<summary>
        ///创建时间(服务器时间)
        ///</summary>
        public virtual DateTime Sys_CreateTime { get; set; } = DateTime.Now;
        ///<summary>
        ///数据来源
        ///</summary>
        public virtual string Sys_DataSource { get; set; } = "手动创建";
    }
    public abstract class GenericEntity : GenericEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
    }
}
