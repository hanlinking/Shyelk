using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shyelk.UserCenter.Models;
using Shyelk.UserCenter.IService;
using Microsoft.Extensions.Logging;

namespace Shyelk.UserCenter.Web.ApiControllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserManageService _userManageService;
        public AccountController(IUserManageService userManageService, ILoggerFactory loggerFactory)
        {
            _userManageService = userManageService;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }
        // POST api/values
        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserDto dto)
        {

            var result = await this._userManageService.CreateAsync(dto);
            _logger.LogDebug(result.ToString());
            return Ok(result);
        }
        [HttpGet]
        [Route("SetHeader")]
        public IActionResult SetHeader()
        {
            return Ok();
        }
        [HttpGet]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var result = await this._userManageService.GetUserByName("TEST");
                return Ok(result);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetVerficationCode")]
        public IActionResult VerificationCode()
        {
            return Ok(this._userManageService.GetVerficationCode());
        }
    }
}
