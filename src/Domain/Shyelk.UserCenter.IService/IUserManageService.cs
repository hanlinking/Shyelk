using System;
using System.Security.Claims;
using Shyelk.UserCenter.Models;
using System.Threading.Tasks;

namespace Shyelk.UserCenter.IService
{
    public interface IUserManageService:Shyelk.Infrastructure.Core.Service.IService
    {
        ///<summary>
        /// 登陆
        ///</summary>
        Task<ClaimsIdentity> LoginAsync(LoginDto dto);
        Task<bool>  CreateAsync(UserDto dto);
        Task<UserDto> GetUserByName(string userName);
        VerficateCode GetVerficationCode();
    }
}