using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Shyelk.UserCenter.OAuthTokenProvider
{
    public class ShyelkTokenHandler:JwtSecurityTokenHandler
    {
        protected override ClaimsIdentity CreateClaimsIdentity(JwtSecurityToken jwt, string issuer, TokenValidationParameters validationParameters)
        {
            object username=null;
            ShyelkIndentity identity=null;
            if(jwt.Payload.TryGetValue("UserName",out username))
            {
                identity=new ShyelkIndentity("AuthenticationTypes.Federation",true,username.ToString());
            }            
            var result= base.CreateClaimsIdentity(jwt,issuer,validationParameters);
            var custom= new ClaimsIdentity(identity,jwt.Claims);
            return custom;
        }
    }
}
