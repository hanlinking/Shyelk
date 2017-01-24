using Shyelk.Infrastructure.Core.Data.EntityFramework;
using Shyelk.UserCenter.Entity;

namespace Shyelk.UserCenter.Repository
{
    public class RoleRepository : BaseRepository<string, User>, IUserRepository
    {
        public RoleRepository(SEDbContext dbContext) : base(dbContext)
        {
        }
    }
}