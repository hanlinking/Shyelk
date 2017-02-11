using Shyelk.Infrastructure.Core.Data.EntityFramework;
using Shyelk.UserCenter.Entity;

namespace Shyelk.UserCenter.Repository
{
    public class RoleRepository : BaseRepository<string, Role>, IRoleRepository
    {
        public RoleRepository() : base()
        {
        }
    }
}