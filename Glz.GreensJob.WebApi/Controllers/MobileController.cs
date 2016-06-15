using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.GreensJob.WebApi.Filters;
using Glz.GreensJob.WebApi.Models;
using Glz.GreensJob.WebApi.Models.RequestParams;
using Glz.Infrastructure;
using Glz.Infrastructure.Caching;

namespace Glz.GreensJob.WebApi.Controllers
{
    /// <summary>
    /// 手机端接口
    /// </summary>
    [AuthorizationSeeker]
    [RoutePrefix("api/mobile")]
    public class MobileController : MobileBaseController
    {
        private readonly ICollectService _collectService;
        private readonly IEnrollService _enrollService;
        private readonly IEnrollDetailService _enrollDetailService;
        private readonly IComplaintService _complaintService;
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IJobSeekerService _jobSeekerService;
        private readonly IFeedBackService _feedBackService;
        private readonly IResumeService _resumeService;
        private readonly IJobService _jobService;
        private readonly IProvinceService _provinceService;
        private readonly ISearchRecordService _searchRecord;
        private readonly ICache _cache;
        private readonly ICityService _cityService;
        private readonly IUniversityService _universityService;

        public MobileController(ICollectService collectService, IEnrollService enrollService,
            IEnrollDetailService enrollDetailService, IComplaintService complaintService,
            IVerificationCodeService verificationCodeService, IJobSeekerService jobSeekerService,
            IFeedBackService feedBackService, IResumeService resumeService, ICache cache,
            IProvinceService provinceService, IJobService jobService, ISearchRecordService searchRecord,
            ICityService cityService, IUniversityService universityService)
        {
            _collectService = collectService;
            _enrollService = enrollService;
            _enrollDetailService = enrollDetailService;
            _complaintService = complaintService;
            _verificationCodeService = verificationCodeService;
            _jobSeekerService = jobSeekerService;
            _feedBackService = feedBackService;
            _cache = cache;
            _provinceService = provinceService;
            _resumeService = resumeService;
            _jobService = jobService;
            _searchRecord = searchRecord;
            _cityService = cityService;
            _universityService = universityService;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("v1/getverifycode")]
        public ResultBase<VerificationCodeModel> GetVerifyCode([FromBody] GetVerifyCodeRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<VerificationCodeModel>>(StatusCodes.Failure, "无效参数");

            var data = _verificationCodeService.GetVerifyCode(requestParam);

            var result = CreateResult<ResultBase<VerificationCodeModel>>(StatusCodes.Success, "发送成功");
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 获取职位列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getJobs")]
        [AllowAnonymous]
        public ResultBase<PagedResultModel<GetJobsModel>> GetJobs([FromBody]GetJobsRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<PagedResultModel<GetJobsModel>>>(StatusCodes.Failure, "无效参数");
            var result = CreateResult<ResultBase<PagedResultModel<GetJobsModel>>>(StatusCodes.Success);
            result.Data = _jobService.GetJobs(requestParam);
            return result;
        }

        /// <summary>
        /// 获取职位列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/searchJobs")]
        [AllowAnonymous]
        public ResultBase<PagedResultModel<GetJobsModel>> SearchJobs([FromBody]SearchJobsRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<PagedResultModel<GetJobsModel>>>(StatusCodes.Failure, "无效参数");
            var result = CreateResult<ResultBase<PagedResultModel<GetJobsModel>>>(StatusCodes.Success);
            result.Data = _jobService.SearchJobs(requestParam);
            return result;
        }

        /// <summary>
        /// 用户收藏职位
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/favoriteJob")]
        public ResultBase FavoriteJob([FromBody]FavoriteJobRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Failure, "无效参数");

            _collectService.FavoriteJob(requestParam);

            return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Success, "收藏成功");
        }

        /// <summary>
        /// 用户移除收藏
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/cancelCollect")]
        public ResultBase CancelCollect([FromBody]CancelCollectRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase>(StatusCodes.Failure, "无效参数");

            _collectService.CancelCollect(requestParam);

            return CreateResult<ResultBase>(StatusCodes.Success, "取消成功");
        }

