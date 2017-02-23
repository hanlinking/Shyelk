using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Shyelk.Infrastructure.Core.Security.Authentication
{
    public class JwtTokenHandler : JwtSecurityTokenHandler
    {
        protected override ClaimsIdentity CreateClaimsIdentity(JwtSecurityToken jwt, string issuer, TokenValidationParameters validationParameters)
        {
            object username = null;
            JwtIndentity identity = null;
            if (jwt.Payload.TryGetValue("UserName", out username))
            {
                identity = new JwtIndentity("AuthenticationTypes.Federation", true, username.ToString());
            }
            var result = base.CreateClaimsIdentity(jwt, issuer, validationParameters);
            var custom = new ClaimsIdentity(identity, jwt.Claims);
            return custom;
        }
    }
}