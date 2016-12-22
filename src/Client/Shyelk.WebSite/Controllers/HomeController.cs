using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Shyelk.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache _redisCache;
        public HomeController(IDistributedCache redisCache)
        {
            _redisCache=redisCache;
        }
        public IActionResult Index()
        {
            string result=_redisCache.GetString("henrykey");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
