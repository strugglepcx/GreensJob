using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Apworks;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.GreensJob.WebApi.Controllers;
using Glz.Infrastructure;
using Glz.Infrastructure.Caching;
using Newtonsoft.Json;

namespace Glz.GreensJob.WebApi.Filters
{
    /// <summary>
    /// 求职者
    /// </summary>
    public class AuthorizationSeekerAttribute : AuthorizationFilterAttribute
    {
        private readonly ICache _cache;

        /// <summary>
        /// 创建一个<see cref="AuthorizationSeekerAttribute"/>实例
        /// </summary>
        public AuthorizationSeekerAttribute()
        {
            _cache = new RedisCache();
        }

        /// <summary>
        /// 授权过程
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var apiBaseControllor = actionContext.ControllerContext.Controller as MobileBaseController;

            if (apiBaseControllor == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK,
                    ResultBaseExtend.CreateResult<ResultBase>(StatusCodes.Failure, "未能识别的api控制器！"));
                return;
            }

            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }
            var identityRequestParam = GetIdentityRequestParam(actionContext);
            if (identityRequestParam != null)
            {
                var cacheKey = Const.SeekerSessionCodeCacheKey + identityRequestParam.jsId;
                var userInfo = _cache.Get<MobileUserInfoModel>(cacheKey);
                if (userInfo != null)
                {
                    if (userInfo.sessionId == identityRequestParam.sessionId)
                    {
                        apiBaseControllor.UserInfo = userInfo;
                        // 刷新Session缓存
                        _cache.Set(cacheKey, userInfo, TimeSpan.FromHours(Const.SeekerSessionsLidingExpireTime));
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK,
                        ResultBaseExtend.CreateResult<ResultBase>(StatusCodes.Forbidden, "未授权，请重新登陆！"));
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK,
                    ResultBaseExtend.CreateResult<ResultBase>(StatusCodes.Timeout, "超时，请重新登陆！"));
                }
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK,
                ResultBaseExtend.CreateResult<ResultBase>(StatusCodes.InvalidParameter, "身份验证参数错误！"));
            }
        }

        private WeiXinIdentityRequestParam GetIdentityRequestParam(HttpActionContext actionContext)
        {
            var contentString = actionContext.Request.Content.ReadAsStringAsync().Result;

            if (actionContext.Request.Content.IsFormData())
            {
                var formData = HttpUtility.ParseQueryString(contentString);

                return new WeiXinIdentityRequestParam()
                {
                    openId = formData.Get("openId"),
                    jsId = formData.Get("jsId").GetIntOrDefault(0),
                    sessionId = formData.Get("sessionId")
                };
            }

            return JsonConvert.DeserializeObject<WeiXinIdentityRequestParam>(contentString);
            // 参数
            //actionContext.Request.Content = new StringContent(contentString, Encoding.UTF8, actionContext.Request.Content.Headers.ContentType.MediaType);
            //var result = new IdentityRequestParam();
            //var queryParams = actionContext.Request.GetQueryNameValuePairs();
            //var userIdParam = queryParams.FirstOrDefault(queryParam => queryParam.Key == "userId");
            //var sessionIdParam = queryParams.FirstOrDefault(queryParam => queryParam.Key == "sessionId");
            //result.userId = userIdParam.Value.GetIntOrDefault(0);
            //result.sessionId = sessionIdParam.Value;
        }
    }
}