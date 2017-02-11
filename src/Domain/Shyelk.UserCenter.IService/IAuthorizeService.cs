using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shyelk.UserCenter.IService
{
    public interface IAuthorizeService:Shyelk.Infrastructure.Core.Service.IService
    {
        ///<summary>
        /// 登陆
        ///</summary>
        ///<param name="account">账户名(用户名/Email/手机号码)</param>
        ///<param name="password">密码</param>
        Task<ClaimsIdentity> Login(string account,string password);
    }
}