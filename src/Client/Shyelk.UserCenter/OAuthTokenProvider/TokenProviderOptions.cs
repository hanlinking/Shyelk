using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Shyelk.UserCenter.OAuthTokenProvider
{
    /// <summary>
    /// Provides options for <see cref="TokenProviderMiddleware"/>.
    /// </summary>
    public class TokenProviderOptions : IOptions<TokenProviderOptions>
    {
        /// <summary>
        /// The relative request path to listen on.
        /// </summary>
        /// <remarks>The default path is <c>/token</c>.</remarks>
        public string Path { get; set; } = "/token";

        /// <summary>
        ///  The Issuer (iss) claim for generated tokens.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// The Audience (aud) claim for the generated tokens.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// The expiration time for the generated tokens.
        /// </summary>
        /// <remarks>The default is five minutes (300 seconds).</remarks>
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(5);
        ///<summary>
        /// The Signing Key original value;
        ///</summary>
        public string SigningSecurityKey { get; set; }
        ///<summary>
        ///The Algorithm to generate SigningCredentials from SigningSecurityKey
        ///</summary>
        public string SigningAlgorithm{get;set;}
        /// <summary>
        /// The signing key to use when generating tokens.
        /// </summary>
        public SigningCredentials SigningCredentials { get { return new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("luhanlin1@#$%^&#%")),SigningAlgorithm);} }

        /// <summary>
        /// Resolves a user identity given a username and password.
        /// </summary>
        public Func<string, string, Task<ClaimsIdentity>> IdentityResolver { get; set; }
        = (username, password) =>
        {
            if (username == "TEST" && password == "TEST123")
            {
                return Task.FromResult(new ClaimsIdentity(new System.Security.Principal.GenericIdentity(username, "Token"), new Claim[] { }));
            }

            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        };

        /// <summary>
        /// Generates a random value (nonce) for each generated token.
        /// </summary>
        /// <remarks>The default nonce is a random GUID.</remarks>
        public Func<Task<string>> NonceGenerator { get; set; }
            = new Func<Task<string>>(() => Task.FromResult(Guid.NewGuid().ToString()));

        TokenProviderOptions IOptions<TokenProviderOptions>.Value
        {
            get { return this; }
        }
    }
}