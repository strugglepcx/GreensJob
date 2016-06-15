using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using AutoMapper;
using Glz.Infrastructure.Logging;
using Glz.Infrastructure.Logging.Models;
using Hangfire;
using Newtonsoft.Json;

namespace Glz.GreensJob.WebApi.Filters
{
    /// <summary>
    /// action日志过滤器
    /// </summary>
    public class ActionLoggingAttribute : ActionFilterAttribute
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        /// <summary>
        /// 创建一个日志过滤器
        /// </summary>
        public ActionLoggingAttribute()
        {
            //_logging = new HttpLogging();
        }

        /// <summary>
        /// Action执行之前
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _stopwatch.Restart();
            base.OnActionExecuting(actionContext);
        }

        /// <summary>
        /// Action执行之后
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var nowTime = DateTime.Now;
            var actionLogModel = new ActionLogModel
            {
                ControllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                ActionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName,
                ContentType = actionExecutedContext.Request.Content.Headers.ContentType.MediaType,
                CreateTime = nowTime,
                ExecuteTime = nowTime,
                RequestContent = JsonConvert.SerializeObject(actionExecutedContext.ActionContext.ActionArguments),
                RequestUrl = actionExecutedContext.Request.RequestUri.AbsoluteUri
            };
            _stopwatch.Stop();
            actionLogModel.ResultCode = actionExecutedContext.Response?.StatusCode.ToString() ?? "无";
            actionLogModel.ResultContent = actionExecutedContext.Response?.Content.ReadAsStringAsync().Result ?? "无";
            actionLogModel.Duration = (int)_stopwatch.ElapsedMilliseconds;
            BackgroundJob.Enqueue<ILogging>(x => x.LogAction(actionLogModel));
            //_logging.LogAction(_actionLogModel);
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}