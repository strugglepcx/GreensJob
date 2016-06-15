using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using Apworks;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.GreensJob.WebApi.Filters;
using Glz.GreensJob.WebApi.Models;
using Glz.GreensJob.WebApi.Models.RequestParams;
using Glz.Infrastructure;
using Glz.Infrastructure.Sms;
using Glz.Infrastructure.Caching;
using Aspose.Cells;
using System.IO;
using System.Net.Http.Headers;
using System.Data;
using Glz.GreensJob.Domain.Application;

namespace Glz.GreensJob.WebApi.Controllers
{
    /// <summary>
    /// B端控制器
    /// </summary>
    [AuthorizationUser]
    [RoutePrefix("api/business")]
    public class BusinessController : BusinessBaseController
    {
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly ICompanyService _companyService;
        private readonly IAgencyRecruitJobService _agencyRecruitJobService;
        private readonly IJobGroupService _jobGroupService;
        private readonly IJobService _jobService;
        private readonly IPublisherService _publisher;
        private readonly IEnrollService _enroll;
        private readonly IEnrollPayService _enrollPay;
        private readonly IJobDraftService _jobDraftService;
        private readonly IJobSeekerService _jobSeekerService;
        private readonly IJobRecruitDetailService _jobRecruitDetailRepository;
        private readonly ICache _cache;

