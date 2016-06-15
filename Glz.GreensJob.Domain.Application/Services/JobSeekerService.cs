using System;
using System.Web;
using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using AutoMapper;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;
using Glz.Infrastructure.Caching;
using Glz.GreensJob.Domain.Enums;

namespace Glz.GreensJob.Domain.Application.Services
{
    /// <summary>
    /// 求职者服务
    /// </summary>
    public class JobSeekerService : ApplicationService, IJobSeekerService
    {
        private readonly IRepository<int, JobSeeker> _jobSeekerRepository;
        private readonly IRepository<int, JobSeekerWalletActionLog> _jobSeekerWalletActionLogRepository;
        private readonly IRepository<int, JobSeekerMessage> _jobSeekerMessageRepository;
        private readonly ICache _cache;

        /// <summary>
        /// 创建一个 <c>JobSeekerService</c> 类型实例
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jobSeekerRepository"></param>
        /// <param name="jobSeekerWalletActionLogRepository"></param>
        /// <param name="jobSeekerMessageRepository"></param>
        /// <param name="cache"></param>
        public JobSeekerService(IRepositoryContext context, IRepository<int, JobSeeker> jobSeekerRepository,
            IRepository<int, JobSeekerWalletActionLog> jobSeekerWalletActionLogRepository,
            IRepository<int, JobSeekerMessage> jobSeekerMessageRepository, ICache cache)
            : base(context)
        {
            _jobSeekerRepository = jobSeekerRepository;
            _cache = cache;
            _jobSeekerWalletActionLogRepository = jobSeekerWalletActionLogRepository;
            _jobSeekerMessageRepository = jobSeekerMessageRepository;
        }

        /// <summary>
        /// 获取注册登录信息并且保存登录Session信息
        /// </summary>
        /// <returns></returns>
        private MobileUserInfoModel GetUserInfoAndSaveLoginSession(int jsId, string openId, string seekerCurrentOpenId)
        {
            var userInfo = GetUserInfo(new GetUserInfoRequestParam { jsId = jsId });

            // 记录缓存
            var sessionId = Guid.NewGuid().ToString().Replace("-", "");
            var cacheKey = Const.SeekerSessionCodeCacheKey + jsId;

            userInfo.sessionId = sessionId;
            userInfo.openId = openId;
            if (string.IsNullOrWhiteSpace(openId))
            {
                userInfo.isSameWechatAccount = 1;
            }
            else
            {
                userInfo.isSameWechatAccount = seekerCurrentOpenId == openId ? 1 : 0;
            }
            _cache.Set(cacheKey, userInfo, TimeSpan.FromHours(Const.SeekerSessionsLidingExpireTime));
            return userInfo;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="userMobileNumber"></param>
        /// <param name="password"></param>
        /// <param name="openId"></param>
        /// <param name="invitation"></param>
        private void Register(string userMobileNumber, string password, string openId, int invitation)
        {
            var nowTime = DateTime.Now;

            var invitationjobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == invitation));

            var jobSeeker = new JobSeeker
            {
                payWechatAccount = string.Empty,
                SID = Guid.Empty,
                createDate = nowTime,
                lastLoginDate = nowTime,
                mobile = userMobileNumber,
                password = DES.MD5Encode(password),
                nickName = string.Empty,
                virtualImage = string.Empty,
                wechatToken = openId,
                weiboToken = string.Empty,
                invitation = invitationjobSeeker?.ID
            };
            jobSeeker.JobSeekerConfig = new JobSeekerConfig
            {
                RecruitMessage = true,
                UrgentJobMessage = true,
                CreateTime = nowTime,
                UpdateTime = nowTime,
                JobSeeker = jobSeeker
            };
            jobSeeker.JobSeekerWallet = new JobSeekerWallet
            {
                Password = string.Empty,
                TotalAmounts = 0,
                FrozenAmounts = 0,
                ActualAmounts = 0,
                LastUpdateAmounts = 0,
                CreateTime = nowTime,
                LastUpdateTime = nowTime,
                JobSeeker = jobSeeker,
                LastExtractTime = nowTime.AddDays(-1),
                LastUpdatePasswordTime = nowTime
            };
            jobSeeker.Resume = new Resume
            {
                Gender = true,
                HealthCertificate = false,
                IDNumber = string.Empty,
                Image = string.Empty,
                City_ID = 0,
                CreateTime = nowTime,
                Education = string.Empty,
                Height = 0,
                Major = string.Empty,
                Name = string.Empty,
                University_ID = 0,
                Weight = 0,
                UpdateTime = nowTime,
                Birthday = new DateTime(1900, 1, 1),
                Description = string.Empty,
                JobSeeker = jobSeeker
            };

