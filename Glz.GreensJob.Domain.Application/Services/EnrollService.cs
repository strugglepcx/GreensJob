using System;
using System.Collections.Generic;
using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;
using System.Linq;
using Glz.GreensJob.Domain.Enums;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;
using Glz.Infrastructure.Locking;
using Glz.Infrastructure.Sms;
using Glz.Infrastructure.Logging;
using Hangfire;
using System.Linq.Expressions;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class EnrollService : ApplicationService, IEnrollService
    {
        private readonly IRepository<int, Enroll> _enrollRepository;
        private readonly IRepository<int, Job> _jobRepository;
        private readonly IRepository<int, JobGroup> _jobGroupRepository;
        private readonly IRepository<int, JobSeeker> _jobSeekerRepository;
        private readonly IRepository<int, JobRecruitDetail> _jobRecruitDetailRepository;
        private readonly IRepository<int, EnrollActionLog> _enrollActionLogRepository;
        private readonly IRepository<int, EnrollDetail> _enrollDetailRepository;
        private readonly IRepository<int, Company> _companyRepository;
        private readonly IRepository<int, EnrollPayDetail> _enrollPayDetailRepository;

        public EnrollService(IRepositoryContext context, IRepository<int, Enroll> enrollRepository,
            IRepository<int, Job> jobRepository, IRepository<int, JobSeeker> jobSeekerRepository,
            IRepository<int, JobGroup> jobGroupRepository, IRepository<int, JobRecruitDetail> jobRecruitDetailRepository,
            IRepository<int, EnrollActionLog> enrollActionLogRepository,
            IRepository<int, EnrollDetail> enrollDetailRepository, IRepository<int, Company> companyRepository,
            IRepository<int, EnrollPayDetail> enrollPayDetailRepository)
            : base(context)
        {
            _enrollRepository = enrollRepository;
            _jobRepository = jobRepository;
            _jobSeekerRepository = jobSeekerRepository;
            _jobGroupRepository = jobGroupRepository;
            _jobRecruitDetailRepository = jobRecruitDetailRepository;
            _enrollActionLogRepository = enrollActionLogRepository;
            _enrollDetailRepository = enrollDetailRepository;
            _companyRepository = companyRepository;
            _enrollPayDetailRepository = enrollPayDetailRepository;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="applyDateList"></param>
        /// <param name="enroll"></param>
        /// <param name="job"></param>
        /// <param name="jobGroup"></param>
        private void AddEnrollDetails(IEnumerable<DateTime> applyDateList, Enroll enroll, Job job, JobGroup jobGroup)
        {
            foreach (var applyDate in applyDateList.Distinct())
            {
                enroll.EnrollDetails.Add(new EnrollDetail
                {
                    date = applyDate.Date,
                    start = job.startDate,
                    end = job.endDate,
                    Enroll = enroll,
                    isEmploy = false,
                    isRetired = false
                });
                var jobRecruitDetail =
                    _jobRecruitDetailRepository.Find(
                        Specification<JobRecruitDetail>.Eval(x => x.RecruitDate == applyDate.Date && x.Job_ID == job.ID));
                if (jobRecruitDetail == null)
                {
                    _jobRecruitDetailRepository.Add(new JobRecruitDetail
                    {
                        ApplicantNum = 1,
                        EmploymentNum = 0,
                        RecruitDate = applyDate.Date,
                        JobGroup_ID = jobGroup.ID,
                        Job_ID = job.ID,
                        RecruitNum = job.recruitNum
                    });
                }
                else
                {
                    jobRecruitDetail.ApplicantNum++;
                    _jobRecruitDetailRepository.Update(jobRecruitDetail);
                }
            }
        }

        /// <summary>
        /// 验证报名时间
        /// </summary>
        /// <param name="applyDateList"></param>
        /// <param name="job"></param>
        private void ValidateApplyDates(IEnumerable<DateTime> applyDateList, Job job)
        {
            // 报名时间校验
            ValidateDateRange(applyDateList, job);

            if (job.autoTimeShare)
            {
                // 分时

                var tempApplyDate = DateTime.MinValue; // 临时时间
                var continousDays = 1; // 连续天数
                var isValidateContinousDays = false; // 是否验证连续天数标识

                if (job.continousDays > 1)
                {
                    // 连续报名时间校验
                    foreach (var applyDate in applyDateList.OrderBy(dateTime => dateTime))
                    {
                        if (tempApplyDate.AddDays(1) == applyDate.Date)
                        {
                            continousDays++;
                            if (continousDays >= job.continousDays)
                            {
                                isValidateContinousDays = true;
                                break;
                            }
                        }
                        else
                        {
                            continousDays = 1;
                        }
                        tempApplyDate = applyDate.Date;
                    }
                    if (!isValidateContinousDays)
                    {
                        throw new GreensJobException(0, $"报名需要至少连续{job.continousDays}天");
                    }
                }
            }
            else
            {
                var nowDate = DateTime.Now.Date;

                // 不分时
                for (var i = job.startDate.Date > nowDate ? job.startDate.Date : nowDate; i <= job.endDate.Date; i = i.AddDays(1))
                {
                    if (!applyDateList.Any(x => x == i))
                        throw new GreensJobException(0, $"该职位必须全部报名");
                }
            }
        }

        private void ValidateDateRange(IEnumerable<DateTime> applyDateList, Job job)
        {
            if (
                applyDateList.Any(
                    applyDate => applyDate.Date < job.startDate.Date || applyDate.Date > job.endDate.Date))
                throw new GreensJobException(0, "报名时间不在工作时间范围之内");
        }

        private void CancelApply(Enroll enroll, Job job, JobGroup jobGroup, JobSeeker jobSeeker)
        {
            // 更新招聘明细记录
            foreach (var enrollDetail in enroll.EnrollDetails)
            {
                var jobRecruitDetail =
                    _jobRecruitDetailRepository.Find(
                        Specification<JobRecruitDetail>.Eval(
                            x => x.RecruitDate == enrollDetail.date && x.Job_ID == job.ID));
                if (jobRecruitDetail != null)
                {
                    if (enroll.employStatus == EmployStatu.EmployNotConfirmed ||
                        enroll.employStatus == EmployStatu.SignUp)
                    {
                        jobRecruitDetail.ApplicantNum--;
                    }
                    if (enroll.employStatus == EmployStatu.Employ)
                    {
                        jobRecruitDetail.EmploymentNum--;
                    }
                    _jobRecruitDetailRepository.Update(jobRecruitDetail);
                }
                var enrollPayDetails = _enrollPayDetailRepository.FindAll(Specification<EnrollPayDetail>.Eval(x => x.Enroll_ID == enroll.ID));
                foreach (var enrollPayDetail in enrollPayDetails)
                {
                    _enrollPayDetailRepository.Remove(enrollPayDetail);
                }
            }

            if (enroll.employStatus == EmployStatu.EmployNotConfirmed || enroll.employStatus == EmployStatu.SignUp)
            {
                job.applicantNum--;
                jobGroup.applicantNum--;
            }
            if (enroll.employStatus == EmployStatu.Employ)
            {
                job.employNum--;
                job.onlineEmployNum--;
                jobGroup.employNum--;
                jobGroup.onlineEmployNum--;
            }

            enroll.employStatus = EmployStatu.Cancel;
            _enrollRepository.Update(enroll);
            _jobRepository.Update(job);
            _jobGroupRepository.Update(jobGroup);
            // 添加招聘操作记录
            _enrollActionLogRepository.Add(new EnrollActionLog
            {
                Job_ID = job.ID,
                ActionID = EnrollAction.Cancel,
                ActionName = "取消报名",
                CreateTime = DateTime.Now,
                Enroll = enroll,
                JobSeeker_ID = jobSeeker.ID,
                Job_GroupID = jobGroup.ID,
                Job_Name = job.name
            });
        }

        private void CancelApply(Enroll enroll, JobSeeker jobSeeker)
        {
            var job = _jobRepository.GetByKey(enroll.jobID);
            if (job == null) throw new GreensJobException(0, "未能找到该职位信息");
            var jobGroup = _jobGroupRepository.GetByKey(job.groupID);
            if (jobGroup == null) throw new GreensJobException(0, "未能找到该职位信息");
            CancelApply(enroll, job, jobGroup, jobSeeker);
        }

        public void CancelApply(int enrollId, int jobSeekerId)
        {
            var enroll = _enrollRepository.GetByKey(enrollId);
            if (enroll.employStatus == EmployStatu.Employ)
            {
                return;
            }
            var jobSeeker = _jobSeekerRepository.GetByKey(jobSeekerId);

            CancelApply(enroll, jobSeeker);
            Context.Commit();
        }

        public int Count(int jobSeekerID, bool status)
        {
            try
            {
                var query =
                    _enrollRepository.FindAll(
                        Specification<Enroll>.Eval(x => x.jobSeekerID == jobSeekerID && x.status == status));
                return query.Count<Enroll>();
            }
            catch
            {
                return 0;
            }
        }

        public int AddObject(EnrollObject obj)
        {
            try
            {
                var enroll = new Enroll()
                {
                    jobID = obj.jobID,
                    jobSeekerID = obj.jobSeekerID,
                    introducer = obj.introducer
                };
                _enrollRepository.Add(enroll);
                _enrollRepository.Context.Commit();
                return enroll.ID;
            }
            catch
            {
                _enrollRepository.Context.Rollback();
                return 0;
            }
        }

        public PagedResult<EnrollObject> GetObjectByPaged(int jobSeekerID, int pageIndex)
        {
            try
            {
                var list = _enrollRepository.FindAll(Specification<Enroll>.Eval(x => x.jobSeekerID == jobSeekerID),
                    x => x.jobID, SortOrder.Descending, pageIndex, 10);

                return null;
            }
            catch
            {
                return null;
            }
        }

        public int RemoveObject(int id)
        {
            try
            {
                _enrollRepository.Remove(new Enroll() { ID = id });
                _enrollRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _enrollRepository.Context.Rollback();
                return 0;
            }
        }

        public PagedResultModel<EnrollObject> GetList(int jobGroupID, string name, int pageIndex, int pageSize)
        {
            try
            {
                var list = _enrollRepository.FindAll(
                    Specification<Enroll>.Eval(
                        x =>
                            x.Job.groupID == jobGroupID &&
                            (x.employStatus == EmployStatu.SignUp || x.employStatus == EmployStatu.EmployNotConfirmed || x.employStatus == EmployStatu.UnEmploy) &&
                            (string.IsNullOrEmpty(name) || x.name.Contains(name))),
                        x => x.CreateTime,
                        SortOrder.Descending,
                        pageIndex,
                        pageSize
                    );
                return AutoMapper.Mapper.Map<PagedResultModel<EnrollObject>>(list);
            }
            catch(Exception ex)
            {
                _enrollRepository.Context.Rollback();
                return null;
            }
        }

        public void ApplyJob(ApplyJobRequestParam requestParam)
        {
            if (requestParam == null) throw new ArgumentNullException(nameof(requestParam));
            if (requestParam.applyDateList == null || !requestParam.applyDateList.Any()) throw new GreensJobException(0, "必须填写报名时间");
            var job = _jobRepository.GetByKey(requestParam.jobId);
            if (job == null) throw new GreensJobException(0, "未能找到该职位信息");
            var jobGroup = _jobGroupRepository.GetByKey(job.groupID);
            if (jobGroup == null) throw new GreensJobException(0, "未能找到该职位信息");
            ValidateApplyDates(requestParam.applyDateList, job);
            if (job.status != JobStatus.Employ)
            {
                throw new GreensJobException(0, "该职位还没有发布或者已过期");
            }
            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }
            // 性别
            if (job.genderLimit < 3)
            {
                if ((jobSeeker.Resume.Gender ? 1 : 2) != job.genderLimit)
                {
                    throw new GreensJobException(StatusCodes.Failure, $"该职位要求性别为{(job.genderLimit == 1 ? "男性" : "女性")}");
                }
            }
            // 身高
            if (job.heightLimit > 0)
            {
                if (jobSeeker.Resume.Height < job.heightLimit)
                {
                    throw new GreensJobException(StatusCodes.Failure, $"该职位身高要求{job.heightLimit}CM以上");
                }
            }
            // 健康证需要
            if (job.healthCertificate)
            {
                if (!jobSeeker.Resume.HealthCertificate)
                {
                    throw new GreensJobException(StatusCodes.Failure, $"该职位需要有健康证");
                }
            }

            if (
                _enrollRepository.Exists(
                    Specification<Enroll>.Eval(
                        x =>
                            x.jobID == job.ID && x.jobSeekerID == jobSeeker.ID &&
                            (x.employStatus == EmployStatu.SignUp || x.employStatus == EmployStatu.Employ ||
                             x.employStatus == EmployStatu.EmployNotConfirmed))))
            {
                throw new GreensJobException(StatusCodes.Failure, "不能重复报名");
            }

            if (
                _enrollRepository.Exists(
                    Specification<Enroll>.Eval(
                        x =>
                            x.jobID == job.ID && x.jobSeekerID == jobSeeker.ID &&
                            (x.employStatus == EmployStatu.UnEmploy))))
            {
                throw new GreensJobException(StatusCodes.Failure, "用人单位已经不录用您，请选择其它职位。");
            }

            var dateRange = requestParam.applyDateList.ToList();
            var hasSameDateEnrolls =
                _enrollRepository.FindAll(x => x.EnrollDetails).Any(x =>
                    x.EnrollDetails.Any(enrollDetail => dateRange.Contains(enrollDetail.date)) &&
                    //(x.employStatus == EmployStatu.EmployNotConfirmed || x.employStatus == EmployStatu.Employ) &&
                    (x.employStatus == EmployStatu.Employ || x.employStatus == EmployStatu.Payed) &&
                    (x.jobSeekerID == jobSeeker.ID) &&
                    x.jobID != job.ID);
            if (hasSameDateEnrolls)
            {
                throw new GreensJobException(StatusCodes.Failure, "报名时间中包含了已经工作的时间。");
            }

            var enroll = new Enroll
            {
                jobID = requestParam.jobId,
                employStatus = EmployStatu.SignUp,
                experienced = false,
                introducer = 0,
                mobile = jobSeeker.mobile,
                name = jobSeeker.nickName,
                status = true,
                enrollMethod = EnrollMethod.Online,
                jobSeekerID = jobSeeker.ID,
                CreateTime = DateTime.Now
            };
            AddEnrollDetails(requestParam.applyDateList, enroll, job, jobGroup);
            job.applicantNum++;
            jobGroup.applicantNum++;

            _enrollRepository.Add(enroll);
            _jobRepository.Update(job);
            _jobGroupRepository.Update(jobGroup);
            // 添加招聘操作记录
            _enrollActionLogRepository.Add(new EnrollActionLog
            {
                Job_ID = job.ID,
                ActionID = EnrollAction.SignUp,
                ActionName = "报名",
                CreateTime = DateTime.Now,
                Enroll = enroll,
                JobSeeker_ID = jobSeeker.ID,
                Job_GroupID = jobGroup.ID,
                Job_Name = job.name
            });
            Context.Commit();
        }

        public void CancelApply(CancelApplyRequestParam requestParam)
        {
            if (requestParam == null) throw new ArgumentNullException(nameof(requestParam));
            var job = _jobRepository.GetByKey(requestParam.jobId);
            if (job == null) throw new GreensJobException(0, "未能找到该职位信息");
            var jobGroup = _jobGroupRepository.GetByKey(job.groupID);
            if (jobGroup == null) throw new GreensJobException(0, "未能找到该职位信息");
            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }
            var enroll =
                _enrollRepository.Find(
                    Specification<Enroll>.Eval(
                        x =>
                            x.jobID == job.ID && x.jobSeekerID == jobSeeker.ID &&
                            (x.employStatus == EmployStatu.SignUp || x.employStatus == EmployStatu.EmployNotConfirmed ||
                             x.employStatus == EmployStatu.Employ)), x => x.EnrollDetails);
            if (enroll == null)
            {
                throw new GreensJobException(0, "您还没有报名该职位。");
            }

            CancelApply(enroll, job, jobGroup, jobSeeker);
            Context.Commit();
        }

        public void ModifyApply(ModifyApplyRequestParam requestParam)
        {
            if (requestParam == null) throw new ArgumentNullException(nameof(requestParam));
            if (requestParam.applyDateList == null) throw new GreensJobException(0, "必须填写报名时间");
            var job = _jobRepository.GetByKey(requestParam.jobId);
            if (job == null) throw new GreensJobException(0, "未能找到该职位信息");
            var jobGroup = _jobGroupRepository.GetByKey(job.groupID);
            if (jobGroup == null) throw new GreensJobException(0, "未能找到该职位信息");
            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }
            var enroll =
                _enrollRepository.Find(
                    Specification<Enroll>.Eval(
                        x =>
                            x.jobID == job.ID && x.jobSeekerID == jobSeeker.ID && (x.employStatus == EmployStatu.SignUp)),
                    x => x.EnrollDetails);
            if (enroll == null)
            {
                throw new GreensJobException(0, "您还没有报名该职位或者您已经被录用");
            }

            ValidateApplyDates(requestParam.applyDateList, job);

            var removeList = new List<EnrollDetail>();
            // 更新招聘明细记录
            foreach (var enrollDetail in enroll.EnrollDetails)
            {
                var jobRecruitDetail =
                    _jobRecruitDetailRepository.Find(
                        Specification<JobRecruitDetail>.Eval(
                            x => x.RecruitDate == enrollDetail.date && x.Job_ID == job.ID));
                if (jobRecruitDetail != null)
                {
                    jobRecruitDetail.ApplicantNum--;
                    _jobRecruitDetailRepository.Update(jobRecruitDetail);
                }
                removeList.Add(enrollDetail);
            }
            foreach (var enrollDetail in removeList)
            {
                _enrollDetailRepository.Remove(enrollDetail);
            }
            AddEnrollDetails(requestParam.applyDateList, enroll, job, jobGroup);

            enroll.employStatus = EmployStatu.SignUp;
            _enrollRepository.Update(enroll);
            // 添加招聘操作记录
            _enrollActionLogRepository.Add(new EnrollActionLog
            {
                Job_ID = job.ID,
                ActionID = EnrollAction.UpdateEmploy,
                ActionName = "修改录用",
                CreateTime = DateTime.Now,
                Enroll = enroll,
                JobSeeker_ID = jobSeeker.ID,
                Job_GroupID = jobGroup.ID,
                Job_Name = job.name
            });
            _enrollRepository.Update(enroll);
            Context.Commit();
        }

        public void ConfirmApply(ConfirmApplyRequestParam requestParam)
        {
            if (requestParam == null) throw new ArgumentNullException(nameof(requestParam));
            var job = _jobRepository.GetByKey(requestParam.jobId);
            if (job == null) throw new GreensJobException(0, "未能找到该职位信息");
            var jobGroup = _jobGroupRepository.GetByKey(job.groupID);
            if (jobGroup == null) throw new GreensJobException(0, "未能找到该职位信息");
            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }
            var enroll =
                _enrollRepository.Find(
                    Specification<Enroll>.Eval(
                        x =>
                            x.jobID == job.ID && x.jobSeekerID == jobSeeker.ID &&
                            (x.employStatus == EmployStatu.EmployNotConfirmed)));
            if (enroll == null)
            {
                throw new GreensJobException(0, "该职位商户还没有录用您或者您已经确认过");
            }
            var dateRange = enroll.EnrollDetails.Select(enrollDetail => enrollDetail.date).ToList();
            var nowTime = DateTime.Now;
            // 包含相同日期其他报名信息全部取消
            var sameDateEnrolls =
                _enrollRepository.FindAll(x => x.EnrollDetails).Where(x =>
                    x.EnrollDetails.Any(enrollDetail => dateRange.Contains(enrollDetail.date)) &&
                    (x.employStatus == EmployStatu.SignUp || x.employStatus == EmployStatu.EmployNotConfirmed) &&
                    (x.jobSeekerID == jobSeeker.ID) &&
                    x.jobID != job.ID);
            var sameDateEnrollList = sameDateEnrolls.ToList();
            var saveStateSameDateEnrollList = new List<Tuple<string, string, EmployStatu>>();
            foreach (var sameDateEnroll in sameDateEnrollList)
            {
                saveStateSameDateEnrollList.Add(new Tuple<string, string, EmployStatu>(sameDateEnroll.Job.ID.ToString(), sameDateEnroll.Job.name, sameDateEnroll.employStatus));
                CancelApply(sameDateEnroll, jobSeeker);
            }

            // 更新招聘明细记录
            foreach (var enrollDetail in enroll.EnrollDetails)
            {
                var jobRecruitDetail =
                    _jobRecruitDetailRepository.Find(
                        Specification<JobRecruitDetail>.Eval(
                            x => x.RecruitDate == enrollDetail.date && x.Job_ID == job.ID));
                if (jobRecruitDetail != null)
                {
                    jobRecruitDetail.EmploymentNum++;
                    jobRecruitDetail.ApplicantNum--;

                    _jobRecruitDetailRepository.Update(jobRecruitDetail);
                }
                enroll.EnrollEmployDetails.Add(new EnrollEmployDetail
                {
                    Enroll_ID = enroll.ID,
                    Date = enrollDetail.date,
                    StartTime = enrollDetail.start,
                    EndTime = enrollDetail.end
                });
            }

            job.applicantNum--;
            job.employNum++;
            job.onlineEmployNum++;

            jobGroup.applicantNum--;
            jobGroup.employNum++;
            jobGroup.onlineEmployNum++;

            _jobRepository.Update(job);
            _jobGroupRepository.Update(jobGroup);

            enroll.employStatus = EmployStatu.Employ;
            _enrollRepository.Update(enroll);
            // 添加招聘操作记录
            _enrollActionLogRepository.Add(new EnrollActionLog
            {
                Job_ID = job.ID,
                ActionID = EnrollAction.Confirm,
                ActionName = "确认",
                CreateTime = nowTime,
                Enroll = enroll,
                JobSeeker_ID = jobSeeker.ID,
                Job_GroupID = jobGroup.ID,
                Job_Name = job.name
            });
            _enrollPayDetailRepository.Add(new EnrollPayDetail
            {
                AmountSalary = 0,
                BankCardNo = "",
                Company_ID = job.companyID,
                CreateTime = nowTime,
                StartDate = job.startDate.Date,
                EndDate = job.endDate.Date,
                DaySalary = 0,
                EnrollPay_ID = null,
                Enroll_ID = enroll.ID,
                JobName = job.addr,
                JobSeeker_ID = jobSeeker.ID,
                Job_ID = job.ID,
                JobGroup_ID = jobGroup.ID,
                Publisher_ID = null,
                State = EnrollPayState.Processed,
                UserMobile = jobSeeker.mobile,
                UserName = jobSeeker.Resume.Name,
                WorkDays = enroll.EnrollEmployDetails.Count,
                PayTime = nowTime,
                JobGroupName = jobGroup.name
            });

            Context.Commit();

            // 发送微信信息
            foreach (var saveStateSameDateEnroll in saveStateSameDateEnrollList)
            {
                BackgroundJob.Enqueue<IWeiXinMessage>(
                    x =>
                        x.SendEmployResultNotice(jobSeeker.wechatToken, $"{job.startDate:MM月dd日}至{job.endDate:MM月dd日}",
                            saveStateSameDateEnroll.Item1, saveStateSameDateEnroll.Item2,
                            saveStateSameDateEnroll.Item3 == EmployStatu.EmployNotConfirmed
                                ? "待确认录用"
                                : saveStateSameDateEnroll.Item3 == EmployStatu.SignUp ? "报名职位" : "未知状态",
                                job.name));
            }
        }

        public PagedResultModel<EnrollObject> GetEmployeeInfoList(int jobGroupId, int[] showMethod, int pageIndex, int pageSize)
        {
            if (showMethod == null)
            {
                throw new GreensJobException(0, "参数错误");
            }

            Expression<Func<Enroll, bool>> expression = null;
            Expression<Func<Enroll, bool>> expression1 = null;
            Expression<Func<Enroll, bool>> expression2 = x => x.employStatus == EmployStatu.Employ && x.Job.JobGroup.ID == jobGroupId;

            EnrollMethod m1 = 0;
            EnrollMethod m2 = 0;
            EnrollMethod m3 = 0;

            if (showMethod.Length == 1)
            {
                m1 = (EnrollMethod)showMethod[0];
                expression1 = x => x.enrollMethod == m1;
            }
            if (showMethod.Length == 2)
            {
                m1 = (EnrollMethod)showMethod[0];
                m2 = (EnrollMethod)showMethod[1];
                expression1 = x => x.enrollMethod == m1 || x.enrollMethod == m2;
            }
            if (showMethod.Length == 3)
            {
                m1 = (EnrollMethod)showMethod[0];
                m2 = (EnrollMethod)showMethod[1];
                m3 = (EnrollMethod)showMethod[2];
                expression1 = x => x.enrollMethod == m1 || x.enrollMethod == m2 || x.enrollMethod == m3;
            }

            expression=expression1.And(expression2);

            var list =
                _enrollRepository.FindAll(
                    Specification<Enroll>.Eval(expression),
                    x => x.CreateTime,
                    SortOrder.Descending,
                    pageIndex,
                    pageSize
                );

            return AutoMapper.Mapper.Map<PagedResultModel<EnrollObject>>(list);
        }

        public void Employ(EmployRequestParam requestParam)
        {
            if (requestParam == null) throw new ArgumentNullException(nameof(requestParam));
            if (requestParam.applyDateList == null || !requestParam.applyDateList.Any()) throw new GreensJobException(0, "必须填写报名时间");
            var enroll = _enrollRepository.GetByKey(requestParam.enrollId);
            if (enroll == null)
                throw new GreensJobException(0, "该录用信息不存在");
            if (!(enroll.employStatus == EmployStatu.SignUp || enroll.employStatus == EmployStatu.EmployNotConfirmed))
                throw new GreensJobException(0, "该录用信息不是报名状态");
            var job = _jobRepository.GetByKey(enroll.jobID);
            if (job == null)
                throw new GreensJobException(0, "未能找到该职位信息");
            var jobGroup = _jobGroupRepository.GetByKey(job.groupID);
            if (jobGroup == null)
                throw new GreensJobException(0, "未能找到该职位信息");
            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == enroll.jobSeekerID));
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Failure, "未找到求职者信息");
            }
            var company = _companyRepository.GetByKey(job.companyID);
            if (company == null)
            {
                throw new GreensJobException(StatusCodes.Failure, "未找到公司信息");
            }
            ValidateDateRange(requestParam.applyDateList, job);
            var dateRange = enroll.EnrollDetails.Select(enrollDetail => enrollDetail.date).ToList();
            var hasSameDateEnrolls =
                _enrollRepository.FindAll(x => x.EnrollDetails).Any(x =>
                    x.EnrollDetails.Any(enrollDetail => dateRange.Contains(enrollDetail.date)) &&
                    //(x.employStatus == EmployStatu.EmployNotConfirmed || x.employStatus == EmployStatu.Employ) &&
                    (x.employStatus == EmployStatu.Employ) &&
                    (x.jobSeekerID == jobSeeker.ID) &&
                    x.jobID != job.ID);
            if (hasSameDateEnrolls)
            {
                throw new GreensJobException(StatusCodes.Failure, "录用时间冲突，请调整录用时间");
            }

            var removeList = new List<EnrollDetail>();
            // 更新招聘明细记录
            foreach (var enrollDetail in enroll.EnrollDetails)
            {
                var jobRecruitDetail =
                    _jobRecruitDetailRepository.Find(
                        Specification<JobRecruitDetail>.Eval(
                            x => x.RecruitDate == enrollDetail.date && x.Job_ID == job.ID));
                if (jobRecruitDetail != null)
                {
                    jobRecruitDetail.ApplicantNum--;
                    _jobRecruitDetailRepository.Update(jobRecruitDetail);
                }
                removeList.Add(enrollDetail);
            }
            foreach (var enrollDetail in removeList)
            {
                _enrollDetailRepository.Remove(enrollDetail);
            }
            AddEnrollDetails(requestParam.applyDateList, enroll, job, jobGroup);

            enroll.employStatus = EmployStatu.EmployNotConfirmed;
            _enrollRepository.Update(enroll);
            // 添加招聘操作记录
            _enrollActionLogRepository.Add(new EnrollActionLog
            {
                Job_ID = job.ID,
                ActionID = EnrollAction.Employ,
                ActionName = "录用",
                CreateTime = DateTime.Now,
                Enroll = enroll,
                JobSeeker_ID = enroll.jobSeekerID,
                Job_GroupID = jobGroup.ID,
                Job_Name = job.name
            });
            _enrollRepository.Update(enroll);
            Context.Commit();
            // 发送短信
            BackgroundJob.Enqueue<ISmsMessage>(x => x.SendEmploySuccessNotice(jobSeeker.mobile, job.name, enroll.EnrollDetails.Min(enrollDetail => enrollDetail.date).ToString("MM月dd日"), job.addr));
            // 发送成功录用通知
            BackgroundJob.Enqueue<IWeiXinMessage>(
                x =>
                    x.SendEmploySuccessNotice(jobSeeker.wechatToken, job.name, company.name, $"{job.salary}{job.PayUnit.name}",
                        $"{job.startDate:MM月dd日}至{job.endDate:MM月dd日}", job.addr));
        }

        public void UnEmploy(EmployRequestParam requestParam)
        {
            var enroll = _enrollRepository.GetByKey(requestParam.enrollId);
            if (enroll == null)
                throw new GreensJobException(0, "该录用信息不存在");
            if (!(enroll.employStatus == EmployStatu.SignUp || enroll.employStatus == EmployStatu.EmployNotConfirmed || enroll.employStatus == EmployStatu.Employ))
                throw new GreensJobException(0, "该录用状态不正确");
            var job = _jobRepository.GetByKey(enroll.jobID);
            if (job == null)
                throw new GreensJobException(0, "未能找到该职位信息");
            var jobGroup = _jobGroupRepository.GetByKey(job.groupID);
            if (jobGroup == null)
                throw new GreensJobException(0, "未能找到该职位信息");
            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == enroll.jobSeekerID));
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Failure, "未找到求职者信息");
            }
            var company = _companyRepository.GetByKey(job.companyID);
            if (company == null)
            {
                throw new GreensJobException(StatusCodes.Failure, "未找到公司信息");
            }

            if (enroll.employStatus == EmployStatu.Employ)
            {
                job.applicantNum++;
                job.employNum--;
                job.onlineEmployNum--;
                _jobRepository.Update(job);

                jobGroup.applicantNum++;
                jobGroup.employNum--;
                jobGroup.onlineEmployNum--;
                _jobGroupRepository.Update(jobGroup);

                // 更新招聘明细记录
                foreach (var enrollDetail in enroll.EnrollDetails)
                {
                    var jobRecruitDetail =
                        _jobRecruitDetailRepository.Find(
                            Specification<JobRecruitDetail>.Eval(
                                x => x.RecruitDate == enrollDetail.date && x.Job_ID == job.ID));
                    if (jobRecruitDetail != null)
                    {
                        jobRecruitDetail.ApplicantNum++;
                        jobRecruitDetail.EmploymentNum--;
                        _jobRecruitDetailRepository.Update(jobRecruitDetail);
                    }
                }

                var payDetail = _enrollPayDetailRepository.Find(Specification<EnrollPayDetail>.Eval(x => x.Enroll_ID == enroll.ID));
                _enrollPayDetailRepository.Remove(payDetail);
            }

            enroll.employStatus = EmployStatu.UnEmploy;//不录用
            _enrollRepository.Update(enroll);
            // 添加招聘操作记录
            _enrollActionLogRepository.Add(new EnrollActionLog
            {
                Job_ID = job.ID,
                ActionID = EnrollAction.UnEmploy,
                ActionName = "不录用",
                CreateTime = DateTime.Now,
                Enroll = enroll,
                JobSeeker_ID = enroll.jobSeekerID,
                Job_GroupID = jobGroup.ID,
                Job_Name = job.name
            });
            _enrollRepository.Update(enroll);
            Context.Commit();
        }

        public int EditEnrollDates(int enrollId, IEnumerable<DateTime> enrollDates)
        {
            if (enrollDates == null || !enrollDates.Any()) throw new GreensJobException(0, "必须填写报名时间");
            try
            {
                var list = _enrollDetailRepository.FindAll(Specification<EnrollDetail>.Eval(x => x.enrollID == enrollId)).ToList();
                foreach (var item in list)
                {
                    if (!enrollDates.Contains(item.date))
                    {
                        _enrollDetailRepository.Remove(item);
                        continue;
                    }
                    item.start = DateTime.Parse(enrollDates.Min().ToString("yyyy-MM-dd ") + item.start.TimeOfDay);
                    item.end = DateTime.Parse(enrollDates.Max().ToString("yyyy-MM-dd ") + item.end.TimeOfDay);
                }
                Context.Commit();

                return 1;
            }
            catch (Exception)
            {
                _enrollDetailRepository.Context.Rollback();
                return 0;
            }
        }

        public void AutoCancelApply(int enrollId, int jobSeekerId, double hour)
        {
            BackgroundJob.Schedule<IEnrollService>(x => x.CancelApply(enrollId, jobSeekerId), TimeSpan.FromHours(hour));
        }
    }
}
