using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;

namespace Glz.GreensJob.Mobile.Controllers
{
    public class HomeController : Controller
    {
        private readonly string apiHost = WebConfigurationManager.AppSettings["ApiHost"];
        public ActionResult Index()
        {
            //Response.SetCookie(new HttpCookie(Const.MobileUserSessionCodeCookiesKey, "123123123123123123"));

            //using (var proxy = new HttpClient())
            //{
            //    proxy.BaseAddress = new Uri(apiHost);
            //    var proxyResult = proxy.PostAsJsonAsync("/api/mobile/v1/login", new LoginRequestParam { openId = "sdfsdfsdf" }).Result;
            //    if (proxyResult.StatusCode == HttpStatusCode.OK)
            //    {
            //        var resultData = proxyResult.Content.ReadAsAsync<ResultBase<MobileUserInfoModel>>().Result;
            //        if (resultData.code == 1)
            //        {
            //            Response.SetCookie(new HttpCookie(Const.MobileUserSessionCodeCookiesKey, resultData.Data.sessionId));
            //        }
            //        else
            //        {
            //            ViewBag.Message = resultData.code.ToString();
            //        }
            //    }
            //    else
            //    {
            //        ViewBag.Message = proxyResult.StatusCode.ToString();
            //    }
            //}

            return View();
        }
    }
}