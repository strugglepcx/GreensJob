using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Glz.Infrastructure;
using Newtonsoft.Json;

namespace Glz.GreensJob.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestPingpp()
        {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Result()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Charge(string channel)
        {
            var OrderID = "P" + DateTime.Now.ToString("yyyyMMddhhmmssfffff");
            var clientIP = Request.UserHostAddress;
            var chargeId = string.Empty;
            var charge = Pingxx.CreateInstance().CreateCharge(OrderID, 100, channel, "测试单", "测试测试");
            return Content(JsonConvert.SerializeObject(charge));
        }
    }
}