        public BusinessController(IVerificationCodeService verificationCodeService,
            ICompanyService companyService, IAgencyRecruitJobService agencyRecruitJobService, IJobService jobService,
            IJobGroupService jobGroupService, IPublisherService publisher, ICache cache, IEnrollService enroll,
            IEnrollPayService enrollPay, IJobDraftService jobDraftService, IJobSeekerService jobSeekerService, IJobRecruitDetailService jobRecruitDetailRepository)
        {
            _verificationCodeService = verificationCodeService;
            _companyService = companyService;
            _agencyRecruitJobService = agencyRecruitJobService;
            _jobService = jobService;
            _jobGroupService = jobGroupService;
            _publisher = publisher;
            _cache = cache;
            _enroll = enroll;
            _enrollPay = enrollPay;
            _jobDraftService = jobDraftService;
            _jobSeekerService = jobSeekerService;
            _jobRecruitDetailRepository = jobRecruitDetailRepository;
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
        /// 会员登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("v1/loginAction")]
        public ResultBase<WebUserInfoModel> LoginAction([FromBody] LoginActionRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<WebUserInfoModel>>(StatusCodes.Failure, "无效参数");

            var userInfo = _publisher.LoginAction(requestParam);

            var result = CreateResult<ResultBase<WebUserInfoModel>>(StatusCodes.Success, "登录成功");
            result.Data = userInfo;
            return result;
        }

        /// <summary>
        /// 会员注册
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("v1/registerAction")]
        public ResultBase RegisterAction([FromBody] RegisterActionRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase>(StatusCodes.Failure, "无效参数");

            var cacheKey = Const.ValidateCodeCacheKey + "2_" + requestParam.userMobileNumber;
            // 获取验证码
            var verificationCodeModel =
                _cache.Get<VerificationCodeModel>(cacheKey);

            if (verificationCodeModel == null) return CreateResult<ResultBase>(StatusCodes.Failure, "请重新获取验证码");

            if (verificationCodeModel.verificationCode != requestParam.verificationCode) return CreateResult<ResultBase>(StatusCodes.Failure, "验证码错误");

            #region 发布者（B端用户）

            // 将手机号带入数据库进行查询，如果返回的发布者对象为空，表示此手机号未被使用，否则提示用户手机号被占用
            var obj = _publisher.GetObjectByMobile(requestParam.userMobileNumber);
            if (obj != null) return CreateResult<ResultBase>(StatusCodes.Failure, "手机号被占用");

            // 执行新增发布者方法，返回的是发布者编号
            var id = _publisher.AddObject(new PublisherObject()
            {
                companyID = 0,
                isAdmin = true,
                mobile = requestParam.userMobileNumber,
                password = DES.MD5Encode(requestParam.password),
                name = requestParam.userMobileNumber,
                PublisherRight = new PublisherRightObject()
                {
                    AddUser = true,
                    DeleteUser = true,
                    Finicial = true,
                    ImportEmployee = true,
                    ModifyUser = true,
                    ReleaseJob = true
                }
            });
            if (id <= 0) return CreateResult<ResultBase>(StatusCodes.Failure, "注册失败");
            _cache.Remove(cacheKey);
            // 创建输出对象
            var outCode = CreateResult<ResultBase>(StatusCodes.Success, "注册成功");
            //// 重新读取一次 发布者 信息
            //obj = _publisher.GetObjectByID(id);
            //// 登录用户对象集合
            //List<MemberLoginData> list = new List<MemberLoginData>();
            //list.Add(new MemberLoginData()
            //{
            //    id = obj.id,
            //    isAdmin = obj.isAdmin,
            //    mobile = obj.mobile,
            //    name = obj.name,
            //    lastLoginDate = obj.lastLoginDate
            //});
            //outCode.Data = list;
            return outCode;

            #endregion
        }
        /*
        /// <summary>
        /// 分配用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/admeasureUser")]
        public ResultBase admeasureUser([FromBody]InMemberModel appKey)
        {
            if (appKey != null)
            {
                // 通过 userID 验证 操作当前功能的用户是否具备admin权限
                if (_publisher.IsAdmin(appKey.userID))
                {
                    // 判断手机号的唯一性
                    if (_publisher.GetObjectByMobile(appKey.mobile) != null)
                    {
                        if (_publisher.AddObject(new PublisherObject()
                        {
                            name = appKey.name,
                            companyID = appKey.companyID,
                            mobile = appKey.mobile,
                            password = DES.MD5Encode("123456"),
                            isAdmin = false,
                            createDate = Convert.ToDateTime("1990-01-01"),
                            lastLoginDate = Convert.ToDateTime("1990-01-01")
                        }) > 0)
                        {
                            return CreateResult<ResultBase>(1, "创建成功");
                        }
                        else
                        {
                            return CreateResult<ResultBase>(0, "创建失败");
                        }
                    }
                    else
                    {
                        return CreateResult<ResultBase>(0, "手机号已被占用");
                    }
                }
                else
                {
                    return CreateResult<ResultBase>(0, "权限不足");
                }
            }
            return CreateResult<ResultBase>(0, "无效参数");
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/removeUser")]
        public ResultBase removeUser([FromBody]InMemberModel appKey)
        {
            if (appKey != null)
            {
                if (_publisher.IsAdmin(appKey.userID))
                {
                    if (_publisher.RemoveObject(appKey.id) > 0)
                    {
                        return CreateResult<ResultBase>(1, "删除成功");
                    }
                    else
                        return CreateResult<ResultBase>(0, "删除失败");
                }
                else
                    return CreateResult<ResultBase>(0, "权限不足");
            }
            return CreateResult<ResultBase>(0, "无效参数");
        }
        */
        /// <summary>
        /// 修改密码/重置密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/updatePassword")]
        public ResultBase updatePassword([FromBody]InMembeLoginModel appKey)
        {
            if (appKey != null)
            {
                var obj = _publisher.GetObjectByID(appKey.userID);
                if (obj != null)
                {
                    // 设置新密码
                    obj.password = DES.MD5Encode(appKey.userPassword);
                    // 修改该用户的信息
                    if (_publisher.UpdateObject(obj) > 0)
                    {
                        return CreateResult<ResultBase>(StatusCodes.Success, "重置成功");
                    }
                    else
                        return CreateResult<ResultBase>(StatusCodes.Failure, "重置失败");
                }
                else
                    return CreateResult<ResultBase>(StatusCodes.Failure, "未找到编辑的用户");
            }
            return CreateResult<ResultBase>(StatusCodes.Failure, "无效参数");
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("v1/resetPasswordAction")]
        public ResultBase resetPasswordAction([FromBody]InMembeLoginModel appKey)
        {
            if (appKey != null)
            {
                // 获取验证码
                //var results = _verificationCodeService.GetObjectByMobile(appKey.userMobileNumber);
                var loginCacheKey = Const.ValidateCodeCacheKey + "2_" + appKey.userMobileNumber;
                // 获取验证码
                var verificationCodeModel =
                    _cache.Get<VerificationCodeModel>(loginCacheKey);

                if (verificationCodeModel == null)
                    throw new GreensJobException(0, "请重新获取验证码");

                if (verificationCodeModel.verificationCode != appKey.verificationCode)
                    throw new GreensJobException(0, "验证码错误");

                PublisherObject obj = _publisher.GetObjectByMobile(appKey.userMobileNumber);
                if (obj != null)
                {
                    obj.password = DES.MD5Encode(appKey.userPassword);
                    if (_publisher.UpdateObject(obj) > 0)
                    {
                        return CreateResult<ResultBase<OutMemberLoginModel>>(StatusCodes.Success, "重置成功");
                    }
                }
                else
                    return CreateResult<ResultBase<OutMemberLoginModel>>(StatusCodes.Failure, "无效用户");

            }
            return CreateResult<ResultBase<OutMemberLoginModel>>(StatusCodes.Failure, "无效参数");
        }