            _jobSeekerRepository.Add(jobSeeker);
        }

        /// <summary>
        /// 是否存在该手机号
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        public bool ExistsByMobile(string mobile)
        {
            return _jobSeekerRepository.Exists(Specification<JobSeeker>.Eval(x => x.mobile == mobile));
        }

        /// <summary>
        /// 是否存在该手机号
        /// </summary>
        /// <param name="openId">openId</param>
        /// <returns></returns>
        public bool ExistsByOpenId(string openId)
        {
            return _jobSeekerRepository.Exists(Specification<JobSeeker>.Eval(x => x.wechatToken == openId));
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        public MobileUserInfoModel Login(LoginRequestParam requestParam)
        {
            if (requestParam == null)
                throw new ArgumentNullException(nameof(requestParam));
            var queryJobSeeker =
                _jobSeekerRepository.Find(
                    Specification<JobSeeker>.Eval(jobSeeker => jobSeeker.mobile == requestParam.userMobileNumber));
            switch (requestParam.type)
            {
                case 0:
                    if (queryJobSeeker == null)
                    {
                        throw new GreensJobException(StatusCodes.Failure, "手机号或者密码错误");
                    }
                    if (!queryJobSeeker.password.Equals(DES.MD5Encode(requestParam.password)))
                    {
                        throw new GreensJobException(StatusCodes.Failure, "手机号或者密码错误");
                    }
                    break;
                case 1:
                    var loginCacheKey = Const.ValidateCodeCacheKey + "5_" + requestParam.userMobileNumber;
                    // 获取验证码
                    var verificationCodeModel =
                        _cache.Get<VerificationCodeModel>(loginCacheKey);

                    if (verificationCodeModel == null)
                        throw new GreensJobException(0, "请重新获取验证码");

                    if (verificationCodeModel.verificationCode != requestParam.verificationCode)
                        throw new GreensJobException(0, "验证码错误");
                    if (queryJobSeeker == null)
                    {
                        Register(requestParam.userMobileNumber, requestParam.password, requestParam.openId,
                            requestParam.invitation);
                        Context.Commit();
                    }
                    break;
                default:
                    throw new GreensJobException(StatusCodes.Failure, "非法的登陆方式");
            }
            if (queryJobSeeker == null)
            {
                queryJobSeeker =
                _jobSeekerRepository.Find(
                    Specification<JobSeeker>.Eval(jobSeeker => jobSeeker.mobile == requestParam.userMobileNumber));
            }

            UpdateLoginUser(queryJobSeeker, requestParam.isMobile, requestParam.channelId);
            Context.Commit();

            var userInfo = GetUserInfoAndSaveLoginSession(queryJobSeeker.ID, requestParam.openId, queryJobSeeker.wechatToken);

            return userInfo;
        }

        /// <summary>
        /// 绑定手机
        /// </summary>
        /// <param name="requestParam"></param>
        public void BindingMobile(BindingMobileRequestParam requestParam)
        {
            if (string.IsNullOrWhiteSpace(requestParam.openId) || requestParam.openId.ToLower() == "NULL")
            {
                throw new GreensJobException(StatusCodes.Failure, "非法微信号");
            }
            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }
            jobSeeker.wechatToken = requestParam.openId;

            _jobSeekerRepository.Update(jobSeeker);
            Context.Commit();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        public MobileUserInfoModel GetUserInfo(GetUserInfoRequestParam requestParam)
        {
            if (requestParam == null)
                throw new ArgumentNullException(nameof(requestParam));

            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }
            var nowTime = DateTime.Now;
            var result = new MobileUserInfoModel
            {
                id = jobSeeker.ID,
                userName = jobSeeker.Resume.Name,
                age = jobSeeker.Resume.Age,
                gender = jobSeeker.Resume.Gender ? "女" : "男",
                haveNotReadAccountInfo = 0,
                haveNotReadEnrollInfo = 0,
                presentState = (jobSeeker.JobSeekerWallet.TotalAmounts == 0.00m || jobSeeker.JobSeekerWallet.LastExtractTime.Date == nowTime.Date) ? 0 : 1,
                walletBalance = jobSeeker.JobSeekerWallet.ActualAmounts,
                isBinding = 1,
                isSetPassword = string.IsNullOrEmpty(jobSeeker.JobSeekerWallet.Password) ? 0 : 1,
                isSetPayWechatAccount = string.IsNullOrEmpty(jobSeeker.payWechatAccount) ? 0 : 1,
                mobile = jobSeeker.mobile,
                payWechatAccount = jobSeeker.payWechatAccount
            };
            return result;
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        public ConfigurationModel GetConfiguration(GetConfigurationRequestParam requestParam)
        {
            if (requestParam == null)
                throw new ArgumentNullException(nameof(requestParam));

            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            //var jobSeeker = _jobSeekerRepository.FindAll().FirstOrDefault(x => x.wechatToken == requestParam.openId);
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }
            return Mapper.Instance.Map<ConfigurationModel>(jobSeeker.JobSeekerConfig);
        }

