using System;
using Shyelk.UserCenter.Entity;
using Shyelk.Infrastructure.Core.Data.EntityFramework;

namespace Shyelk.UserCenter.Repository
{
    public class UserRepository : BaseRepository<string, User>, IUserRepository
    {
        public UserRepository() : base()
        {
        }
    }
}
