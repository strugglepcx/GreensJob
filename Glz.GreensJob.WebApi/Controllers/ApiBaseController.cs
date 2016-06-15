using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Glz.GreensJob.Dto;
using Glz.GreensJob.WebApi.Filters;
using Glz.GreensJob.WebApi.Models;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Controllers
{
    /// <summary>
    /// api基础控制器
    /// </summary>
    [ExceptionLogging]
    [ActionLogging]
    public class ApiBaseController : ApiController
    {
        /// <summary>
        /// 创建OutCode实例对象
        /// </summary>
        /// <typeparam Name="T">必须继承自OutCode</typeparam>
        /// <param Name="code">返回码,错误信息会根据Resource中的配置自动填充</param>
        /// <returns>OutCode实例对象</returns>
        protected T CreateResult<T>(StatusCodes code)
            where T : ResultBase, new()
        {
            return ResultBaseExtend.CreateResult<T>(code);
        }

        /// <summary>
        /// 创建OutCode实例对象
        /// </summary>
        /// <typeparam Name="T">必须继承自OutCode</typeparam>
        /// <param Name="code">返回码</param>
        /// <param Name="message">返回消息</param>
        /// <returns>OutCode实例对象</returns>
        protected T CreateResult<T>(StatusCodes code, string message)
            where T : ResultBase, new()
        {
            return ResultBaseExtend.CreateResult<T>(code, message);
        }
    }
}
