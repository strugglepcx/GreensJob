using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.GreensJob.WebApi.Models.RequestParams;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Controllers
{
    /// <summary>
    /// 财务系统控制器
    /// </summary>
    [RoutePrefix("api/finance")]
    public class FinanceController : ApiBaseController
    {
        private readonly IEnrollPayService _enrollPayService;

        /// <summary>
        /// 创建一个FinanceController的控制器
        /// </summary>
        /// <param name="enrollPayService"></param>
        public FinanceController(IEnrollPayService enrollPayService)
        {
            _enrollPayService = enrollPayService;
        }

        /// <summary>
        /// 获取申请列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("v1/getExtractApplys")]
        public ResultBase<IEnumerable<ExtractApplyModel>> GetExtractApplys([FromBody] GetExtractApplysRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<IEnumerable<ExtractApplyModel>>>(StatusCodes.Failure, "无效参数");

            var data = _enrollPayService.GetExtractApplys(requestParam);

            var result = CreateResult<ResultBase<IEnumerable<ExtractApplyModel>>>(StatusCodes.Success);
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 完成申请支付
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("v1/completeExtractApplys")]
        public ResultBase CompleteExtractApplys([FromBody] CompleteExtractApplysRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase>(StatusCodes.Failure, "无效参数");

            _enrollPayService.CompleteExtractApplys(requestParam);

            return CreateResult<ResultBase>(StatusCodes.Success, "操作成功");
        }
    }
}
