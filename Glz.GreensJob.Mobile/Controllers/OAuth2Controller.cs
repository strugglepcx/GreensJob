using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;
using Senparc.Weixin;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.Helpers;

namespace Glz.GreensJob.Mobile.Controllers
{
    public class OAuth2Controller : Controller
    {
        //下面换成账号对应的信息，也可以放入web.config等地方方便配置和更换
        private readonly string appId = WebConfigurationManager.AppSettings["WeixinAppId"];
        private readonly string secret = WebConfigurationManager.AppSettings["WeixinAppSecret"];
        private readonly string host = WebConfigurationManager.AppSettings["Host"];
        private readonly string apiHost = WebConfigurationManager.AppSettings["ApiHost"];
        private readonly string mobileUserOpenIdCookiesKey = WebConfigurationManager.AppSettings["MobileUserOpenIdCookiesKey"];
        private const string AuthState = "greensjob";
        private const string homeUrl = "../gelin/home_gl.html";

        // GET: OAuth2
        public ActionResult Index(string returnUrl)
        {
            var oAuthUrl = OAuthApi.GetAuthorizeUrl(appId, $"{host}/OAuth2/UserInfoCallback?returnUrl={returnUrl ?? string.Empty}",
                AuthState, OAuthScope.snsapi_userinfo);
            return Redirect(oAuthUrl);
        }

        /// <summary>
        /// OAuthScope.snsapi_userinfo方式回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult UserInfoCallback(string code, string state, string returnUrl)
        {

            if (string.IsNullOrEmpty(code))
            {
                return Redirect(homeUrl);
            }

            if (state != AuthState)
            {
                return Redirect(homeUrl);
            }

            OAuthAccessTokenResult result;

            //通过，用code换取access_token
            try
            {
                result = OAuthApi.GetAccessToken(appId, secret, code);
            }
            catch (Exception ex)
            {
                return Redirect(homeUrl);
            }
            if (result.errcode != ReturnCode.请求成功)
            {
                return Redirect(homeUrl);
            }
            //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
            //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
            //Session["OAuthAccessTokenStartTime"] = DateTime.Now;
            //Session["OAuthAccessToken"] = result;
            //Session["OAuthState"] = true;
            //因为第一步选择的是OAuthScope.snsapi_userinfo，这里可以进一步获取用户详细信息

            //using (var proxy = new HttpClient())
            //{
            //    proxy.BaseAddress = new Uri(apiHost);
            //    var proxyResult = proxy.PostAsJsonAsync("/api/mobile/v1/login", new LoginRequestParam { openId = result.openid }).Result;
            //    if (proxyResult.StatusCode == HttpStatusCode.OK)
            //    {
            //        var resultData = proxyResult.Content.ReadAsAsync<ResultBase<MobileUserInfoModel>>().Result;
            //        if (resultData.code == StatusCodes.Success)
            //        {
            //            Response.SetCookie(new HttpCookie(Const.MobileUserSessionCodeCookiesKey, resultData.Data.sessionId)
            //            {
            //                Expires = DateTime.Now.AddHours(1)
            //            });
            //        }
            //    }
            //}
            Response.SetCookie(new HttpCookie(mobileUserOpenIdCookiesKey, result.openid)
            {
                Expires = DateTime.Now.AddDays(2)
            });
            try
            {
                var userInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);
                return Redirect($"{(string.IsNullOrEmpty(returnUrl) ? homeUrl : returnUrl)}?openid={result.openid}");
                //return RedirectToAction("Index", "Home");
            }
            catch (ErrorJsonResultException ex)
            {
                return Redirect(homeUrl);
            }
        }

        /// <summary>
        /// 获取jssdkUi包
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public ActionResult GetJsSdkUiPackage(string url)
        {
            var jssdkUiPackage = JSSDKHelper.GetJsSdkUiPackage(appId, secret, HttpUtility.HtmlDecode(url));
            return Json(jssdkUiPackage);
        }
    }
}