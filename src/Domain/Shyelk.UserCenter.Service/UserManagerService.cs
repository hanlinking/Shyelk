using Shyelk.UserCenter.IService;
using Shyelk.UserCenter.Entity;
using Shyelk.Infrastructure.Core.Data.EntityFramework;
using Shyelk.Infrastructure.Core.Security;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using Shyelk.UserCenter.Models;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;

namespace Shyelk.UserCenter.Service
{
    public class UserManagerService : IUserManageService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private IUnitOfWork _unitOfWork;
        public UserManagerService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper, ILoggerFactory loggerFactory)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _unitOfWork = new SEUnitOfWork();
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger(nameof(UserManagerService));
        }

        public Task<bool> CreateAsync(UserDto dto)
        {
            return Task<bool>.Factory.StartNew(() =>
               {
                   try
                   {
                       User user = _mapper.Map<User>(dto);
                       _logger.LogDebug(JsonConvert.SerializeObject(user));
                       var pwmd5 = MD5Tools.MD5Encrypt32(dto.Password);
                       var scmd5 = StringGenerator.GetMixString(32);
                       var pwdHash = MD5Tools.MD5Encrypt64(pwmd5 + scmd5);
                       user.PasswordHash = pwdHash;
                       user.SecurityCode = scmd5;
                       _userRepository.Add(user);
                       int count = _unitOfWork.SaveChanges();
                       _logger.LogDebug("保存成功" + count);
                   }
                   catch (System.Exception ex)
                   {
                       _logger.LogError(ex.Message);
                       return false;
                   }
                   return true;
               });
        }

        public Task<UserDto> GetUserByName(string userName)
        {
            return Task.FromResult<UserDto>(_userRepository.Query.Where(u => u.UserName == userName).ProjectTo<UserDto>().FirstOrDefault());
        }

        public Task<ClaimsIdentity> LoginAsync(LoginDto dto)
        {
            User user = _userRepository.Query.FirstOrDefault(f => f.Email == dto.Account || f.Phone == dto.Account || f.UserName == dto.Account);
            if (user == null)
            {
                return Task.FromResult<ClaimsIdentity>(null);
            }
            string pwmd5 = MD5Tools.MD5Encrypt32(dto.Password);
            string pwdHash = MD5Tools.MD5Encrypt64(pwmd5 + user.SecurityCode);
            if (pwdHash==user.PasswordHash)
            {
                return Task.FromResult(new ClaimsIdentity(new System.Security.Principal.GenericIdentity(dto.Account, "Token"), new Claim[] { }));
            }
            return Task.FromResult<ClaimsIdentity>(null);

        }

    }
}