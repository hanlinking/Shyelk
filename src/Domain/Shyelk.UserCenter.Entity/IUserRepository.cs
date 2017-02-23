using System;
using Shyelk.Infrastructure.Core.Data.EntityFramework;

namespace Shyelk.UserCenter.Entity
{
    /// <summary>
    /// 用户表仓储
    /// </summary>
    public interface IUserRepository : IRepository<string, User>
    {
        
    }
}