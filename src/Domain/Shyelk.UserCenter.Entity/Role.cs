using Shyelk.Infrastructure.Core.Data.EntityFramework;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
namespace Shyelk.UserCenter.Entity
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : GenericEntity
    {
        ///<summary>
        ///角色名
        ///</summary>
        [Required]
        [MaxLengthAttribute(18)]
        public string RoleName { get; set; }
        public virtual ICollection<UserRole> Users { get; set; } = new List<UserRole>();
    }
}