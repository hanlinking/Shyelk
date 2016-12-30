using Shyelk.Infrastructure.Core.Data.EntityFramework;
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
        public string RoleName { get; set; }
    }
}