        /// <summary>
        /// 设置配置
        /// </summary>
        /// <param name="requestParam"></param>
        public void ConfigurationSet(ConfigurationSetRequestParam requestParam)
        {
            if (requestParam == null)
                throw new ArgumentNullException(nameof(requestParam));

            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            //var jobSeeker = _jobSeekerRepository.FindAll().FirstOrDefault(x => x.wechatToken == requestParam.openId);
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }
            jobSeeker.JobSeekerConfig.RecruitMessage = requestParam.recruitMessage;
            jobSeeker.JobSeekerConfig.UrgentJobMessage = requestParam.urgentJobMessage;
            _jobSeekerRepository.Update(jobSeeker);
            Context.Commit();
        }

        /// <summary>
        /// 提现密码设置
        /// </summary>
        /// <param name="requestParam"></param>
        public void AccountPwdSet(AccountPwdSetRequestParam requestParam)
        {
            if (requestParam == null)
                throw new ArgumentNullException(nameof(requestParam));
            if (string.IsNullOrEmpty(requestParam.password))
                throw new GreensJobException(0, "密码不能为空");
            if (requestParam.password.Length < 6)
                throw new GreensJobException(0, "密码长度不能小于6位");
            if (string.IsNullOrEmpty(requestParam.userMobileNumber))
                throw new GreensJobException(0, "手机号不能为空");

            var cacheKey = Const.ValidateCodeCacheKey + "3_" + requestParam.userMobileNumber;
            // 获取验证码
            var verificationCodeModel =
                _cache.Get<VerificationCodeModel>(cacheKey);

            if (verificationCodeModel == null)
                throw new GreensJobException(0, "请重新获取验证码");

            if (verificationCodeModel.verificationCode != requestParam.verificationCode)
                throw new GreensJobException(0, "验证码错误");

            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            //var jobSeeker = _jobSeekerRepository.FindAll().FirstOrDefault(x => x.wechatToken == requestParam.openId);
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }
            if (jobSeeker.mobile != requestParam.userMobileNumber)
            {
                throw new GreensJobException(0, "该手机号没有和该账号绑定");
            }
            jobSeeker.JobSeekerWallet.Password = DES.MD5Encode(requestParam.password);
            _jobSeekerRepository.Update(jobSeeker);
            Context.Commit();
        }

        /// <summary>
        /// 提现
        /// </summary>
        /// <param name="requestParam"></param>
        public void TransferMoneyOut(TransferMoneyOutRequestParam requestParam)
        {
            if (requestParam == null) throw new ArgumentNullException(nameof(requestParam));
            var nowTime = DateTime.Now;
            if (requestParam.Money <= 0)
                throw new GreensJobException(0, "提现金额必须大于0");
            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            //var jobSeeker = _jobSeekerRepository.FindAll().FirstOrDefault(x => x.wechatToken == requestParam.openId);
            var isSetPayWechatAccount = !string.IsNullOrEmpty(jobSeeker.payWechatAccount);
            if (!isSetPayWechatAccount && string.IsNullOrEmpty(requestParam.cardNumber))
            {
                throw new GreensJobException(0, "必须设置卡号");
            }
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }
            if (requestParam.Money > jobSeeker.JobSeekerWallet.ActualAmounts)
            {
                throw new GreensJobException(0, "提现金额超过了账号余额");
            }
            if (jobSeeker.JobSeekerWallet.LastExtractTime.Date == nowTime.Date)
            {
                throw new GreensJobException(0, "一天只能提现一次");
            }

            if (!isSetPayWechatAccount)
            {
                jobSeeker.payWechatAccount = requestParam.cardNumber;
            }

            jobSeeker.JobSeekerWallet.FrozenAmounts += requestParam.Money;
            jobSeeker.JobSeekerWallet.ActualAmounts -= requestParam.Money;
            jobSeeker.JobSeekerWallet.LastExtractTime = nowTime;

            var extractApply = new ExtractApply
            {
                Amount = requestParam.Money,
                BankCardNo = requestParam.cardNumber,
                CreateTime = nowTime,
                JobSeeker = jobSeeker,
                ExecuteTime = new DateTime(1970, 1, 1),
                Executor_ID = 0
            };

            jobSeeker.ExtractApplys.Add(extractApply);
            _jobSeekerRepository.Update(jobSeeker);
            _jobSeekerWalletActionLogRepository.Add(new JobSeekerWalletActionLog
            {
                ActionID = WalletAction.Extract,
                ActionName = "提现",
                Amount = requestParam.Money * -1,
                BankCardNo = string.Empty,
                CreateTime = nowTime,
                Enroll_ID = 0,
                ExtractApply_ID = 0,
                JobGroup_ID = 0,
                JobSeekerWallet_ID = jobSeeker.JobSeekerWallet.ID,
                Job_ID = 0,
                OpenCity_ID = 0,
                PaySn = string.Empty,
                PayType = 2,
                PayTypeName = "微信",
                State = 0,
                UserName = jobSeeker.mobile,
                ExtractApply = extractApply,
                JobName = "",
                JobGroupName = ""
            });
            Context.Commit();
        }

        /// <summary>
        /// 钱包交易记录
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        public PagedResultModel<JobSeekerWalletActionLogModel> DspTransactionInfo(
            DspTransactionInfoRequestParam requestParam)
        {
            if (requestParam == null) throw new ArgumentNullException(nameof(requestParam));
            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            //var jobSeeker = _jobSeekerRepository.FindAll().FirstOrDefault(x => x.wechatToken == requestParam.openId);
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }
            return
                Mapper.Instance.Map<PagedResultModel<JobSeekerWalletActionLogModel>>(
                    _jobSeekerWalletActionLogRepository.FindAll(
                        Specification<JobSeekerWalletActionLog>.Eval(
                            x => x.JobSeekerWallet_ID == jobSeeker.JobSeekerWallet.ID), x => x.CreateTime,
                        SortOrder.Descending, requestParam.pageIndex, requestParam.pageSize));
        }

        /// <summary>
        /// 消息列表
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        public PagedResultModel<JobSeekerMessageModel> DspMsgList(
            DspMsgListRequestParam requestParam)
        {
            if (requestParam == null) throw new ArgumentNullException(nameof(requestParam));
            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            //var jobSeeker = _jobSeekerRepository.FindAll().FirstOrDefault(x => x.wechatToken == requestParam.openId);
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }
            return
                Mapper.Instance.Map<PagedResultModel<JobSeekerMessageModel>>(
                    _jobSeekerMessageRepository.FindAll(
                        Specification<JobSeekerMessage>.Eval(
                            x => x.JobSeeker_ID == jobSeeker.ID), x => x.CreateTime,
                        SortOrder.Descending, requestParam.pageIndex, requestParam.pageSize));
        }

        /// <summary>
        /// 获取简历
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        public ResumeObject GetResume(GetResumeRequestParam requestParam)
        {
            if (requestParam == null) throw new ArgumentNullException(nameof(requestParam));
            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            //var jobSeeker = _jobSeekerRepository.FindAll().FirstOrDefault(x => x.wechatToken == requestParam.openId);
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }
            var result = Mapper.Instance.Map<ResumeObject>(jobSeeker.Resume);
            result.Mobile = jobSeeker.mobile;
            return result;
        }

        /// <summary>
        /// 获取简历（商铺）
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        public ResumeObject GetResume(GetResumeBRequestParam requestParam)
        {
            if (requestParam == null) throw new ArgumentNullException(nameof(requestParam));
            var jobSeeker = _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.ID));
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }
            var result = Mapper.Instance.Map<ResumeObject>(jobSeeker.Resume);
            result.Mobile = jobSeeker.mobile;
            return result;
        }

        /// <summary>
        /// 设置简历
        /// </summary>
        /// <param name="requestParam"></param>
        public void SetResume(SetResumeRequestParam requestParam)
        {
            if (requestParam == null)
                throw new ArgumentNullException(nameof(requestParam));
            if (requestParam.ResumeObject == null)
                throw new ArgumentNullException(nameof(requestParam.ResumeObject));
            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            //var jobSeeker = _jobSeekerRepository.FindAll().FirstOrDefault(x => x.wechatToken == requestParam.openId);
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }
            jobSeeker.Resume.Age = requestParam.ResumeObject.Age;
            jobSeeker.Resume.Birthday = requestParam.ResumeObject.Birthday;
            jobSeeker.Resume.City_ID = requestParam.ResumeObject.City_ID;
            jobSeeker.Resume.Description = requestParam.ResumeObject.Description;
            jobSeeker.Resume.Education = requestParam.ResumeObject.Education;
            jobSeeker.Resume.Gender = requestParam.ResumeObject.Gender;
            jobSeeker.Resume.HealthCertificate = requestParam.ResumeObject.HealthCertificate;
            jobSeeker.Resume.Height = requestParam.ResumeObject.Height;
            jobSeeker.Resume.IDNumber = requestParam.ResumeObject.IDNumber;
            jobSeeker.Resume.Image = requestParam.ResumeObject.Image;
            jobSeeker.Resume.Major = requestParam.ResumeObject.Major;
            jobSeeker.Resume.Name = requestParam.ResumeObject.Name;
            jobSeeker.Resume.University_ID = requestParam.ResumeObject.University_ID;
            jobSeeker.Resume.Weight = requestParam.ResumeObject.Weight;
            jobSeeker.Resume.UpdateTime = DateTime.Now;

            _jobSeekerRepository.Update(jobSeeker);

            Context.Commit();
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="requestParam"></param>
        /// <returns></returns>
        public MobileUserInfoModel Register(RegisterRequestParam requestParam)
        {
            if (requestParam == null)
                throw new ArgumentNullException(nameof(requestParam));
            if (string.IsNullOrWhiteSpace(requestParam.userMobileNumber))
                throw new GreensJobException(StatusCodes.Failure, "必须填写手机号");
            if (string.IsNullOrWhiteSpace(requestParam.password))
                throw new GreensJobException(StatusCodes.Failure, "必须填写密码");
            if (requestParam.password.Length < 6 || requestParam.password.Length > 20)
                throw new GreensJobException(StatusCodes.Failure, "密码必须为6到8位");

            var cacheKey = Const.ValidateCodeCacheKey + "4_" + requestParam.userMobileNumber;
            // 获取验证码
            var verificationCodeModel =
                _cache.Get<VerificationCodeModel>(cacheKey);

            if (verificationCodeModel == null)
                throw new GreensJobException(0, "请重新获取验证码");

            if (verificationCodeModel.verificationCode != requestParam.verificationCode)
                throw new GreensJobException(0, "验证码错误");

            if (ExistsByMobile(requestParam.userMobileNumber))
                throw new GreensJobException(0, "该手机号已经被注册过");
            Register(requestParam.userMobileNumber, requestParam.password, requestParam.openId, requestParam.invitation);

            Context.Commit();

            var queryJobSeeker =
                _jobSeekerRepository.Find(
                    Specification<JobSeeker>.Eval(jobSeeker => jobSeeker.mobile == requestParam.userMobileNumber));

            // 更新求职者信息
            UpdateLoginUser(queryJobSeeker, requestParam.isMobile, requestParam.channelId);

            Context.Commit();
            var userInfo = GetUserInfoAndSaveLoginSession(queryJobSeeker.ID, requestParam.openId, queryJobSeeker.wechatToken);
            return userInfo;
        }

        /// <summary>
        /// 更新用户登陆信息
        /// </summary>
        /// <param name="queryJobSeeker"></param>
        /// <param name="isMobile"></param>
        /// <param name="channelId"></param>
        private void UpdateLoginUser(JobSeeker queryJobSeeker, int isMobile, string channelId)
        {
            if (isMobile == 1)
            {
                if (string.IsNullOrWhiteSpace(channelId) || channelId.ToUpper() == "NULL")
                {
                    throw new GreensJobException(StatusCodes.Failure, "非法的手机推送标识");
                }
            }

            queryJobSeeker.lastLoginDate = DateTime.Now;
            if (isMobile == 1)
            {
                queryJobSeeker.channelId = channelId;
            }
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="requestParam"></param>
        public void RetrievePassword(RetrievePasswordRequestParam requestParam)
        {
            if (requestParam == null)
                throw new ArgumentNullException(nameof(requestParam));
            if (string.IsNullOrEmpty(requestParam.password))
                throw new GreensJobException(0, "密码不能为空");
            if (requestParam.password.Length < 6)
                throw new GreensJobException(0, "密码长度不能小于6位");
            if (string.IsNullOrEmpty(requestParam.userMobileNumber))
                throw new GreensJobException(0, "手机号不能为空");

            var cacheKey = Const.ValidateCodeCacheKey + "6_" + requestParam.userMobileNumber;
            // 获取验证码
            var verificationCodeModel =
                _cache.Get<VerificationCodeModel>(cacheKey);

            if (verificationCodeModel == null)
                throw new GreensJobException(0, "请重新获取验证码");

            if (verificationCodeModel.verificationCode != requestParam.verificationCode)
                throw new GreensJobException(0, "验证码错误");

            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.mobile == requestParam.userMobileNumber));
            //var jobSeeker = _jobSeekerRepository.FindAll().FirstOrDefault(x => x.wechatToken == requestParam.openId);
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Failure, "该用户还没有注册");
            }
            jobSeeker.password = DES.MD5Encode(requestParam.password);
            _jobSeekerRepository.Update(jobSeeker);
            Context.Commit();
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="requestParam"></param>
        public void Logout(LogoutRequestParam requestParam)
        {
            var cacheKey = Const.SeekerSessionCodeCacheKey + requestParam.jsId;
            _cache.Remove(cacheKey);
        }
    }
}
