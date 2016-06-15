using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using Glz.GreensJob.Dto;

namespace Glz.GreensJob.WebApi.Controllers
{
    /// <summary>
    /// B端基础控制器
    /// </summary>
    public class BusinessBaseController : ApiBaseController
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public WebUserInfoModel UserInfo { get; set; }
        /// <summary>
        /// 控制器初始化
        /// </summary>
        /// <param name="controllerContext"></param>
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            UserInfo = new WebUserInfoModel { id = 0, CompanyID = 0 };
            base.Initialize(controllerContext);
        }
    }
}