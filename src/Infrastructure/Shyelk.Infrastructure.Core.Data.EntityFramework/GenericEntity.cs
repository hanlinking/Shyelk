using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Shyelk.Infrastructure.Core.Converter;

namespace Shyelk.Infrastructure.Core.Data.EntityFramework
{
    public abstract class GenericEntity<TKey> : BaseEntity<TKey>
    {
        ///<summary>
        ///系统状态,True为正常，false为删除
        ///</summary>
        public virtual bool Sys_Status { get; set; } = true;
        ///<summary>
        ///修改人(用户名)
        ///</summary>
        public virtual string Sys_Modifier { get; set; } = "Administrator";
        ///<summary>
        ///修改时间(UTC)
        ///</summary>
        public virtual DateTime? Sys_ModifyTimeUtc { get; set; } = DateTime.UtcNow;
        ///<summary>
        /// 修改时间(服务器时间)
        ///</summary>
        public virtual DateTime? Sys_ModifyTime { get; set; } = DateTime.Now;
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
        /// <summary>
        /// 时间戳
        /// </summary>
        [Timestamp]
        public virtual byte[] Sys_Timestamp { get; set; }
    }
    public abstract class GenericEntity : GenericEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToShortGuidString();
    }
}
