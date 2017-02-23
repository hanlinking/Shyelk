using System.Security.Principal;

namespace Shyelk.Infrastructure.Core.Security.Authentication
{
    public class JwtIndentity:IIdentity
    {
        public JwtIndentity(string authenticationType,bool isAuthenticated,string name)
        {
            AuthenticationType=authenticationType;
            IsAuthenticated=isAuthenticated;
            Name=name;
        }

        public string AuthenticationType
        {
            get;
        }

        public bool IsAuthenticated
        {
            get;
        }

        public string Name
        {
            get;
        }
    }
}