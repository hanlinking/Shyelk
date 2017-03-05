using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shyelk.UserCenter.IService;
using Shyelk.UserCenter.Models;

namespace Shyelk.UserCenter.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManageService _userManageService;
        private readonly ILogger _logger;
        public AccountController(IUserManageService userManageService, ILoggerFactory _loggerFactory)
        {
            _userManageService = userManageService;
            _logger = _loggerFactory.CreateLogger(nameof(AccountController));
        }
        [HttpGet]
        public IActionResult Login(string returnUrl=null)
        {
            // ViewData["ReturnUrl"]=returnUrl;
            return View();
        }
    }
}