        /// <summary>
        /// 创建企业
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/putCompany")]
        public ResultBase<CompanyModel> putCompany([FromBody]CompanyActionRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<CompanyModel>>(StatusCodes.Failure, "无效参数");
            var id = _companyService.PutCompany(requestParam);
            if (id > 0)
            {
                var outCode = CreateResult<ResultBase<CompanyModel>>(StatusCodes.Success, "提交成功");
                var model = new CompanyModel()
                {
                    companyID = id,
                    cityID = requestParam.cityID,
                    companyName = requestParam.companyName,
                    companyImage = requestParam.companyImage,
                    companyAddr = requestParam.companyAddr,
                    companyIntroduce = requestParam.companyIntroduce,
                    companyContact = requestParam.companyContact,
                    companyTel = requestParam.companyTel,
                    status = requestParam.status
                };
                outCode.Data = model;
                return outCode;
            }
            else if (id == 0)
                return CreateResult<ResultBase<CompanyModel>>(StatusCodes.Failure, "提交失败");
            else
                return CreateResult<ResultBase<CompanyModel>>(StatusCodes.InvalidParameter, "无效用户");

        }

        /// <summary>
        /// 获取企业信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getCompanyInfo")]
        public OutCompanyModel getCompanyInfo([FromBody]InCompanyModel appKey)
        {
            if (appKey != null)
            {
                CompanyObject obj = _companyService.GetObjectByID(appKey.companyID);
                List<CompanyModel> list = new List<CompanyModel>();
                list.Add(new CompanyModel()
                {
                    companyID = obj.id,
                    companyName = obj.name,
                    companyImage = obj.image,
                    companyAddr = obj.addr,
                    companyIntroduce = obj.introduce,
                    cityID = obj.cityID,
                    cityName = obj.cityName,
                    status = obj.status
                });
                var outCode = CreateResult<OutCompanyModel>(StatusCodes.Success, "获取成功");
                outCode.Data = list;
                return outCode;
            }
            return CreateResult<OutCompanyModel>(StatusCodes.Failure, "无效参数");
        }

        /// <summary>
        /// 新增代招职位
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/recruitAgencyAction")]
        public ResultBase recruitAgencyAction([FromBody]InJobModel appKey)
        {
            if (appKey != null)
            {
                if (_agencyRecruitJobService.AddObject(new AgencyRecruitJobObject()
                {
                    name = appKey.name,
                    contact = appKey.contact,
                    phone = appKey.phone,
                    recruitNum = appKey.recruitNum,
                    payUnit = appKey.payUnitID,
                    salary = appKey.salary,
                    addr = appKey.addr,
                    status = 0,
                    startDate = appKey.startDate,
                    endDate = appKey.endDate
                }) > 0)
                {
                    return CreateResult<ResultBase>(StatusCodes.Success, "代招信息已经成功提交，格林会尽快和您联系");
                }
                else
                {
                    return CreateResult<ResultBase>(StatusCodes.Failure, "后台操作失败");
                }
            }
            return CreateResult<ResultBase>(StatusCodes.Failure, "无效参数");
        }

        /// <summary>
        /// 发布职位
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/releaseJob")]
        public ResultBase releaseJob([FromBody]ReleaseJobRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase>(StatusCodes.Failure, "无效参数");

            _jobService.ReleaseJob(requestParam);
            return CreateResult<ResultBase>(StatusCodes.Success, "发布成功");
        }


