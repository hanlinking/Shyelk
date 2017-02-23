using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Shyelk.UserCenter.Controllers
{
    public class ValuesController : Controller
    {
        public IActionResult Get()
        {
            return Ok("搞定");
        }
        [Authorize]
        public IActionResult Add()
        {
            var name = User.Identity.Name;
            return Ok(name);
        }
        [RouteAttribute("AccessDenied")]
        public IActionResult AuthenticationFail()
        {
            return Ok("验证失败");
        }
        public IActionResult Login([FromQueryAttribute]string ReturnUrl)
        {
            return Ok("登陆");
        }
    }
}