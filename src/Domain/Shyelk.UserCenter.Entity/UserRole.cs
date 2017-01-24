using Shyelk.Infrastructure.Core.Data.EntityFramework;
using System.Collections.Generic;
namespace Shyelk.UserCenter.Entity
{
    /// <summary>
    /// 用户角色配对表
    /// </summary>
    public class UserRole : GenericEntity
    {
        public string RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}