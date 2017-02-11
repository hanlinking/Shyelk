using Shyelk.UserCenter.IService;
using Shyelk.UserCenter.Entity;
using Shyelk.Infrastructure.Core.Data.EntityFramework;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shyelk.UserCenter.Service
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private IUnitOfWork _unitOfWork;
        public AuthorizeService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public Task<ClaimsIdentity> Login(string account, string password)
        {
            bool result = true;
            if (result)
            {
                return Task.FromResult(new ClaimsIdentity(new System.Security.Principal.GenericIdentity(account, "Token"), new Claim[] { }));
            }
            return Task.FromResult<ClaimsIdentity>(null);
        }

    }
}