        /// <summary>
        /// 获取用户相关职位列表
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getEmployeeJobs")]
        public ResultBase<PagedResultModel<EmployeeJobInfo>> GetEmployeeJobs([FromBody]GetEmployeeJobsRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<PagedResultModel<EmployeeJobInfo>>>(StatusCodes.Failure, "无效参数");
            var data = _jobService.GetEmployeeJobs(requestParam);
            var result = CreateResult<ResultBase<PagedResultModel<EmployeeJobInfo>>>(StatusCodes.Success);
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 用户申请职位
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/applyJob")]
        public ResultBase ApplyJob([FromBody]ApplyJobRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Failure, "无效参数");

            _enrollService.ApplyJob(requestParam);

            return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Success, "报名成功");
        }

        /// <summary>
        /// 用户取消申请职位
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/cancelApply")]
        public ResultBase CancelApply([FromBody]CancelApplyRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Failure, "无效参数");

            _enrollService.CancelApply(requestParam);

            return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Success, "取消成功");
        }

        /// <summary>
        /// 用户修改申请职位
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/modifyApply")]
        public ResultBase ModifyApply([FromBody]ModifyApplyRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Failure, "无效参数");

            _enrollService.ModifyApply(requestParam);

            return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Success, "修改成功");
        }

        /// <summary>
        /// 用户确认申请职位
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/confirmApply")]
        public ResultBase ConfirmApply([FromBody]ConfirmApplyRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Failure, "无效参数");

            _enrollService.ConfirmApply(requestParam);

            return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Success, "确认成功");
        }

        ///// <summary>
        ///// 获取用户申请的职位列表
        ///// </summary>
        ///// <param name="appKey"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("v1/getApplyJobs")]
        //public OutJobModel getApplyJobs([FromBody]InEnrollModel appKey)
        //{
        //    if (appKey != null)
        //    {
        //        var list = _enrollService.GetObjectByPaged(appKey.userID, appKey.pageIndex);
        //    }
        //    return CreateResult<OutJobModel>(0, "无效参数");
        //}

        /// <summary>
        /// 投诉
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/commentJob")]
        [AllowAnonymous]
        public ResultBase CommentJob([FromBody]InComplaintModel appKey)
        {
            if (appKey != null)
            {
                var complaintObject = new ComplaintObject()
                {
                    jobID = appKey.jobID,
                    jobSeekerID = appKey.userID,
                    category = appKey.category,
                    content = appKey.content,
                    createDate = DateTime.Now
                };
                if (_complaintService.AddObject(complaintObject) > 0)
                    return CreateResult<ResultBase>(StatusCodes.Success, "感谢您的投诉及建议");
                else
                    return CreateResult<ResultBase>(StatusCodes.Failure, "投诉失败");
            }
            return CreateResult<ResultBase>(StatusCodes.Failure, "无效参数");
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/login")]
        [AllowAnonymous]
        public ResultBase<MobileUserInfoModel> Login([FromBody]LoginRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Failure, "无效参数");

            var userInfo = _jobSeekerService.Login(requestParam);

            var result = CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Success, "登录成功");
            result.Data = userInfo;
            return result;
        }

        /// <summary>
        /// 绑定手机号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/bindingMobile")]
        public ResultBase BindingMobile([FromBody]BindingMobileRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase>(StatusCodes.Failure, "无效参数");
            // 未能表示为能找到相关的求职者用户信息
            _jobSeekerService.BindingMobile(requestParam);
            // 修改成功则返回成功的结果
            return CreateResult<ResultBase>(StatusCodes.Success, "绑定成功");
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/register")]
        [AllowAnonymous]
        public ResultBase<MobileUserInfoModel> Register([FromBody]RegisterRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Failure, "无效参数");
            // 未能表示为能找到相关的求职者用户信息
            var userInfo = _jobSeekerService.Register(requestParam);
            // 修改成功则返回成功的结果
            var result = CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Success, "注册成功");
            result.Data = userInfo;
            return result;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getUserInfo")]
        public ResultBase<MobileUserInfoModel> GetUserInfo([FromBody]GetUserInfoRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Failure, "无效参数");

            var data = _jobSeekerService.GetUserInfo(requestParam);
            var result = CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Success);
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 意见反馈
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/feedBack")]
        [AllowAnonymous]
        public ResultBase feedBack([FromBody]InFeedBackModel appKey)
        {
            if (appKey != null)
            {
                if (_feedBackService.AddObject(new FeedBackObject()
                {
                    Category = appKey.category,
                    MemberID = appKey.memberID,
                    MemberCategory = appKey.memberCategory,
                    Contact = appKey.contact,
                    Content = appKey.content
                }) > 0)
                {
                    return CreateResult<ResultBase>(StatusCodes.Success, "已收到您的意见");
                }
                else
                    return CreateResult<ResultBase>(StatusCodes.Failure, "反馈失败");
            }
            return CreateResult<ResultBase>(StatusCodes.Failure, "无效参数");
        }

        /// <summary>
        /// 获取个人简历
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getResume")]
        public ResultBase<ResumeObject> GetResume([FromBody]GetResumeRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<ResumeObject>>(StatusCodes.Failure, "无效参数");
            var data = _jobSeekerService.GetResume(requestParam);
            var result = CreateResult<ResultBase<ResumeObject>>(StatusCodes.Success);
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 修改个人简历
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/setResume")]
        public ResultBase SetResume([FromBody]SetResumeRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Failure, "无效参数");

            _jobSeekerService.SetResume(requestParam);

            return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Success, "修改成功");
        }

        /// <summary>
        /// 获取OpenId
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getOpenId")]
        [NonAction]
        public ResultBase<string> GetOpenId([FromBody] GetOpenIdRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<string>>(StatusCodes.Failure, "无效参数");

            var cacheKey = Const.UserSessionCodeCacheKey + requestParam.sessionId;

            var userInfo = _cache.Get<MobileUserInfoModel>(cacheKey);
            if (userInfo != null)
            {
                var result = CreateResult<ResultBase<string>>(StatusCodes.Success);
                result.Data = userInfo.openId;
                return result;
            }
            return CreateResult<ResultBase<string>>(StatusCodes.Failure, "授权失败");
        }

        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getCities")]
        [AllowAnonymous]
        public ResultBase<IEnumerable<ProvinceObject>> GetCities([FromBody]GetCitiesRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<IEnumerable<ProvinceObject>>>(StatusCodes.Failure, "无效参数");
            var data = _provinceService.GetCities(requestParam);
            var result = CreateResult<ResultBase<IEnumerable<ProvinceObject>>>(StatusCodes.Success);
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 获取搜索记录列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/prefetchSearch")]
        [AllowAnonymous]
        public ResultBase<PagedResultModel<SearchRecordModel>> PrefetchSearch([FromBody]PrefetchSearchRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<PagedResultModel<SearchRecordModel>>>(StatusCodes.Failure, "无效参数");
            var data = _searchRecord.PrefetchSearch(requestParam);
            var result = CreateResult<ResultBase<PagedResultModel<SearchRecordModel>>>(StatusCodes.Success);
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 获取职位详情
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/GetJobDetail")]
        [AllowAnonymous]
        public ResultBase<Dto.JobModel> GetJobDetail([FromBody]GetJobDetailRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<Dto.JobModel>>(StatusCodes.Failure, "无效参数");
            var data = _jobService.GetJobDetail(requestParam);
            var result = CreateResult<ResultBase<Dto.JobModel>>(StatusCodes.Success);
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 设置钱袋子密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/accountPwdSet")]
        public ResultBase AccountPwdSet([FromBody]AccountPwdSetRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Failure, "无效参数");

            _jobSeekerService.AccountPwdSet(requestParam);

            return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Success, "设置成功");
        }

        /// <summary>
        /// 获取设置信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getConfiguration")]
        public ResultBase<ConfigurationModel> GetConfiguration([FromBody]GetConfigurationRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<ConfigurationModel>>(StatusCodes.Failure, "无效参数");

            var data = _jobSeekerService.GetConfiguration(requestParam);
            var result = CreateResult<ResultBase<ConfigurationModel>>(StatusCodes.Success);
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 配置设置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/configurationSet")]
        public ResultBase ConfigurationSet([FromBody]ConfigurationSetRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Failure, "无效参数");

            _jobSeekerService.ConfigurationSet(requestParam);

            return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Success, "设置成功");
        }

        /// <summary>
        /// 坐标获取地址
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getCityForCoordinate")]
        [AllowAnonymous]
        public ResultBase<CityModel> GetCityForCoordinate([FromBody]GetCityForCoordinateRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<CityModel>>(StatusCodes.Failure, "无效参数");

            var data = _cityService.GetCityForCoordinate(requestParam);
            var result = CreateResult<ResultBase<CityModel>>(StatusCodes.Success);
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 提现
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/transferMoneyOut")]
        public ResultBase TransferMoneyOut([FromBody]TransferMoneyOutRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Failure, "无效参数");

            _jobSeekerService.TransferMoneyOut(requestParam);

            return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Success, "提现成功，24小时内到账");
        }

        /// <summary>
        /// 账户明细
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/dspTransactionInfo")]
        public ResultBase<PagedResultModel<JobSeekerWalletActionLogModel>> DspTransactionInfo([FromBody]DspTransactionInfoRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<PagedResultModel<JobSeekerWalletActionLogModel>>>(StatusCodes.Failure, "无效参数");

            var data = _jobSeekerService.DspTransactionInfo(requestParam);

            var result = CreateResult<ResultBase<PagedResultModel<JobSeekerWalletActionLogModel>>>(StatusCodes.Success);
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 信息列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/dspMsgList")]
        public ResultBase<PagedResultModel<JobSeekerMessageModel>> DspMsgList([FromBody]DspMsgListRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<PagedResultModel<JobSeekerMessageModel>>>(StatusCodes.Failure, "无效参数");

            var data = _jobSeekerService.DspMsgList(requestParam);

            var result = CreateResult<ResultBase<PagedResultModel<JobSeekerMessageModel>>>(StatusCodes.Success);
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 获取学校信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getUniversitys")]
        [AllowAnonymous]
        public ResultBase<IEnumerable<UniversityObject>> GetUniversitys([FromBody]GetUniversitysRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<IEnumerable<UniversityObject>>>(StatusCodes.Failure, "无效参数");

            var data = _universityService.GetUniversitys(requestParam);

            var result = CreateResult<ResultBase<IEnumerable<UniversityObject>>>(StatusCodes.Success);
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 获取开放城市列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getOpenCities")]
        [AllowAnonymous]
        public ResultBase<IEnumerable<CityModel>> GetOpenCities([FromBody]GetOpenCitiesRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<IEnumerable<CityModel>>>(StatusCodes.Failure, "无效参数");
            var result = CreateResult<ResultBase<IEnumerable<CityModel>>>(StatusCodes.Success);
            result.Data = _cityService.GetOpenCities(requestParam);
            return result;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/retrievePassword")]
        [AllowAnonymous]
        public ResultBase RetrievePassword([FromBody]RetrievePasswordRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Failure, "无效参数");

            _jobSeekerService.RetrievePassword(requestParam);

            return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Success, "设置成功");
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/logout")]
        public ResultBase Logout(LogoutRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Failure, "无效参数");

            _jobSeekerService.Logout(requestParam);

            return CreateResult<ResultBase<MobileUserInfoModel>>(StatusCodes.Success, "注销成功");
        }
    }

}