        ///// <summary>
        ///// 获取职位详情
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("v1/getJobInfo")]
        //public OutJobModel getJobInfo([FromBody]InJobModel appKey)
        //{
        //    if (appKey != null)
        //    {
        //        JobObject obj = _jobService.GetObjectByID(appKey.id);
        //        if (obj != null)
        //        {
        //            var outCode = CreateResult<OutJobModel>(1, "获取成功");
        //            List<JobModel> data = new List<JobModel>();
        //            data.Add(new JobModel()
        //            {
        //                id = obj.id,
        //                name = obj.name,
        //                jobCategoryID = obj.jobCategoryID,
        //                jobCategoryName = obj.jobCategoryName,
        //                jobClassifyID = obj.jobClassifyID,
        //                jobClassifyName = obj.jobClassifyName,
        //                jobSchduleID = obj.jobSchduleID,
        //                jobSchduleName = obj.jobSchduleName,
        //                publisherID = obj.publisherID,
        //                publisherName = obj.publisherName,
        //                publisherMobile = obj.publisherMobile,
        //                payCategoryID = obj.payCategoryID,
        //                payCategoryName = obj.payCategoryName,
        //                payUnitID = obj.payUnitID,
        //                payUnitName = obj.payUnitName,
        //                genderLimit = obj.genderLimit,
        //                heightLimit = obj.heightLimit,
        //                autoTimeShare = obj.autoTimeShare,
        //                erollMethod = obj.erollMethod,
        //                recruitNum = obj.recruitNum,
        //                salary = obj.salary,
        //                urgent = obj.urgent,
        //                healthCertificate = obj.healthCertificate,
        //                interview = obj.interview,
        //                interviewPlace = obj.interviewPlace,
        //                employer = obj.employer,
        //                groupID = obj.groupID,
        //                addr = new Address()
        //                {
        //                    addr = obj.addr,
        //                    lng = obj.lng,
        //                    lat = obj.lat
        //                },
        //                content = obj.content,
        //                startDate = obj.startDate,
        //                endDate = obj.endDate,
        //                contact = obj.contactMan,
        //                phone = obj.mobileNumber,
        //                gatheringPlace = obj.gatheringPlace,
        //                releaseDate = obj.releaseDate,
        //                createDate = obj.createDate,
        //                status = obj.status
        //            });
        //            outCode.Data = data;
        //            return outCode;
        //        }
        //        else
        //            return CreateResult<OutJobModel>(0, "无效职位");
        //    }
        //    return CreateResult<OutJobModel>(0, "无效参数");
        //}

