using Shyelk.UserCenter.IService;
using Shyelk.UserCenter.Entity;
using Shyelk.Infrastructure.Core.Data.EntityFramework;
using Shyelk.Infrastructure.Core.Security;
using Shyelk.UserCenter.Models;
using Shyelk.Tools.Drawing;
using Shyelk.Infrastructure.Core.Data.EntityFramework.Extensions;
using Shyelk.Infrastructure.Core.Converter;
using Shyelk.Infrastructure.Core.Caching.Redis;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;
using System;


namespace Shyelk.UserCenter.Service
{
    public class UserManagerService : IUserManageService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IRedisCache _redisCache;
        private IUnitOfWork _unitOfWork;
        private const string V_CODE_FORMAT = "0000000-V_CODE:{0}";
        public UserManagerService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper, IRedisCache redisCache, ILoggerFactory loggerFactory)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _unitOfWork = new SEUnitOfWork();
            _mapper = mapper;
            _redisCache = redisCache;
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
                       var scmd5 = Guid.NewGuid().ToString("N");
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
            var result = _userRepository.Query.Where(u => u.UserName == userName).ToSql();
            return Task.FromResult<UserDto>(_userRepository.Query.Where(u => u.UserName == userName).ProjectTo<UserDto>().FirstOrDefault());
        }

        public VerficateCode GetVerficationCode()
        {
            string code = StringGenerator.GetMixString(4);
            byte[] image = VerificationCode.Generate(code, DrawFormat.Png);
            VerficateCode vcode = new VerficateCode();
            vcode.Image = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(image));
            vcode.AntiForgetCode = Guid.NewGuid().ToShortGuidString().ToLower();
            Task.Factory.StartNew(() =>
            {
                string key = string.Format(V_CODE_FORMAT, vcode.AntiForgetCode);
                _redisCache.Set(key,code,TimeSpan.FromMinutes(5));
            });
            return vcode;
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
            if (pwdHash == user.PasswordHash)
            {
                return Task.FromResult(new ClaimsIdentity(new System.Security.Principal.GenericIdentity(dto.Account, "Token"), new Claim[] { }));
            }
            return Task.FromResult<ClaimsIdentity>(null);

        }

    }
}