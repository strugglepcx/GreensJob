using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Glz.GreensJob.Logs.Dto;
using Glz.GreensJob.Logs.IApplication;

namespace Glz.GreensJob.Logs.WebApi.Controllers
{
    [RoutePrefix("api/logs")]
    public class ActionLogController : ApiController
    {
        private readonly IActionLogService _actionLogService;

        public ActionLogController(IActionLogService actionLogService)
        {
            this._actionLogService = actionLogService;
        }

        [HttpPost]
        [Route("v1/logAction")]
        public IHttpActionResult LogAction(ActionLogModel actionLogModel)
        {
            if (!ModelState.IsValid) return Ok(new { state = 0, message = "缺少参数" });
            _actionLogService.LogAction(actionLogModel);
            return Ok(new { code = 1, message = "" });
        }

        [HttpGet]
        [Route("v1/getActionPageList")]
        public IHttpActionResult GetActionPageList(string actionName = "", int pageIndex = 1, int pageSize = 20)
        {
            if (!ModelState.IsValid) return Ok(new { state = 0, message = "缺少参数" });
            var data = _actionLogService.GetActionPageList(actionName, pageIndex, pageSize);
            return Ok(new { code = 1, Data = data, message = "" });
        }

        [HttpPost]
        [Route("v1/logException")]
        public IHttpActionResult LogException(ActionExceptionLogModel actionExceptionLogModel)
        {
            if (!ModelState.IsValid) return Ok(new { state = 0, message = "缺少参数" });
            _actionLogService.LogException(actionExceptionLogModel);
            return Ok(new { code = 1, message = "" });
        }

        [HttpGet]
        [Route("v1/getActionExceptionPageList")]
        public IHttpActionResult GetActionExceptionPageList(string actionName = "", int pageIndex = 1, int pageSize = 20)
        {
            if (!ModelState.IsValid) return Ok(new { state = 0, message = "缺少参数" });
            var data = _actionLogService.GetActionExceptionPageList(actionName, pageIndex, pageSize);
            return Ok(new { code = 1, Data = data, message = "" });
        }
    }
}