        ///// <summary>
        ///// 删除职位
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("v1/deleteJob")]
        //public ResultBase deleteJob([FromBody]InJobModel appKey)
        //{
        //    if (appKey != null)
        //    {
        //        if (_jobService.RemoveObject(appKey.id) > 0)
        //        {
        //            return CreateResult<ResultBase>(1, "删除成功");
        //        }
        //        else
        //        {
        //            return CreateResult<ResultBase>(0, "删除失败");
        //        }
        //    }
        //    return CreateResult<ResultBase>(0, "无效参数");
        //}


        /// <summary>
        /// 刷新职位
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/refreshJob")]
        public ResultBase RefreshJob([FromBody] InRefreshJobModel appKey)
        {
            try
            {
                int result = _jobGroupService.RefreshJob(appKey.jobGroupID);
                if (result == 1)
                    return CreateResult<ResultBase>(StatusCodes.Success, "");
                else
                    return CreateResult<ResultBase>(StatusCodes.Failure, "后台操作失败，请重试");
            }
            catch (Exception e)
            {
                return CreateResult<ResultBase>(StatusCodes.Failure, "后台操作失败，请重试");
            }
        }

        /// <summary>
        /// 录用
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/employ")]
        public ResultBase Employ([FromBody] EmployRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase>(StatusCodes.Failure, "无效参数");

            _enroll.Employ(requestParam);

            return CreateResult<ResultBase>(StatusCodes.Success, "录用成功");
        }

        /// <summary>
        /// 不录用
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/unemploy")]
        public ResultBase UnEmploy([FromBody] EmployRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase>(StatusCodes.Failure, "无效参数");

            _enroll.UnEmploy(requestParam);

            return CreateResult<ResultBase>(StatusCodes.Success, "取消录用成功");
        }

        /// <summary>
        /// 获取工作详情
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getJobDetail")]
        public ResultBase<JobGroupObject> GetJobDetail([FromBody]GetJobGroupDetailRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<JobGroupObject>>(StatusCodes.Failure, "无效参数");
            var result = CreateResult<ResultBase<JobGroupObject>>(StatusCodes.Success);
            result.Data = _jobService.GetJobGroupDetail(requestParam);
            return result;
        }

        /// <summary>
        /// 获取工作列表
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getJobInfo")]
        public ResultBase<PagedResultModel<JobInfo>> GetJobInfo([FromBody]GetJobInfoRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<PagedResultModel<JobInfo>>>(StatusCodes.Failure, "无效参数");
            var result = CreateResult<ResultBase<PagedResultModel<JobInfo>>>(StatusCodes.Success);
            result.Data = _jobService.GetJobInfo(requestParam);
            return result;
        }

        /// <summary>
        /// 编辑职位
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/editJobInfo")]
        public ResultBase EditJobInfo([FromBody]EditJobInfoRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase>(StatusCodes.Failure, "无效参数");

            _jobService.EditJobInfo(requestParam);
            return CreateResult<ResultBase>(StatusCodes.Success, "修改成功");
        }

        /// <summary>
        /// 删除工作
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [Route("v1/deleteJob")]
        [HttpPost]
        public ResultBase deleteJob([FromBody] InRefreshJobModel appKey)
        {
            try
            {
                int result = _jobGroupService.DeleteJob(appKey.jobGroupID);
                if (result == 1)
                    return CreateResult<ResultBase>(StatusCodes.Success, "");
                else
                    return CreateResult<ResultBase>(StatusCodes.Failure, "后台操作失败，请重试");
            }
            catch (Exception e)
            {
                return CreateResult<ResultBase>(StatusCodes.Failure, "后台操作失败，请重试");
            }
        }

        /// <summary>
        /// 申请者列表
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [Route("v1/onlineApplicants")]
        [HttpPost]
        public OutOnLineApplicantModel onlineApplicants([FromBody] InOnLineApplicantModel appKey)
        {
            try
            {
                var result = CreateResult<OutOnLineApplicantModel>(StatusCodes.Success, "");
                var list = _enroll.GetList(appKey.jobGroupID, appKey.keyword, appKey.PageIndex, appKey.PageSize);

                result.employerList = list.Data.Select(input => new Employer
                {
                    userID = input.jobSeekerID,
                    enrollID = input.id,
                    employerName = input.JobSeeker.nickName,
                    Experienced = input.experienced,
                    workDays = input.EnrollDetails.Count,
                    startDate = input.EnrollDetails.Min(x => x.date).ToString("yyyy-MM-dd"),
                    endDate = input.EnrollDetails.Max(x => x.date).ToString("yyyy-MM-dd"),
                    enrollDates = input.EnrollDetails.Select(x => x.date),
                    employStatus = input.state,
                    jobAddress = input.name,
                    phoneNum = input.JobSeeker.mobile,
                    jobId = input.jobID
                }).ToList();

                result.PageNumber = list.PageNumber;
                result.PageSize = list.PageSize;
                result.TotalPages = list.TotalPages;
                result.TotalRecords = list.TotalRecords;

                return result;

            }
            catch (Exception e)
            {
                return CreateResult<OutOnLineApplicantModel>(StatusCodes.Failure, "后台操作失败，请重试");
            }
        }

        /// <summary>
        /// 录用者列表
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [Route("v1/employeeInfo")]
        [HttpPost]
        public OutEmployeeInfoModele employeeInfo([FromBody] InEmplyeeInfoModel appKey)
        {
            try
            {
                var result = CreateResult<OutEmployeeInfoModele>(StatusCodes.Success, "");
                var list = _enroll.GetEmployeeInfoList(appKey.jobGroupID, appKey.showMethod, appKey.PageIndex, appKey.PageSize);
                result.employerList = list.Data.Select(input => new EmployeeInfo
                {
                    userID = input.jobSeekerID,
                    EnrollID = input.id,
                    continousDays = input.EnrollDetails.Count,
                    employeeName = input.JobSeeker.nickName,
                    employeeMobileNumber = input.mobile,
                    employeeState = input.state,
                    enrollMethod = input.method,
                    employData = input.EnrollDetails.Select(x => new EmployeeDate()
                    {
                        startDate = x.start,
                        endDate = x.end
                    }).ToList(),
                    EnrollDetail = input.EnrollDetails,
                    jobName = input.name,
                    jobId = input.jobID
                }).ToList();

                result.PageNumber = list.PageNumber;
                result.PageSize = list.PageSize;
                result.TotalPages = list.TotalPages;
                result.TotalRecords = list.TotalRecords;

                return result;

            }
            catch (Exception e)
            {
                return CreateResult<OutEmployeeInfoModele>(StatusCodes.Failure, "后台操作失败，请重试");
            }
        }

        /// <summary>
        /// 导出录用者列表
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [Route("v1/exportEmployeeToXls")]
        [HttpPost]
        public string ExportEmployeeToXls([FromBody] InEmplyeeInfoModel appKey)
        {
            var list = _enroll.GetEmployeeInfoList(appKey.jobGroupID, appKey.showMethod, appKey.PageIndex, appKey.PageSize);
            List<dynamic> li = new List<dynamic>();
            for (int i = 0; i < list.Data.Count; i++)
            {
                for (int j = 0; j < list.Data[i].EnrollDetails.Count; j++)
                {
                    li.Add(new
                    {
                        jobName = list.Data[i].Job.name,
                        employeeName = list.Data[i].JobSeeker.nickName,
                        employeeMobileNumber = list.Data[i].mobile,
                        employeeState = list.Data[i].state == 1 ? "报名" : (list.Data[i].state == 5 ? "录用未确认" : (list.Data[i].state == 10 ? "录用" : (list.Data[i].state == 20 ? "付款" : "取消"))),
                        startDate = list.Data[i].EnrollDetails[j].start,
                        endDate = list.Data[i].EnrollDetails[j].end
                    });
                }
            }
            string[] colNames = new string[] { "职位名称", "求职者姓名", "手机号（防重名）", "录用状态", "录用起始日期", "录用结束日期", "备注" };
            var path = ReportingTools.ExportXls(li, colNames);

            return "temp/" + path;
        }

        /// <summary>
        /// 导出申请者列表
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [Route("v1/exportOnlineApplicants")]
        [HttpPost]
        public string ExportOnlineApplicants([FromBody] InOnLineApplicantModel appKey)
        {
            var list = _enroll.GetList(appKey.jobGroupID, appKey.keyword, appKey.PageIndex, appKey.PageSize);
            List<dynamic> li = new List<dynamic>();
            for (int i = 0; i < list.Data.Count; i++)
            {
                li.Add(new
                {
                    employerName = list.Data[i].JobSeeker.nickName,
                    Experienced = list.Data[i].experienced ? "有经验" : "无经验"
                });
            }
            string[] colNames = new string[] { "求职者姓名", "是否有经验" };
            var path = ReportingTools.ExportXls(li, colNames);

            return "temp/" + path;
        }

        /*
        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [Route("v1/ReCharge")]
        [HttpPost]
        public ResultBase ReCharge([FromBody] WalletRequestParam appKey)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase>(StatusCodes.Error, "无效参数");
            if (_enrollPay.AddObject(appKey) > 0)
                return CreateResult<ResultBase>(StatusCodes.Success, "充值成功");
            else
                return CreateResult<ResultBase>(0, "充值失败");
        }
        */

        /// <summary>
        /// 导入支付明细
        /// </summary>
        /// <returns></returns>
        [Route("v1/ImportPayDetail/{companyId}/{userId}/{jobGroupId}")]
        [HttpPost]
        [AllowAnonymous]
        public ResultBase<PagedResult<EnrollPayDetailModel>> ImportPayDetail(int companyId, int userId, int jobGroupId)
        {
            var root = HttpContext.Current.Server.MapPath("~/upload");
            #region 上传文件
            string fileName = string.Empty;
            //获取根路径
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                string extendName = ".xlsx";
                if (HttpContext.Current.Request.Files[0].ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    extendName = ".xlsx";
                else if (HttpContext.Current.Request.Files[0].ContentType == "application/vnd.ms-excel")
                    extendName = ".xls";

                fileName = root + "\\" + Guid.NewGuid().ToString().Replace("-", "") + extendName;
                HttpContext.Current.Request.Files[0].SaveAs(fileName);
            }
            #endregion
            // 如果 fileName 不为空，则表示上传成功；
            // 暂未通过try catch抓异常进行判断
            //if (!string.IsNullOrEmpty(fileName))
            //{

            //    if (companyId > 0 && jobId > 0 && userId > 0 && importDatas.Any())
            //    {
            //        var result = CreateResult<ResultBase<PagedResult<EnrollPayDetailObject>>>(StatusCodes.Success);
            //        result.Data = _enrollPayDetail.ImportDetail(importDatas, companyId, userId, jobId);
            //        return result;
            //    }
            //}
            _enrollPay.ImportDetail(new ImportPayDetailRequestParam
            {
                FilePath = fileName,
                jobGroupId = jobGroupId

            });
            return CreateResult<ResultBase<PagedResult<EnrollPayDetailModel>>>(StatusCodes.Failure, "");
        }

        /// <summary>
        /// 导出支付明细Execl结构
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/exportExecl")]
        public string ExportExecl([FromBody]IdentityRequestParam requestParam)
        {
            if (!ModelState.IsValid) return string.Empty;
            var jobs = _jobService.GetJobInfo(new GetJobInfoRequestParam()
            {
                companyId = requestParam.companyId,
                sessionId = requestParam.sessionId,
                userId = requestParam.userId,
                Status = 1,
                pageSize = 20,
                pageIndex = 1
            });
            string servepath = HttpContext.Current.Server.MapPath("~/upload/");
            string fileName = Guid.NewGuid().ToString();
            string path = servepath + fileName + ".xls";
            if (jobs.Data.Count > 0)
            {
                foreach (JobInfo obj in jobs.Data)
                {
                    DataTable table = new DataTable(obj.jobName + "_" + obj.jobGroupID);
                    table.Columns.Add("序号");
                    table.Columns.Add("姓名");
                    table.Columns.Add("手机号");
                    table.Columns.Add("收款账户");
                    table.Columns.Add("支付金额");
                    DataRow row = table.NewRow();
                    row[0] = row[1] = row[2] = row[3] = row[4] = "";
                    table.Rows.Add(row);
                    if (!ExcelUtls.DataTable2Sheet(servepath + fileName + ".xls", table))
                        return string.Empty;
                }
            }
            else
                path = string.Empty;
            return path;
        }

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        [Route("v1/payment")]
        [HttpPost]
        public ResultBase<string> Payment([FromBody]PaymentRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<string>>(StatusCodes.Failure, "无效参数");
            var result = CreateResult<ResultBase<string>>(StatusCodes.Success);
            var data = _enrollPay.Payment(requestParam);
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 支付成功
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        [Route("v1/paymentSuccess/")]
        [HttpPost]
        [AllowAnonymous]
        public ResultBase PaymentSuccess([FromBody]PaymentSuccessRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<string>>(StatusCodes.Failure, "无效参数");
            _enrollPay.PaymentSuccess(requestParam);
            return CreateResult<ResultBase<string>>(StatusCodes.Success, "操作完成");
        }

        /// <summary>
        /// 获取支付明细
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [Route("v1/GetPayDetail")]
        [HttpPost]
        public ResultBase<PagedResultModel<EnrollPayDetailModel>> GetPayDetail([FromBody]GetPayDetailRequestParam appKey)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<PagedResultModel<EnrollPayDetailModel>>>(StatusCodes.Failure, "无效参数");
            var result = CreateResult<ResultBase<PagedResultModel<EnrollPayDetailModel>>>(StatusCodes.Success);
            result.Data = _enrollPay.GetPayDetail(appKey);
            return result;
        }

        /// <summary>
        /// 保存职位草稿
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/AddJobDraft")]
        public ResultBase AddJobDraft([FromBody]AddJobDraftRequestParam param)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase>(StatusCodes.InvalidParameter, "无效参数");

            _jobDraftService.AddObject(param);
            return CreateResult<ResultBase>(StatusCodes.Success, "发布成功");
        }
        /// <summary>
        /// 获取职位草稿列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/GetJobDraftList")]
        public ResultBase<PagedResultModel<JobDraftObject>> GetJobDraftList([FromBody]GetJobDraftListRequestParam param)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<PagedResultModel<JobDraftObject>>>(StatusCodes.InvalidParameter, "无效参数");
            var result = CreateResult<ResultBase<PagedResultModel<JobDraftObject>>>(StatusCodes.Success);
            result.Data = _jobDraftService.GetObjectByPaged(param);
            return result;
        }
        /// <summary>
        /// 获取职位草稿
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/GetJobDraft")]
        public ResultBase GetJobDraft([FromBody]GetJobDraftRequestParam param)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase>(StatusCodes.InvalidParameter, "无效参数");
            var result = CreateResult<ResultBase<JobDraftObject>>(StatusCodes.Success);
            result.Data = _jobDraftService.GetObjectByID(param.ID);
            return result;
        }

        /// <summary>
        /// 删除草稿
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/DeleteJobDraft")]
        public ResultBase DeleteJobDraft([FromBody]GetJobDraftRequestParam param)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase>(StatusCodes.InvalidParameter, "无效参数");

            _jobDraftService.RemoveObject(param.ID);
            return CreateResult<ResultBase>(StatusCodes.Success, "删除成功");
        }

        /// <summary>
        /// 获取个人简历
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/GetResume")]
        public ResultBase<ResumeObject> GetResume([FromBody]GetResumeBRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<ResumeObject>>(StatusCodes.InvalidParameter, "无效参数");
            var data = _jobSeekerService.GetResume(requestParam);
            var result = CreateResult<ResultBase<ResumeObject>>(StatusCodes.Success);
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 下架职位
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/StopJob")]
        public ResultBase StopJob([FromBody] StopJobRequestParam param)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<JobGroupObject>>(StatusCodes.InvalidParameter, "无效参数");
            var data = _jobGroupService.StopJob(param.jobGroupId);
            var result = CreateResult<ResultBase<int>>(StatusCodes.Success);
            return result;
        }

        /// <summary>
        /// 编辑录用时间
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/EditEnrollDate")]
        public ResultBase EditEnrollDate([FromBody] EditEnrollDatesRequestParam param)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<EnrollDetailObject>>(StatusCodes.InvalidParameter, "无效参数");
            var data = _enroll.EditEnrollDates(param.enrollId, param.enrollDates);
            var result = CreateResult<ResultBase<int>>(StatusCodes.Success);
            return result;
        }

        /// <summary>
        /// 自动取消录用
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/AutoCancelApply")]
        public ResultBase AutoCancelApply([FromBody] AutoCancelApplyRequestParam param)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<EnrollDetailObject>>(StatusCodes.InvalidParameter, "无效参数");
            try
            {
                _enroll.AutoCancelApply(param.enrollId, param.jobSeekerId, param.hour);
            }
            catch (Exception ex)
            {
            }
            var result = CreateResult<ResultBase<int>>(StatusCodes.Success);
            return result;
        }

        /// <summary>
        /// 获取职位每日招聘情况
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/GetDailyRecruitList")]
        public ResultBase GetDailyRecruitList([FromBody]GetDailyRecruitRequestParam param)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<PagedResultModel<JobRecruitDetailModel>>>(StatusCodes.InvalidParameter, "无效参数");
            var result = CreateResult<ResultBase<PagedResultModel<JobRecruitDetailModel>>>(StatusCodes.Success);
            result.Data = _jobRecruitDetailRepository.GetDailyRecruitList(param);
            return result;
        }

        /// <summary>
        /// 获取职位组根据公司
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getJobGroupsByCompany")]
        public ResultBase<IEnumerable<GetJobGroupsByCompanyModel>> GetJobGroupsByCompany([FromBody]GetJobGroupsByCompanyRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<IEnumerable<GetJobGroupsByCompanyModel>>>(StatusCodes.InvalidParameter, "无效参数");
            var data = _jobService.GetJobGroupsByCompany(requestParam);
            var result = CreateResult<ResultBase<IEnumerable<GetJobGroupsByCompanyModel>>>(StatusCodes.Success);
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 获取成功支付列表
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getSuccessPayDetail")]
        public ResultBase<PagedResultModel<EnrollPayDetailModel>> GetSuccessPayDetail([FromBody]GetSuccessPayDetailRequestParam requestParam)
        {
            if (!ModelState.IsValid) return CreateResult<ResultBase<PagedResultModel<EnrollPayDetailModel>>>(StatusCodes.InvalidParameter, "无效参数");
            var data = _enrollPay.GetSuccessPayDetail(requestParam);
            var result = CreateResult<ResultBase<PagedResultModel<EnrollPayDetailModel>>>(StatusCodes.Success);
            result.Data = data;
            return result;
        }
    }
}
