using System;
using Shyelk.Infrastructure.Core.Data.EntityFramework;

namespace Shyelk.UserCenter.Entity
{
    /// <summary>
    /// 登录历史
    /// </summary>
    public class LoginHistory : GenericEntity
    {
        /// <summary>
        /// 登录用户Id
        /// </summary>
        public string UserId { get; set; }
        ///<summary>
        ///登录Ip
        ///</summary>
        public string Ip { get; set; }
        ///<summary>
        ///登录位置
        ///</summary>
        public string Location { get; set; }
        ///<summary>
        ///登录系统Id
        ///</summary>
        public Guid SystemId { get; set; }
        /// <summary>
        /// 登录系统名称
        /// </summary>
        public string SystemName { get; set; }
        public virtual User User { get; set; }
    }
}