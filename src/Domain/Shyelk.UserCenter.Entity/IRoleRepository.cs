using System;
using Shyelk.Infrastructure.Core.Data.EntityFramework;

namespace Shyelk.UserCenter.Entity
{
    public interface IRoleRepository : IRepository<string, Role>
    {

    }
}