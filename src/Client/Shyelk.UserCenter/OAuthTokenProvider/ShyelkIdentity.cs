using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Shyelk.UserCenter.OAuthTokenProvider
{
    public class ShyelkIndentity:IIdentity
    {
        public ShyelkIndentity(string authenticationType,bool isAuthenticated,string name)
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
