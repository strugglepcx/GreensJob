using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using Glz.GreensJob.Domain.Application;
using Glz.Infrastructure;
using Glz.Infrastructure.Logging;
using Glz.Infrastructure.Logging.Models;
using Hangfire;
using Newtonsoft.Json;
using Glz.Infrastructure.Locking;

namespace Glz.GreensJob.WebApi.Filters
{
    /// <summary>
    /// 异常处理过滤器
    /// </summary>
    public class ExceptionLoggingAttribute : ExceptionFilterAttribute
    {
        //private readonly ILogging _logging;

        /// <summary>
        /// 初始化一个新的<c>ExceptionLoggingAttribute</c>类型。
        /// </summary>
        public ExceptionLoggingAttribute()
        {
            //_logging = new HttpLogging();
        }
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var nowTime = DateTime.Now;

            var actionExceptionLog = new ActionExceptionLogModel
            {
                ActionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName,
                ContentType = actionExecutedContext.Request.Content.Headers.ContentType.MediaType,
                CreateTime = nowTime,
                RequestContent = JsonConvert.SerializeObject(actionExecutedContext.ActionContext.ActionArguments),
                RequestUrl = actionExecutedContext.Request.RequestUri.AbsoluteUri,
                ControllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                ExceptionMessage = actionExecutedContext.Exception.ToString(),
                ExceptionTraceStack = actionExecutedContext.Exception.StackTrace,
                ExceptionType = actionExecutedContext.Exception.GetType().Name
            };
            //_logging.LogException(actionExceptionLog);
            BackgroundJob.Enqueue<ILogging>(x => x.LogException(actionExceptionLog));

            if (actionExecutedContext.Exception is GreensJobException)
            {
                var greensJobException = actionExecutedContext.Exception as GreensJobException;
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK,
                            ResultBaseExtend.CreateResult<ResultBase>(greensJobException.Code, greensJobException.Message));
            }
            else if (actionExecutedContext.Exception is LockingJobException)
            {
                var greensJobException = actionExecutedContext.Exception as LockingJobException;
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK,
                            ResultBaseExtend.CreateResult<ResultBase>(StatusCodes.Failure, "操作职位过于频繁，请重试"));
            }
            else if (actionExecutedContext.Exception is GreensJobServiceErrorException)
            {
                var greensJobException = actionExecutedContext.Exception as GreensJobServiceErrorException;
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError,
                            ResultBaseExtend.CreateResult<ResultBase>(StatusCodes.InternalError, greensJobException.Message));
            }
            else
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK,
                    ResultBaseExtend.CreateResult<ResultBase>(StatusCodes.InternalError, actionExecutedContext.Exception.ToString()));

                //base.OnException(actionExecutedContext);
            }
        }
    }
}