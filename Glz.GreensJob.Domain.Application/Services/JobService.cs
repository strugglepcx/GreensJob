using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using Apworks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using AutoMapper;
using AutoMapper.Internal;
using Glz.GreensJob.Domain.Enums;
using Glz.GreensJob.Domain.IApplication;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;
using Glz.Infrastructure.Caching;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class JobService : ApplicationService, IJobService
    {
        private readonly IRepository<int, Job> _jobRepository;
        private readonly IRepository<int, JobCategory> _jobCategoryRepository;
        private readonly IRepository<int, JobClassify> _jobClassifyRepository;
        private readonly IRepository<int, JobSchdule> _jobSchduleRepository;
        private readonly IRepository<int, PayCategory> _payCategoryRepository;
        private readonly IRepository<int, PayUnit> _payUnitRepository;
        private readonly IRepository<int, Publisher> _publisherRepository;
        private readonly IRepository<int, JobGroup> _jobGroupRepository;
        private readonly IRepository<int, Collect> _collectRepository;
        private readonly IRepository<int, SearchRecord> _searchRecordRepository;
        private readonly IRepository<int, Enroll> _enrollRepository;
        private readonly IRepository<int, JobSeeker> _jobSeekerRepository;
        private readonly IRepository<int, EnrollPayDetail> _enrollPayDetailRepository;
        private readonly IJobQueryService _jobQueryService;

        /// <summary>
        /// 共享标题
        /// </summary>
        private string _shareTitle = ConfigurationManager.AppSettings["ShareTitle"];
        /// <summary>
        /// 共享图标
        /// </summary>
        private string _shareIcon = ConfigurationManager.AppSettings["ShareIcon"];
        /// <summary>
        /// 分享描述
        /// </summary>
        private string _shareDes = ConfigurationManager.AppSettings["ShareDes"];
        /// <summary>
        /// 分享链接
        /// </summary>
        private string _shareUrl = ConfigurationManager.AppSettings["ShareUrl"];

        public JobService(IRepositoryContext context, IRepository<int, Job> jobRepository,
            IRepository<int, JobCategory> jobCategoryRepository, IRepository<int, JobClassify> jobClassifyRepository,
            IRepository<int, JobSchdule> jobSchduleRepository, IRepository<int, PayCategory> payCategoryRepository,
            IRepository<int, PayUnit> payUnitRepository, IRepository<int, Publisher> publisherRepository,
            IRepository<int, JobGroup> jobGroupRepository, IRepository<int, Collect> collectRepository,
            IRepository<int, SearchRecord> searchRecordRepository, IRepository<int, Enroll> enrollRepository,
            IRepository<int, JobSeeker> jobSeekerRepository, IRepository<int, EnrollPayDetail> enrollPayDetailRepository, IJobQueryService jobQueryService)
            : base(context)
        {
            _jobRepository = jobRepository;
            _jobCategoryRepository = jobCategoryRepository;
            _jobClassifyRepository = jobClassifyRepository;
            _jobSchduleRepository = jobSchduleRepository;
            _payCategoryRepository = payCategoryRepository;
            _payUnitRepository = payUnitRepository;
            _publisherRepository = publisherRepository;
            _jobGroupRepository = jobGroupRepository;
            _collectRepository = collectRepository;
            _searchRecordRepository = searchRecordRepository;
            _enrollRepository = enrollRepository;
            _jobSeekerRepository = jobSeekerRepository;
            _enrollPayDetailRepository = enrollPayDetailRepository;
            _jobQueryService = jobQueryService;
        }

        /// <summary>
        /// 是否收藏
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="jsId"></param>
        /// <returns></returns>
        private bool IsCollect(int jobId, int jsId)
        {
            return _collectRepository.FindAll(collect => collect.jobSeeker).Any(collect => collect.jobID == jobId && collect.jobSeeker.ID == jsId);
        }

        public int AddObject(List<JobObject> list)
        {
            try
            {
                foreach (JobObject obj in list)
                    _jobRepository.Add(new Job()
                    {
                        jobCategoryID = obj.jobCategoryID,
                        jobClassifyID = obj.jobClassifyID,
                        jobSchduleID = obj.jobSchduleID,
                        payCategoryID = obj.payCategoryID,
                        payUnitID = obj.payUnitID,
                        publisherID = obj.publisherID,
                        name = obj.name,
                        genderLimit = obj.genderLimit,
                        heightLimit = obj.heightLimit,
                        autoTimeShare = obj.autoTimeShare,
                        erollMethod = obj.erollMethod,
                        recruitNum = obj.recruitNum,
                        salary = obj.salary,
                        urgent = obj.urgent,
                        healthCertificate = obj.healthCertificate,
                        interview = obj.interview,
                        interviewPlace = obj.interviewPlace,
                        employer = obj.employer,
                        groupID = obj.groupID,
                        addr = obj.addr,
                        lng = obj.lng,
                        lat = obj.lat,
                        content = obj.content,
                        startDate = obj.startDate,
                        endDate = obj.endDate,
                        contactMan = obj.contactMan,
                        mobileNumber = obj.mobileNumber,
                        gatheringPlace = obj.gatheringPlace,
                        releaseDate = obj.releaseDate,
                        status = (JobStatus)obj.status,
                        createDate = DateTime.Now
                    });
                _jobRepository.Context.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                _jobRepository.Context.Rollback();
                return 0;
            }
        }

        public JobObject GetObjectByID(int id)
        {
            try
            {
                var job = _jobRepository.Find(Specification<Job>.Eval(x => x.ID == id));
                if (job != null)
                {
                    var jobObject = Mapper.Instance.Map<JobObject>(job);
                    // 职位类型
                    var jobCategory = _jobCategoryRepository.Find(Specification<JobCategory>.Eval(x => x.ID == job.jobCategoryID));
                    if (jobCategory != null)
                        jobObject.jobCategoryName = jobCategory.name;
                    // 职位分类
                    var jobClassify = _jobClassifyRepository.Find(Specification<JobClassify>.Eval(x => x.ID == job.jobClassifyID));
                    if (jobClassify != null)
                        jobObject.jobClassifyName = jobClassify.name;
                    // 职位档期
                    var jobSchdule = _jobSchduleRepository.Find(Specification<JobSchdule>.Eval(x => x.ID == job.jobSchduleID));
                    if (jobSchdule != null)
                        jobObject.jobSchduleName = jobSchdule.name;
                    // 结算方式
                    var payCategory = _payCategoryRepository.Find(Specification<PayCategory>.Eval(x => x.ID == job.payCategoryID));
                    if (payCategory != null)
                        jobObject.payCategoryName = payCategory.name;
                    // 结算单位
                    var payUnit = _payUnitRepository.Find(Specification<PayUnit>.Eval(x => x.ID == job.payUnitID));
                    if (payUnit != null)
                        jobObject.payUnitName = payUnit.name;
                    // 发布人信息
                    var publisher = _publisherRepository.Find(Specification<Publisher>.Eval(x => x.ID == job.publisherID));
                    if (publisher != null)
                    {
                        jobObject.publisherName = publisher.name;
                        jobObject.publisherMobile = publisher.mobile;
                    }
                    return jobObject;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public PagedResultModel<GetJobsModel> GetJobs(GetJobsRequestParam requestParam)
        {
            DbGeography location;
            try
            {
                location = DbGeography.FromText($"POINT({requestParam.lng} {requestParam.lat})");
            }
            catch
            {
                throw new GreensJobException(StatusCodes.Failure, "经纬度数据异常！");
            }
            PagedResultModel<GetJobsModel> result;

            switch (requestParam.sortCondition)
            {
                case 0:
                    result =
                        _jobQueryService.GetJobsOrderByRelaseDate(
                            Mapper.Instance.Map<GetJobsOrderByRelaseDateRequestParam>(requestParam));
                    break;
                case 1:
                    var skip = (requestParam.pageIndex - 1) * requestParam.pageSize;
                    var take = requestParam.pageSize;
                    var queryJobs = _jobRepository.FindAll(
                        x => x.PayCategory,
                        x => x.PayUnit,
                        x => x.JobClassify,
                        x => x.JobCategory,
                        x => x.District);
                    var pagedGroupDistanceAscending = queryJobs.Where(x =>
                        (string.IsNullOrEmpty(requestParam.keyword) || x.name.Contains(requestParam.keyword)) &&
                        (requestParam.@class == 0 || x.jobClassifyID == requestParam.@class) &&
                        (requestParam.schedule == 0 || x.jobCategoryID == requestParam.schedule) &&
                        (requestParam.payMethod == 0 || x.payCategoryID == requestParam.payMethod) &&
                        (requestParam.district == 0 || x.District_ID == requestParam.district) &&
                        (x.City_ID == requestParam.city) &&
                        (x.canEnroll == 1) &&
                        (x.status == JobStatus.Employ))
                        .OrderBy(x => x.Location.Distance(location))
                        .Skip(skip)
                        .Take(take)
                        .GroupBy(p => new { Total = queryJobs.Count() })
                        .FirstOrDefault();

                    if (pagedGroupDistanceAscending == null)
                        return null;
                    result =
                        Mapper.Instance.Map<PagedResultModel<GetJobsModel>>(
                            new PagedResult<Job>(pagedGroupDistanceAscending.Key.Total,
                                (pagedGroupDistanceAscending.Key.Total + requestParam.pageSize - 1) /
                                requestParam.pageSize,
                                requestParam.pageSize, requestParam.pageIndex,
                                pagedGroupDistanceAscending.Select(p => p).ToList()));
                    break;
                default:
                    return null;
            }

            if (result != null)
            {
                foreach (var getJobsModel in result.Data)
                {
                    getJobsModel.audit = 1;
                    getJobsModel.Location = DbGeography.FromText($"POINT({getJobsModel.lng} {getJobsModel.lat})");
                    var jobDistance = getJobsModel.Location.Distance(location);
                    getJobsModel.distance = getJobsModel.districtName + "/" +
                                            (jobDistance.HasValue ? $"{jobDistance / 1000:N1}km" : "0km");
                    getJobsModel.startDate = getJobsModel.startDate + getJobsModel.dayNum;
                    getJobsModel.CollectState = IsCollect(getJobsModel.Id, requestParam.jsId);
                    var queryEnroll = _enrollRepository.FindAll(enroll => enroll.JobSeeker,
                        enroll => enroll.EnrollDetails)
                        .FirstOrDefault(
                            enroll =>
                                enroll.jobID == getJobsModel.Id &&
                                (enroll.employStatus == EmployStatu.SignUp ||
                                 enroll.employStatus == EmployStatu.EmployNotConfirmed ||
                                 enroll.employStatus == EmployStatu.Employ) &&
                                enroll.JobSeeker.ID == requestParam.jsId);
                    getJobsModel.ApplyState = queryEnroll != null;
                    getJobsModel.EmployStatu = queryEnroll == null ? 0 : (int)queryEnroll.employStatus;

                }
            }
            return result;
        }

        public int RemoveObject(int id)
        {
            try
            {
                _jobRepository.Remove(new Job() { ID = id });
                _jobRepository.Context.Commit();
                return 1;
            }
            catch
            {
                _jobRepository.Context.Rollback();
                return 0;
            }
        }

        public int UpdateObject(JobObject obj)
        {
            try
            {
                var job = AutoMapper.Mapper.Map<Job>(obj);
                _jobRepository.Update(job);
                _jobRepository.Context.Commit();
                return job.ID;
            }
            catch
            {
                _jobRepository.Context.Rollback();
                return 0;
            }
        }

        public PagedResultModel<JobInfo> GetJobInfo(GetJobInfoRequestParam requestParam)
        {
            if (requestParam.Status < 1 || requestParam.Status > 7) throw new GreensJobException(0, "类型超出范围");
            requestParam.Status = requestParam.Status - 1;
            var status = JobStatus.Draft;
            switch (requestParam.Status)
            {
                case 2:
                    status = JobStatus.Draft;
                    break;
                case 3:
                    status = JobStatus.PendingAudit;
                    break;
                case 4:
                    status = JobStatus.Employ;
                    break;
                case 5:
                    status = JobStatus.Expired;
                    break;
                case 6:
                    status = JobStatus.Stop;
                    break;
                default:
                    break;
            }

            PagedResult<JobGroup> result = null;
            if (!string.IsNullOrEmpty(requestParam.keyWordJobName))
            {
                result = _jobGroupRepository.FindAll(Specification<JobGroup>.Eval(jobGroup => ((requestParam.Status == 0 && jobGroup.companyID == requestParam.companyId && jobGroup.name.Contains(requestParam.keyWordJobName)) || (jobGroup.status == status && jobGroup.companyID == requestParam.companyId && jobGroup.name.Contains(requestParam.keyWordJobName)))),
                jobGroup => jobGroup.releaseDate, SortOrder.Descending, requestParam.pageIndex,
                requestParam.pageSize);
            }
            else
            {
                result = _jobGroupRepository.FindAll(Specification<JobGroup>.Eval(jobGroup => requestParam.Status == 0 && jobGroup.companyID == requestParam.companyId || jobGroup.status == status && jobGroup.companyID == requestParam.companyId),
                jobGroup => jobGroup.releaseDate, SortOrder.Descending, requestParam.pageIndex,
                requestParam.pageSize);
            }

            return
                Mapper.Instance.Map<PagedResultModel<JobInfo>>(result);
        }

        public void EditJobInfo(EditJobInfoRequestParam requestParam)
        {
            if (requestParam.JobGroupObject.jobAdrressList.Count == 0) throw new GreensJobException(0, "请提供工作地址");
            var jobGroup = _jobGroupRepository.GetByKey(requestParam.JobGroupObject.ID);
            if (jobGroup == null)
            {
                throw new GreensJobException(StatusCodes.Failure, "该职位不存在");
            }
            if (!_jobClassifyRepository.Exists(Specification<JobClassify>.Eval(x => x.ID == requestParam.JobGroupObject.jobClassifyID)))
            {
                throw new GreensJobException(StatusCodes.Failure, "非法职位类型");
            }
            if (!_jobCategoryRepository.Exists(Specification<JobCategory>.Eval(x => x.ID == requestParam.JobGroupObject.jobCategoryID)))
            {
                throw new GreensJobException(StatusCodes.Failure, "非法职位类别");
            }
            if (!_jobSchduleRepository.Exists(Specification<JobSchdule>.Eval(x => x.ID == requestParam.JobGroupObject.jobCategoryID)))
            {
                throw new GreensJobException(StatusCodes.Failure, "非法职位档期");
            }
            if (!_payCategoryRepository.Exists(Specification<PayCategory>.Eval(x => x.ID == requestParam.JobGroupObject.payCategoryID)))
            {
                throw new GreensJobException(StatusCodes.Failure, "非法支付类别");
            }
            if (!_payUnitRepository.Exists(Specification<PayUnit>.Eval(x => x.ID == requestParam.JobGroupObject.payUnitID)))
            {
                throw new GreensJobException(StatusCodes.Failure, "非法工资单位");
            }

            jobGroup.name = requestParam.JobGroupObject.name;
            jobGroup.contactMan = requestParam.JobGroupObject.contactMan;
            jobGroup.addrs = string.Join(",", requestParam.JobGroupObject.jobAdrressList.Select(address => address.addr));
            jobGroup.erollMethod = requestParam.JobGroupObject.erollMethod;
            jobGroup.startDate =
                new DateTime(requestParam.JobGroupObject.startDate.Year, requestParam.JobGroupObject.startDate.Month, requestParam.JobGroupObject.startDate.Day,
                    requestParam.JobGroupObject.startTime.Hour, requestParam.JobGroupObject.startTime.Minute, requestParam.JobGroupObject.startTime.Second);
            jobGroup.endDate =
                new DateTime(requestParam.JobGroupObject.endDate.Year, requestParam.JobGroupObject.endDate.Month, requestParam.JobGroupObject.endDate.Day,
                        requestParam.JobGroupObject.endTime.Hour, requestParam.JobGroupObject.endTime.Minute, requestParam.JobGroupObject.endTime.Second);
            jobGroup.salary = requestParam.JobGroupObject.salary;
            jobGroup.interview = requestParam.JobGroupObject.interview == 1;
            jobGroup.interviewPlace = requestParam.JobGroupObject.interviewPlace;
            jobGroup.jobCategoryID = requestParam.JobGroupObject.jobCategoryID;
            jobGroup.jobClassifyID = requestParam.JobGroupObject.jobClassifyID;
            jobGroup.jobSchduleID = requestParam.JobGroupObject.jobCategoryID;
            jobGroup.autoTimeShare = requestParam.JobGroupObject.autoTimeShare;
            jobGroup.content = requestParam.JobGroupObject.content;
            jobGroup.employer = requestParam.JobGroupObject.employer;
            jobGroup.gatheringPlace = requestParam.JobGroupObject.gatheringPlace;
            jobGroup.genderLimit = requestParam.JobGroupObject.genderLimit;
            jobGroup.healthCertificate = requestParam.JobGroupObject.healthCertificate == 1;
            jobGroup.heightLimit = GetHeightForHeightLimitCode(requestParam.JobGroupObject.heightLimit);
            jobGroup.mobileNumber = requestParam.JobGroupObject.mobileNumber;
            jobGroup.payCategoryID = requestParam.JobGroupObject.payCategoryID;
            jobGroup.payUnitID = requestParam.JobGroupObject.payUnitID;
            jobGroup.recruitNum = requestParam.JobGroupObject.recruitNum;
            jobGroup.urgent = requestParam.JobGroupObject.urgent == 1;
            jobGroup.continousDays = requestParam.JobGroupObject.continousDays;
            jobGroup.releaseDate = DateTime.Now;

            // 删除所有职位
            jobGroup.Jobs.Clear();
            foreach (var addressObject in requestParam.JobGroupObject.jobAdrressList)
            {
                var job = Mapper.Instance.Map<Job>(jobGroup);
                job.addr = addressObject.addr;
                job.lat = addressObject.lat;
                job.lng = addressObject.lng;
                job.JobGroup = jobGroup;
                job.Location = DbGeography.FromText($"POINT({addressObject.lng} {addressObject.lat})");
                job.countyName = string.Empty;
                job.City_ID = addressObject.cityId;
                job.District_ID = addressObject.districtId;
                jobGroup.Jobs.Add(job);
            }
            _jobGroupRepository.Update(jobGroup);

            // 添加新的职位
            Context.Commit();
        }

        public JobGroupObject GetJobGroupDetail(GetJobGroupDetailRequestParam requestParam)
        {
            var queryJobGroup = _jobGroupRepository.Find(Specification<JobGroup>.Eval(jopGroup => jopGroup.ID == requestParam.jobGroupID), jobGroup => jobGroup.Jobs);
            if (queryJobGroup == null)
            {
                throw new GreensJobException(0, "该职位不存在");
            }
            var result = Mapper.Instance.Map<JobGroupObject>(queryJobGroup);
            result.heightLimit = GetHeightLimitCodeForHeight(queryJobGroup.heightLimit);
            return result;
        }

        public JobModel GetJobDetail(GetJobDetailRequestParam requestParam)
        {
            var queryJob = _jobRepository.Find(Specification<Job>.Eval(jop => jop.ID == requestParam.jobId),
                job => job.JobClassify, job => job.JobCategory, job => job.PayUnit, job => job.PayCategory,
                job => job.JobRecruitDetails);
            if (queryJob == null)
            {
                throw new GreensJobException(0, "该职位不存在");
            }
            queryJob.browseNum++;
            _jobRepository.Update(queryJob);
            Context.Commit();
            var result = Mapper.Instance.Map<JobModel>(queryJob);

            result.CollectState = IsCollect(requestParam.jobId, requestParam.jsId);

            var queryEnroll =
                _enrollRepository.FindAll(enroll => enroll.JobSeeker, enroll => enroll.EnrollDetails)
                    .FirstOrDefault(
                        enroll =>
                            enroll.jobID == requestParam.jobId &&
                            (enroll.employStatus == EmployStatu.SignUp ||
                             enroll.employStatus == EmployStatu.EmployNotConfirmed ||
                             enroll.employStatus == EmployStatu.Employ) &&
                            enroll.JobSeeker.ID == requestParam.jsId);
            var applyState = queryEnroll != null;
            result.ApplyState = applyState;
            if (applyState)
            {
                result.unvailableDates = queryEnroll.EnrollDetails.Select(enrollDetail => enrollDetail.date).ToList();
            }
            var jobSeeker =
                _jobSeekerRepository.Find(
                    Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            result.shareTitle = _shareTitle.Replace("#Title#", result.jobName)
                .Replace("#Price#", $"{result.salary}{result.salaryUnit}");
            result.shareDes = _shareDes.Replace("#Name#", jobSeeker?.Resume.Name ?? "老友");
            result.shareIcon = _shareIcon;
            result.shareUrl = _shareUrl + result.jobID + $"&invitation={jobSeeker?.ID ?? 0}";
            result.isBinding = jobSeeker != null;
            result.tags = new List<string>();

            // 组合标签 颜色编码说明（01：天蓝色边框 02：粉色边框 03：绿色边框 04：百度UI的深蓝色 05：浅红色 06：紫色 07：浅绿色
            switch (result.isSex)
            {
                case 0:
                    result.tags.Add("03男女不限");
                    break;
                case 1:
                    switch (result.gender)
                    {
                        case 1:
                            result.tags.Add("01限男性");
                            break;
                        case 2:
                            result.tags.Add("02限女性");
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            if (result.isHeight)
            {
                result.tags.Add($"04身高{result.height}+");
            }
            if (result.isHealth)
            {
                result.tags.Add("05要健康证");
            }
            if (result.isInterview)
            {
                result.tags.Add("06需面试");
            }
            else
            {
                result.tags.Add("07不需面试");
            }

            return result;
        }

        public PagedResultModel<EmployeeJobInfo> GetEmployeeJobs(GetEmployeeJobsRequestParam requestParam)
        {
            if (requestParam == null) throw new ArgumentNullException(nameof(requestParam));

            var jobSeeker =
                _jobSeekerRepository.Find(Specification<JobSeeker>.Eval(x => x.ID == requestParam.jsId));
            if (jobSeeker == null)
            {
                throw new GreensJobException(StatusCodes.Forbidden, "该用户未授权");
            }

            PagedResultModel<EmployeeJobInfo> result = null;
            switch (requestParam.type)
            {
                case 1:
                    result =
                        Mapper.Instance.Map<PagedResultModel<EmployeeJobInfo>>(_jobRepository.FindAll(
                            Specification<Job>.Eval(
                                job => job.status == JobStatus.Employ && job.Collects.Any(collect => collect.jobSeekerID == jobSeeker.ID)),
                            job => job.releaseDate, SortOrder.Descending, requestParam.pageIndex, requestParam.pageSize,
                            job => job.Collects));
                    break;
                case 2:
                    var signUpPagedResult =
                        _jobRepository.FindAll(
                            Specification<Job>.Eval(
                                job => (job.status == JobStatus.Employ || job.status == JobStatus.Stop) && job.Enrolls.Any(enroll => enroll.jobSeekerID == jobSeeker.ID && (enroll.employStatus == EmployStatu.SignUp || enroll.employStatus == EmployStatu.UnEmploy))),
                            job => job.releaseDate, SortOrder.Descending, requestParam.pageIndex, requestParam.pageSize,
                            job => job.Enrolls);
                    if (signUpPagedResult != null)
                    {
                        result = new PagedResultModel<EmployeeJobInfo>();

                        result.Data = new List<EmployeeJobInfo>();
                        foreach (var job in signUpPagedResult)
                        {
                            var employeeJobInfo = Mapper.Instance.Map<EmployeeJobInfo>(job);

                            var enroll =
                                job.Enrolls.FirstOrDefault(
                                    x => x.jobSeekerID == jobSeeker.ID && (x.employStatus == EmployStatu.SignUp || x.employStatus == EmployStatu.UnEmploy));
                            if (enroll != null)
                            {
                                employeeJobInfo.isFired = enroll.employStatus == EmployStatu.UnEmploy ? 1 : 0;
                                employeeJobInfo.EnrollDates = Mapper.Instance.Map<List<EnrollDateModel>>(enroll.EnrollDetails.ToList());
                            }
                            result.Data.Add(employeeJobInfo);
                        }
                        result.PageNumber = signUpPagedResult.PageNumber;
                        result.PageSize = signUpPagedResult.PageSize;
                        result.TotalPages = signUpPagedResult.TotalPages;
                        result.TotalRecords = signUpPagedResult.TotalRecords;
                    }
                    break;
                case 3:
                    var employPagedResult =
                        _jobRepository.FindAll(
                            Specification<Job>.Eval(
                                job => (job.status == JobStatus.Employ || job.status == JobStatus.Stop) && job.Enrolls.Any(enroll => enroll.jobSeekerID == jobSeeker.ID && (enroll.employStatus == EmployStatu.EmployNotConfirmed || enroll.employStatus == EmployStatu.Employ))),
                            job => job.releaseDate, SortOrder.Descending, requestParam.pageIndex, requestParam.pageSize,
                            job => job.Enrolls);
                    if (employPagedResult != null)
                    {
                        result = new PagedResultModel<EmployeeJobInfo>();

                        result.Data = new List<EmployeeJobInfo>();
                        foreach (var job in employPagedResult)
                        {
                            var employeeJobInfo = Mapper.Instance.Map<EmployeeJobInfo>(job);

                            var enroll =
                                job.Enrolls.FirstOrDefault(
                                    x => x.jobSeekerID == jobSeeker.ID && (x.employStatus == EmployStatu.EmployNotConfirmed || x.employStatus == EmployStatu.Employ));
                            if (enroll != null)
                            {
                                employeeJobInfo.isConfirm = (enroll.employStatus == EmployStatu.Employ) ? 1 : 0;
                                employeeJobInfo.startDate = $"{enroll.EnrollDetails.Min(x => x.date):MM月dd日起}";
                                employeeJobInfo.dayNum = $"(录用{enroll.EnrollDetails.Count}天)";
                                employeeJobInfo.EnrollDates = Mapper.Instance.Map<List<EnrollDateModel>>(enroll.EnrollDetails.ToList());
                            }
                            //employeeJobInfo.EnrollDates.Clear();

                            //foreach (var employDate in employeeJobInfo.EmployDates)
                            //{
                            //    employeeJobInfo.EnrollDates.Add(Mapper.Instance.Map<EnrollDateModel>(employDate));
                            //}
                            result.Data.Add(employeeJobInfo);
                        }
                        result.PageNumber = employPagedResult.PageNumber;
                        result.PageSize = employPagedResult.PageSize;
                        result.TotalPages = employPagedResult.TotalPages;
                        result.TotalRecords = employPagedResult.TotalRecords;
                    }
                    break;
                case 4:
                    var pagedResult = _jobRepository.FindAll(
                            Specification<Job>.Eval(
                                job => job.Enrolls.Any(enroll => enroll.jobSeekerID == jobSeeker.ID && enroll.employStatus == EmployStatu.Payed)),
                            job => job.releaseDate, SortOrder.Descending, requestParam.pageIndex, requestParam.pageSize,
                            job => job.Enrolls);
                    if (pagedResult != null)
                    {
                        result = new PagedResultModel<EmployeeJobInfo>();

                        result.Data = new List<EmployeeJobInfo>();
                        foreach (var job in pagedResult)
                        {
                            var employeeJobInfo = Mapper.Instance.Map<EmployeeJobInfo>(job);

                            var enrolls =
                                job.Enrolls.Where(
                                    x => x.jobSeekerID == jobSeeker.ID && x.employStatus == EmployStatu.Payed);
                            if (enrolls != null && enrolls.Any())
                            {
                                var enrollDetails = new List<DateTime>();
                                foreach (var enroll in enrolls)
                                {
                                    enrollDetails.AddRange(enroll.EnrollDetails.Select(x => x.date));
                                }

                                employeeJobInfo.actualStartEndDate =
                                    $"{enrollDetails.Min().ToString("MM月dd日")}至{enrollDetails.Max().ToString("MM月dd日")}";
                                employeeJobInfo.actualDayNum = enrollDetails.Distinct().Count();

                                var payEnrollDetails =
                                    _enrollPayDetailRepository.FindAll(
                                        Specification<EnrollPayDetail>.Eval(
                                            x => x.Job_ID == job.ID && x.State == EnrollPayState.Paid));
                                employeeJobInfo.incomeAmount = payEnrollDetails.Any() ? payEnrollDetails.Sum(x => x.AmountSalary) : 0;
                            }
                            result.Data.Add(employeeJobInfo);
                        }
                        result.PageNumber = pagedResult.PageNumber;
                        result.PageSize = pagedResult.PageSize;
                        result.TotalPages = pagedResult.TotalPages;
                        result.TotalRecords = pagedResult.TotalRecords;
                    }
                    break;
                default:
                    break;
            }
            if (result != null)
            {
                foreach (var employeeJobInfo in result.Data)
                {
                    if (employeeJobInfo.EnrollDates != null)
                    {
                        foreach (var enrollDate in employeeJobInfo.EnrollDates)
                        {
                            enrollDate.Date += " " + employeeJobInfo.startEndTime;
                        }
                    }
                }
            }

            return result;
        }

        public PagedResultModel<GetJobsModel> SearchJobs(SearchJobsRequestParam requestParam)
        {
            throw new NotImplementedException();
        }

        public void UpdateExpiredJobStatus()
        {
            // 更新过期职位状态
            var nowTime = DateTime.Now.AddDays(1).Date.AddMilliseconds(-1);
            var expiredJobGroups =
                _jobGroupRepository.FindAll(x => x.Jobs).Where(x => x.endDate < nowTime && (x.status == JobStatus.Employ || x.status == JobStatus.PendingAudit));

            foreach (var expiredJobGroup in expiredJobGroups)
            {
                expiredJobGroup.status = JobStatus.Expired;
                _jobGroupRepository.Update(expiredJobGroup);
                foreach (var job in expiredJobGroup.Jobs)
                {
                    job.status = JobStatus.Expired;
                    _jobRepository.Update(job);
                }
            }
            UpdateNotEnrollJobStatus();
            Context.Commit();
        }

        public IEnumerable<GetJobGroupsByCompanyModel> GetJobGroupsByCompany(GetJobGroupsByCompanyRequestParam requestParam)
        {
            var result = _jobGroupRepository.FindAll(Specification<JobGroup>.Eval(x => x.companyID == requestParam.companyId && x.employNum > 0));

            return Mapper.Instance.Map<IEnumerable<GetJobGroupsByCompanyModel>>(result);
        }

        private void UpdateNotEnrollJobStatus()
        {
            var nowTime = DateTime.Now.AddDays(1).Date.AddMilliseconds(-1);
            var queryJobs =
                _jobRepository.FindAll(
                    Specification<Job>.Eval(x => x.canEnroll == 1 && x.autoTimeShare == true && (DbFunctions.DiffDays(nowTime, x.endDate) + 1) < x.continousDays));
            foreach (var queryJob in queryJobs)
            {
                queryJob.canEnroll = 0;
                _jobRepository.Update(queryJob);
            }
        }

        public void ReleaseJob(ReleaseJobRequestParam requestParam)
        {
            if (requestParam.jobAdrressList.Count == 0) throw new GreensJobException(0, "请提供工作地址");
            // TODO: 正式环境需要恢复
            //var status = JobStatus.Draft;
            //if (requestParam.status == 1)
            //{
            //    status = JobStatus.PendingAudit;
            //}
            if (!_jobClassifyRepository.Exists(Specification<JobClassify>.Eval(x => x.ID == requestParam.jobClassifyID)))
            {
                throw new GreensJobException(StatusCodes.Failure, "非法职位类型");
            }
            if (!_jobCategoryRepository.Exists(Specification<JobCategory>.Eval(x => x.ID == requestParam.jobCategoryID)))
            {
                throw new GreensJobException(StatusCodes.Failure, "非法职位类别");
            }
            if (!_jobSchduleRepository.Exists(Specification<JobSchdule>.Eval(x => x.ID == requestParam.jobCategoryID)))
            {
                throw new GreensJobException(StatusCodes.Failure, "非法职位档期");
            }
            if (!_payCategoryRepository.Exists(Specification<PayCategory>.Eval(x => x.ID == requestParam.payCategoryID)))
            {
                throw new GreensJobException(StatusCodes.Failure, "非法支付类别");
            }
            if (!_payUnitRepository.Exists(Specification<PayUnit>.Eval(x => x.ID == requestParam.payUnitID)))
            {
                throw new GreensJobException(StatusCodes.Failure, "非法工资单位");
            }
            var status = JobStatus.Employ;
            var jobGroup = new JobGroup
            {
                name = requestParam.name,
                contactMan = requestParam.contactMan,
                addrs = string.Join(",", requestParam.jobAdrressList.Select(address => address.addr)),
                erollMethod = requestParam.erollMethod,
                status = status,
                createDate = DateTime.Now,
                startDate =
                    new DateTime(requestParam.startDate.Year, requestParam.startDate.Month, requestParam.startDate.Day,
                        requestParam.startTime.Hour, requestParam.startTime.Minute, requestParam.startTime.Second),
                endDate =
                    new DateTime(requestParam.endDate.Year, requestParam.endDate.Month, requestParam.endDate.Day,
                        requestParam.endTime.Hour, requestParam.endTime.Minute, requestParam.endTime.Second),
                salary = requestParam.salary,
                interview = requestParam.interview == 1,
                interviewPlace = requestParam.interviewPlace == null ? "" : requestParam.interviewPlace,
                jobCategoryID = requestParam.jobCategoryID,
                jobSchduleID = requestParam.jobCategoryID,      // 与jobCategoryID保持一致
                jobClassifyID = requestParam.jobClassifyID,
                autoTimeShare = requestParam.autoTimeShare,
                content = requestParam.content,
                employer = requestParam.employer == null ? "" : requestParam.employer,
                gatheringPlace = requestParam.gatheringPlace == null ? "" : requestParam.gatheringPlace,
                genderLimit = requestParam.genderLimit,
                healthCertificate = requestParam.healthCertificate == 1,
                heightLimit = GetHeightForHeightLimitCode(requestParam.heightLimit),
                mobileNumber = requestParam.mobileNumber,
                payCategoryID = requestParam.payCategoryID,
                payUnitID = requestParam.payUnitID,
                publisherID = requestParam.userId,
                recruitNum = requestParam.recruitNum,
                releaseDate = DateTime.Now,
                urgent = requestParam.urgent == 1,
                continousDays = requestParam.continousDays,
                applicantNum = 0,
                employNum = 0,
                offineEmployNum = 0,
                onlineEmployNum = 0,
                companyID = requestParam.companyId
            };

            foreach (var addressObject in requestParam.jobAdrressList)
            {
                var job = Mapper.Instance.Map<Job>(jobGroup);
                job.addr = addressObject.addr;
                job.lat = addressObject.lat;
                job.lng = addressObject.lng;
                job.JobGroup = jobGroup;
                job.Location = DbGeography.FromText($"POINT({addressObject.lng} {addressObject.lat})");
                job.countyName = string.Empty;
                job.City_ID = addressObject.cityId;
                job.District_ID = addressObject.districtId;
                job.canEnroll = 1;
                jobGroup.Jobs.Add(job);
            }

            _jobGroupRepository.Add(jobGroup);
            Context.Commit();
            //if (_jobService.AddObject(list) > 0)
            //{
            //    throw new GreensJobException(1, "职位信息已经成功提交");
            //}
            //else
            //{
            //    throw new GreensJobException(0, "后台操作失败");
            //}
        }
    }
}
