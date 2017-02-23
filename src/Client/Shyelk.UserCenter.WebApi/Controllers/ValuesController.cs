using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shyelk.UserCenter.Models;
using Shyelk.UserCenter.IService;
using Microsoft.Extensions.Logging;

namespace Shyelk.UserCenter.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserManageService _userManageService;
        public ValuesController(IUserManageService userManageService,ILoggerFactory loggerFactory)
        {
            _userManageService=userManageService;
            _logger=loggerFactory.CreateLogger<ValuesController>();
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserDto dto)
        {
            
            var result= await this._userManageService.CreateAsync(dto);
            _logger.LogDebug(result.ToString());
            return Ok(result);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
