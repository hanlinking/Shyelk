using System;
using System.ComponentModel.DataAnnotations;
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
        [RequiredAttribute]
        [MaxLengthAttribute(40)]
        public string UserId { get; set; }
        ///<summary>
        ///登录Ipv4
        ///</summary>
        [RequiredAttribute]
        [MaxLengthAttribute(16)]
        public string Ipv4 { get; set; }
        ///<summary>
        ///登录Ipv6
        ///</summary>
        [RequiredAttribute]
        [MaxLengthAttribute(46)]
        public string Ipv6 { get; set; }
        ///<summary>
        ///登录位置
        ///</summary>
        public string Location { get; set; }
        ///<summary>
        ///登录系统Id
        ///</summary>
        [RequiredAttribute]
        public Guid SystemId { get; set; }
        /// <summary>
        /// 登录系统名称
        /// </summary>
        [RequiredAttribute]
        [MaxLengthAttribute(50)]
        public string SystemName { get; set; }
        public virtual User User { get; set; }
    }
}