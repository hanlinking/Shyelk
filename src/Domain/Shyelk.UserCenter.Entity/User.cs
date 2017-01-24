using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shyelk.Infrastructure.Core.Data.EntityFramework;

namespace Shyelk.UserCenter.Entity
{
    /// <summary>
    ///  用户实体
    /// </summary>
    public class User : GenericEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        ///<value><see cref="String"></see></value>
        public string UserName { get; set; }
        /// <summary>
        /// 密码哈希值
        /// </summary>
        ///<value><see cref="String"></see></value>
        public string PasswordHash { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        ///<value><see cref="String"></see></value>
        public string NickName { get; set; }
        /// <summary>
        /// 用户电子邮箱
        /// </summary>
        ///<value><see cref="String"></see></value>
        public string Email { get; set; }
        /// <summary>
        /// 用户联系电话
        /// </summary>
        ///<value><see cref="String"></see></value>
        public string Phone { get; set; }
        /// <summary>
        /// 用户简介
        /// </summary>
        ///<value><see cref="String"></see></value>
        public string Summary { get; set; }
        /// <summary>
        /// 用户头像地址
        /// </summary>
        ///<value><see cref="String"></see></value>
        public string HeaderUrl { get; set; }
        public virtual ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
        public virtual ICollection<LoginHistory> LoginHistories { get; set; } = new List<LoginHistory>();
    }
}
