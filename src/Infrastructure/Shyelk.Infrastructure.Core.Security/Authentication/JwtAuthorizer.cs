namespace Shyelk.Infrastructure.Core.Security.Authentication
{
    public class JwtAuthorizee
    {
        public JwtAuthorizee(string userName,string password)
        {
            UserName=userName;
            Password=password;
        }
        public JwtAuthorizee(){}
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}