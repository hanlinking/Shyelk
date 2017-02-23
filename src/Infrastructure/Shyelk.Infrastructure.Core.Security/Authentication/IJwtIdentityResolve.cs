using System.Security.Claims;
using System.Threading.Tasks;

namespace Shyelk.Infrastructure.Core.Security.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJwtIdentityResolver<T> where T:JwtAuthorizee
    {
         Task<ClaimsIdentity> Authorize(T authorizer);
    }
}