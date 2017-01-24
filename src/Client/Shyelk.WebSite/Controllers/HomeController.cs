using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shyelk.Infrastructure.Core.Converter;
using Microsoft.Extensions.Caching.Distributed;
using Shyelk.Infrastructure.Core.Caching.Redis;
using StackExchange.Redis;
using Microsoft.Extensions.Logging;

namespace Shyelk.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private const string HasSetString = "";
        private static List<testobj> _testlist;
        public static List<testobj> Testobj { get { return _testlist ?? (_testlist = Gettestobj()); } }
        private readonly IRedisCache _redisCache;
        private readonly ILogger _logger;
        private static List<testobj> Gettestobj()
        {
            List<testobj> objlist = new List<testobj>();
            for (int i = 0; i < 200000; i++)
            {
                objlist.Add(new testobj(i));
            }
            return objlist;
        }
        public HomeController(IRedisCache redisCache,ILoggerFactory loggerFactory)
        {
            _redisCache = redisCache;
            _logger=loggerFactory.CreateLogger(nameof(HomeController));
        }
        public IActionResult Index()
        {
            _redisCache.SetString("redistest20170105", "123", new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) });
            string result = _redisCache.GetString("henrykey");
            return Content(result);
        }
        public IActionResult StringSet()
        {
            string data = JsonConvert.SerializeObject(Testobj);
            var fromtime = DateTime.Now;
            var result = _redisCache.GetDatabase().StringSet("testobjlist", data, TimeSpan.FromMinutes(30));
            var totime = DateTime.Now;
            var diff = totime.Subtract(fromtime);
            return Ok(diff);
        }
        public IActionResult HashSet()
        {
            string key = "hashtest";
            var database = _redisCache.GetDatabase();
            var fromtime = DateTime.Now;
            foreach (var item in Testobj)
            {
                var itemstring = JsonConvert.SerializeObject(item);
                database.HashSet(key, item.AA, itemstring);
            }
            var totime = DateTime.Now;
            var diff = totime.Subtract(fromtime);
            return Ok(diff);
        }
        public IActionResult hasdAllSet()
        {
            string key = "hashtest";
            var database = _redisCache.GetDatabase();
            if (database.KeyExists(key))
            {
                database.KeyDelete(key);
            }
            List<HashEntry> entrylist = new List<HashEntry>();
            foreach (var item in Testobj)
            {
                var itemstring = JsonConvert.SerializeObject(item);
                entrylist.Add(new HashEntry(item.AA, itemstring));
            }
            var fromtime = DateTime.Now;
            database.HashSet(key, entrylist.ToArray());
            var totime = DateTime.Now;
            var diff = totime.Subtract(fromtime);
            return Ok(diff);
        }
        public IActionResult ListSet()
        {
            var database = _redisCache.GetDatabase();
            var fromtime = DateTime.Now;
            foreach (var item in Testobj)
            {
                var itemstring = JsonConvert.SerializeObject(item);
                database.ListLeftPush("objlist", itemstring);
            }
            var totime = DateTime.Now;
            var diff = totime.Subtract(fromtime);
            return Ok(diff);
        }
        public IActionResult SetAll()
        {
            string key = "settest";
            var database = _redisCache.GetDatabase();
            if (database.KeyExists(key))
            {
                database.KeyDelete(key);
            }
            List<RedisValue> entrylist = new List<RedisValue>();
            foreach (var item in Testobj)
            {
                var itemstring = JsonConvert.SerializeObject(item);
                entrylist.Add(itemstring);
            }
            var fromtime = DateTime.Now;
            database.SetAdd(key, entrylist.ToArray());
            var totime = DateTime.Now;
            var diff = totime.Subtract(fromtime);
            return Ok(diff);
        }
        public IActionResult ZsetAll()
        {
            string key = "zsettest";
            var database = _redisCache.GetDatabase();
            if (database.KeyExists(key))
            {
                database.KeyDelete(key);
            }
            List<SortedSetEntry> entrylist = new List<SortedSetEntry>();
            for (int i = 0; i < Testobj.Count; i++)
            {
                var itemstring = JsonConvert.SerializeObject(Testobj[i]);
                entrylist.Add(new SortedSetEntry(itemstring, i));
            }
            var fromtime = DateTime.Now;
            database.SortedSetAdd(key, entrylist.ToArray());
            var totime = DateTime.Now;
            var diff = totime.Subtract(fromtime);
            return Ok(diff);
        }
        public IActionResult zsetget()
        {
            var fromtime = DateTime.Now;
            var result = _redisCache.GetDatabase().SortedSetRangeByRank("zsettest");
            var totime = DateTime.Now;
            var diff = totime.Subtract(fromtime);
            return Ok(diff);
        }
        public IActionResult setget()
        {
            var fromtime = DateTime.Now;
            var result = _redisCache.GetDatabase().SetMembers("settest");
            var totime = DateTime.Now;
            var diff = totime.Subtract(fromtime);
            return Ok(diff);
        }
        public IActionResult stringGet()
        {
            var fromtime = DateTime.Now;
            var result = _redisCache.GetDatabase().StringGet("testobjlist");
            var totime = DateTime.Now;
            var diff = totime.Subtract(fromtime);
            return Ok(diff);
        }
        public IActionResult ListGet()
        {
            var fromtime = DateTime.Now;
            var result = _redisCache.GetDatabase().ListRange("objlist");
            var totime = DateTime.Now;
            var diff = totime.Subtract(fromtime);
            return Ok(diff);
        }
        public IActionResult HashGet()
        {
            var fromtime = DateTime.Now;
            var result = _redisCache.GetDatabase().HashGetAll("hashtest");
            var totime = DateTime.Now;
            var diff = totime.Subtract(fromtime);
            return Ok(diff);
        }
        public bool getFormRedis(string key)
        {
            string result = _redisCache.GetString(key);
            if (string.IsNullOrEmpty(result))
            {
                return false;
            }
            return true;
        }
        public bool getFormRedisByte(string key)
        {
            //var result =BasicDataConverter.ByteToObject<List<testobj>>(_redisCache.Get(key));
            //return result!=null;
            return false;
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
        public IActionResult VerificationCode()
        {
            var result = Shyelk.Tools.Drawing.VerificationCode.Generate("2324");
            return File(result, "image/Png");
        }
        public IActionResult zsethashtable()
        {
            string key = "zsettest";
            string hashkey = "hashtest";
            var database = _redisCache.GetDatabase();
            if (database.KeyExists(key))
            {
                database.KeyDelete(key);
            }
            List<SortedSetEntry> SortedSetEntryentrylist = new List<SortedSetEntry>();
            List<HashEntry> entrylist = new List<HashEntry>();
            for (int i = 0; i < Testobj.Count; i++)
            {
                var itemstring = JsonConvert.SerializeObject(Testobj[i]);
                entrylist.Add(new HashEntry(Testobj[i].Id, itemstring));
                SortedSetEntryentrylist.Add(new SortedSetEntry(Testobj[i].Id, i));
            }
            var fromtime = DateTime.Now;
            var trans = database.CreateTransaction();
            database.SortedSetAdd(key, SortedSetEntryentrylist.ToArray());
            database.HashSet(hashkey, entrylist.ToArray());
            trans.Execute();
            var totime = DateTime.Now;
            var diff = totime.Subtract(fromtime);
            return Ok(diff);
        }
        public IActionResult zsethashtableget()
        {
            var fromtime = DateTime.Now;
            string key = "zsettest";
            string hashkey = "hashtest";
            var database = _redisCache.GetDatabase();
            var fieid= database.SortedSetRangeByRank(key,0,100);
            var result= database.HashGet(hashkey, fieid);
            var rr= result.ToString();
            var totime = DateTime.Now;
            var diff = totime.Subtract(fromtime);
            return Ok(diff);
        }
        public class testobj
        {
            public string Id { get; set; }
            public testobj(int i)
            {
                Id = i.ToString();
                AA = AB = AC = BA = BB = BC = CA = CB = CC = "testobj" + i;
            }
            public string AA { get; set; }
            public string AB { get; set; }
            public string AC { get; set; }
            public string BA { get; set; }
            public string BB { get; set; }
            public string BC { get; set; }
            public string CA { get; set; }
            public string CB { get; set; }
            public string CC { get; set; }
        }
    }
}
