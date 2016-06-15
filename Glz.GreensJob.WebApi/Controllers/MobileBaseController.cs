using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Glz.GreensJob.Dto;

namespace Glz.GreensJob.WebApi.Controllers
{
    /// <summary>
    /// C端基础控制器
    /// </summary>
    public class MobileBaseController : ApiBaseController
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public MobileUserInfoModel UserInfo { get; set; }
        /// <summary>
        /// 控制器初始化
        /// </summary>
        /// <param name="controllerContext"></param>
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            UserInfo = new MobileUserInfoModel { id = 0, mobile = "", openId = "" };
            base.Initialize(controllerContext);
        }
    }
}
