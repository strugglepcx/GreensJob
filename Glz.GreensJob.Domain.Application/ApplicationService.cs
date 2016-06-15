using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;
using Apworks.Repositories;
using AutoMapper;
using Glz.GreensJob.Domain.Models;
using Glz.GreensJob.Dto;
using Glz.Infrastructure;
using Glz.GreensJob.Dto.RequestParams;

namespace Glz.GreensJob.Domain.Application
{
    /// <summary>
    /// 表示应用层服务的抽象类。
    /// </summary>
    public abstract class ApplicationService : DisposableObject
    {
        #region Private Fields

        private readonly IRepositoryContext _context;
        //private static readonly ILog log = LogManager.GetLogger("VvGotHome.Logger");

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个<c>ApplicationService</c>类型的实例。
        /// </summary>
        /// <param name="context">用来初始化<c>ApplicationService</c>类型的仓储上下文实例。</param>
        protected ApplicationService(IRepositoryContext context)
        {
            _context = context;
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// 获取当前应用层服务所使用的仓储上下文实例。
        /// </summary>
        protected IRepositoryContext Context
        {
            get { return _context; }
        }

        #endregion

        #region Protected Methods

        protected override void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// 根据身高限制代码返回身高
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        protected int GetHeightForHeightLimitCode(int code)
        {
            switch (code)
            {
                case 1:
                    return 158;
                case 2:
                    return 160;
                case 3:
                    return 165;
                case 4:
                    return 170;
                case 5:
                    return 175;
                case 6:
                    return 180;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// 根据身高返回身高限制代码
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        protected int GetHeightLimitCodeForHeight(int height)
        {
            switch (height)
            {
                case 158:
                    return 1;
                case 160:
                    return 2;
                case 165:
                    return 3;
                case 170:
                    return 4;
                case 175:
                    return 5;
                case 180:
                    return 6;
                default:
                    return 0;
            }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// 对应用层服务进行初始化。
        /// </summary>
        /// <remarks>包含的初始化任务有：
        /// 1. AutoMapper框架的初始化</remarks>
        public static void Initialize()
        {
            Mapper.Initialize(configuration =>
            {
                configuration.CreateMap<AgencyRecruitJob, AgencyRecruitJobObject>();
                configuration.CreateMap<City, CityObject>();
                configuration.CreateMap<Collect, CollectObject>();
                configuration.CreateMap<Company, CompanyObject>();
                configuration.CreateMap<Complaint, ComplaintObject>();
                configuration.CreateMap<County, CountyObject>();
                configuration.CreateMap<Dept, DeptObject>();
                configuration.CreateMap<EnrollDetail, EnrollDetailObject>();
                configuration.CreateMap<Enroll, EnrollObject>()
                    .ForMember(x => x.JobSeeker, y => y.MapFrom(s => s.JobSeeker))
                    .ForMember(x => x.name, y => y.MapFrom(s => s.Job.addr))
                    .ForMember(x => x.mobile, y => y.MapFrom(s => s.mobile))
                    .ForMember(x => x.state, y => y.MapFrom(s => s.employStatus))
                    .ForMember(x => x.method, y => y.MapFrom(s => s.enrollMethod))
                    .ForMember(x => x.EnrollDetails, y => y.MapFrom(s => s.EnrollDetails));
                configuration.CreateMap<JobClassify, JobClassifyObject>();
                configuration.CreateMap<Job, JobObject>()
                    .ForMember(d => d.jobCategoryName,
                        m => m.MapFrom(s => s.JobCategory != null ? s.JobCategory.name : string.Empty));
                configuration.CreateMap<JobSchdule, JobSchduleObject>();
                configuration.CreateMap<PayCategory, PayCategoryObject>();
                configuration.CreateMap<PayUnit, PayUnitObject>();
                configuration.CreateMap<Province, ProvinceObject>();
                configuration.CreateMap<Publisher, PublisherObject>();
                configuration.CreateMap<VerificationCode, VerificationCodeObject>();
                configuration.CreateMap<PublisherRight, PublisherRightObject>()
                    .ForMember(x => x.ID, y => y.MapFrom(s => s.ID))
                    .ForMember(x => x.AddUser, y => y.MapFrom(s => s.AddUserRight))
                    .ForMember(x => x.DeleteUser, y => y.MapFrom(s => s.DeleteUserRight))
                    .ForMember(x => x.Finicial, y => y.MapFrom(s => s.FinicialRight))
                    .ForMember(x => x.ImportEmployee, y => y.MapFrom(s => s.ImportEmployeeRight))
                    .ForMember(x => x.ModifyUser, y => y.MapFrom(s => s.ModifyUserRight))
                    .ForMember(x => x.ReleaseJob, y => y.MapFrom(s => s.ReleaseJobRight));
                configuration.CreateMap<PublisherRightObject, PublisherRight>()
                    .ForMember(x => x.ID, y => y.MapFrom(s => s.ID))
                    .ForMember(x => x.AddUserRight, y => y.MapFrom(s => s.AddUser))
                    .ForMember(x => x.DeleteUserRight, y => y.MapFrom(s => s.DeleteUser))
                    .ForMember(x => x.FinicialRight, y => y.MapFrom(s => s.Finicial))
                    .ForMember(x => x.ImportEmployeeRight, y => y.MapFrom(s => s.ImportEmployee))
                    .ForMember(x => x.ModifyUserRight, y => y.MapFrom(s => s.ModifyUser))
                    .ForMember(x => x.ReleaseJobRight, y => y.MapFrom(s => s.ReleaseJob));
                configuration.CreateMap<JobGroup, Job>().ForMember(d => d.ID, m => m.Ignore());
                configuration.CreateMap<JobGroup, JobInfo>()
                    .ForMember(d => d.jobGroupID, m => m.MapFrom(s => s.ID))
                    .ForMember(d => d.jobName, m => m.MapFrom(s => s.name))
                    .ForMember(d => d.releaseStatus, m => m.MapFrom(s => s.status))
                    .ForMember(d => d.jobClass, m => m.MapFrom(s => s.jobCategoryID))
                    .ForMember(d => d.jobSchedule, m => m.MapFrom(s => s.jobSchduleID))
                    .ForMember(d => d.unitPrice, m => m.MapFrom(s => s.salary))
                    .ForMember(d => d.unitName, m => m.MapFrom(s => s.PayUnit.name))
                    .ForMember(d => d.recruitmentNum, m => m.MapFrom(s => s.recruitNum))
                    .ForMember(d => d.releaseDate, m => m.MapFrom(s => s.releaseDate))
                    .ForMember(d => d.startdate, m => m.MapFrom(s => (s.endDate.Date - s.startDate.Date).Days + 1));
                configuration.CreateMap<PagedResult<JobGroup>, PagedResultModel<JobInfo>>();
                configuration.CreateMap<JobGroup, JobGroupObject>()
                    .ForMember(d => d.jobAdrressList, m => m.MapFrom(s => s.Jobs))
                    .ForMember(d => d.urgent, m => m.MapFrom(s => s.urgent ? 1 : 0))
                    .ForMember(d => d.healthCertificate, m => m.MapFrom(s => s.healthCertificate ? 1 : 0))
                    .ForMember(d => d.interview, m => m.MapFrom(s => s.interview ? 1 : 0))
                    .ForMember(d => d.jobAdrressList, m => m.MapFrom(s => s.Jobs));
                configuration.CreateMap<Job, AddressObject>()
                    .ForMember(d => d.cityId, m => m.MapFrom(s => s.City_ID))
                    .ForMember(d => d.districtId, m => m.MapFrom(s => s.District_ID));
                configuration.CreateMap<JobSeeker, JobSeekerObject>()
                    .ForMember(d => d.nickName, m => m.MapFrom(s => s.Resume.Name));
                configuration.CreateMap<Job, GetJobsModel>()
                    .ForMember(d => d.Id, m => m.MapFrom(s => s.ID))
                    .ForMember(d => d.@class, m => m.MapFrom(s => s.jobClassifyID))
                    .ForMember(d => d.jobClass, m => m.MapFrom(s => s.jobClassifyID))
                    .ForMember(d => d.jobClassName, m => m.MapFrom(s => s.JobClassify.name))
                    .ForMember(d => d.JobTitle, m => m.MapFrom(s => s.jobCategoryID == 5 ? $"{s.name}" : $"【{s.JobCategory.name}】{s.name}"))
                    .ForMember(d => d.jobDes, m => m.MapFrom(s => s.content))
                    .ForMember(d => d.releaseTime, m => m.MapFrom(s => s.releaseDate))
                    .ForMember(d => d.startDate, m => m.MapFrom(s => $"{s.startDate:MM月dd日起}"))
                    .ForMember(d => d.dayNum, m => m.MapFrom(s => $"(共{(s.endDate.Date - s.startDate.Date).Days + 1}天)"))
                    .ForMember(d => d.salary, m => m.MapFrom(s => $"{s.salary}{s.PayUnit.name}"))
                    .ForMember(d => d.districtName, m => m.MapFrom(s => s.District.Name))
                    .ForMember(d => d.clearanceMethod, m => m.MapFrom(s => s.PayCategory.name))
                    .ForMember(d => d.isSex, m => m.MapFrom(s => s.genderLimit < 3 ? 1 : 0))
                    .ForMember(d => d.gender, m => m.MapFrom(s => s.genderLimit))
                    .ForMember(d => d.nowToEndDateDayNum, m => m.MapFrom(s => (s.endDate.Date - DateTime.Now.Date).Days + 1));
                configuration.CreateMap<PagedResult<Job>, PagedResultModel<GetJobsModel>>();
                configuration.CreateMap<SearchRecord, SearchRecordModel>();
                configuration.CreateMap<PagedResult<SearchRecord>, PagedResultModel<SearchRecordModel>>();
                configuration.CreateMap<JobRecruitDetail, JobRecruitDetailModel>()
                    .ForMember(d => d.Date, m => m.MapFrom(s => s.RecruitDate));
                configuration.CreateMap<PagedResult<JobRecruitDetailModel>, PagedResultModel<JobRecruitDetailModel>>();
                configuration.CreateMap<Job, JobModel>()
                    .ForMember(d => d.jobID, m => m.MapFrom(s => s.ID))
                    .ForMember(d => d.jobName, m => m.MapFrom(s => s.jobCategoryID == 5 ? $"{s.name}" : $"【{s.JobCategory.name}】{s.name}"))
                        .ForMember(d => d.JobClass, m => m.MapFrom(s => s.JobClassify.name))
                        .ForMember(d => d.CountyName, m => m.MapFrom(s => s.countyName))
                        .ForMember(d => d.salary, m => m.MapFrom(s => s.salary))
                        .ForMember(d => d.salaryUnit, m => m.MapFrom(s => s.PayUnit.name))
                        .ForMember(d => d.clearanceMethod, m => m.MapFrom(s => s.PayCategory.name))
                        .ForMember(d => d.address, m => m.MapFrom(s => s.addr))
                        .ForMember(d => d.clearanceMethod, m => m.MapFrom(s => s.PayCategory.name))
                        .ForMember(d => d.startTime, m => m.MapFrom(s => s.startDate.ToString("HH:mm")))
                        .ForMember(d => d.endtime, m => m.MapFrom(s => s.endDate.ToString("HH:mm")))
                        .ForMember(d => d.startDate, m => m.MapFrom(s => s.startDate.ToString("yyyy-MM-dd")))
                        .ForMember(d => d.endDate, m => m.MapFrom(s => s.endDate.ToString("yyyy-MM-dd")))
                        .ForMember(d => d.isInterview, m => m.MapFrom(s => s.interview))
                        .ForMember(d => d.isSex, m => m.MapFrom(s => s.genderLimit < 3 ? 1 : 0))
                        .ForMember(d => d.isHeight, m => m.MapFrom(s => s.heightLimit > 0))
                        .ForMember(d => d.isHealth, m => m.MapFrom(s => s.healthCertificate))
                        .ForMember(d => d.contactMan, m => m.MapFrom(s => s.contactMan))
                        .ForMember(d => d.Phone, m => m.MapFrom(s => s.mobileNumber))
                        .ForMember(d => d.Content, m => m.MapFrom(s => s.content))
                        .ForMember(d => d.Persons, m => m.MapFrom(s => s.recruitNum))
                        .ForMember(d => d.applicantNumber, m => m.MapFrom(s => s.applicantNum))
                        .ForMember(d => d.CreateDate, m => m.MapFrom(s => s.releaseDate))
                        .ForMember(d => d.CompanyName, m => m.MapFrom(s => s.Company.name))
                        .ForMember(d => d.gender, m => m.MapFrom(s => s.genderLimit))
                        .ForMember(d => d.height, m => m.MapFrom(s => s.heightLimit))
                        .ForMember(d => d.jobLatitude, m => m.MapFrom(s => s.lat))
                        .ForMember(d => d.jobLongitude, m => m.MapFrom(s => s.lng))
                        .ForMember(d => d.nowToStartDateDayNum,
                            m =>
                                m.MapFrom(
                                    s =>
                                        DateTime.Now.Date < s.startDate.Date
                                            ? (s.startDate.Date - DateTime.Now.Date).Days
                                            : 0))
                        .ForMember(d => d.dayNum,
                            m =>
                                m.MapFrom(
                                    s =>
                                        (s.endDate.Date -
                                         (s.startDate.Date > DateTime.Now.Date ? s.startDate.Date : DateTime.Now.Date)).Days))
                        .ForMember(d => d.continousDays, m => m.MapFrom(s => s.continousDays));
                configuration.CreateMap<Job, EmployeeJobInfo>()
                    .ForMember(d => d.jobId, m => m.MapFrom(s => s.ID))
                    .ForMember(d => d.jobName, m => m.MapFrom(s => s.name))
                    .ForMember(d => d.JobClassId, m => m.MapFrom(s => s.JobClassify.ID))
                    .ForMember(d => d.JobClass, m => m.MapFrom(s => s.JobClassify.name))
                    .ForMember(d => d.salary, m => m.MapFrom(s => s.salary))
                    .ForMember(d => d.salaryUnit, m => m.MapFrom(s => s.PayUnit.name))
                    .ForMember(d => d.payMethod, m => m.MapFrom(s => s.PayCategory.name))
                    .ForMember(d => d.startDate, m => m.MapFrom(s => $"{s.startDate:MM月dd日起}"))
                    .ForMember(d => d.dayNum,
                        m => m.MapFrom(s => $"(工作{(s.endDate.Date - s.startDate.Date).Days + 1}天)"))
                    .ForMember(d => d.startEndTime, m => m.MapFrom(s => $"{s.startDate:HH:mm}-{s.endDate:HH:mm}"))
                    .ForMember(d => d.jobAddress, m => m.MapFrom(s => s.District.Name));
                configuration.CreateMap<EnrollDetail, EnrollDateModel>()
                    .ForMember(d => d.Date, m => m.MapFrom(s => s.date.ToString("MM-dd")));
                configuration.CreateMap<PagedResult<Job>, PagedResultModel<EmployeeJobInfo>>();
                configuration.CreateMap<City, CityModel>();
                configuration.CreateMap<OpenCity, CityModel>();
                configuration.CreateMap<JobSeekerConfig, ConfigurationModel>();
                configuration.CreateMap<JobSeekerWalletActionLog, JobSeekerWalletActionLogModel>()
                    .ForMember(d => d.jobName, m => m.MapFrom(s => s.JobGroupName))
                    .ForMember(d => d.successTrans, m => m.MapFrom(s => s.State == 1))
                    .ForMember(d => d.transactionType, m => m.MapFrom(s => s.Amount < 0 ? 1 : 2))
                    .ForMember(d => d.transactionAmount, m => m.MapFrom(s => s.Amount))
                    .ForMember(d => d.transactionTime, m => m.MapFrom(s => s.CreateTime));
                configuration
                    .CreateMap<PagedResult<JobSeekerWalletActionLog>, PagedResultModel<JobSeekerWalletActionLogModel>>();
                configuration.CreateMap<JobSeekerMessage, JobSeekerMessageModel>();
                configuration.CreateMap<PagedResult<JobSeekerMessage>, PagedResultModel<JobSeekerMessageModel>>();
                configuration.CreateMap<University, UniversityObject>();
                configuration.CreateMap<Resume, ResumeObject>()
                    .ForMember(d => d.UniversityName, m => m.MapFrom(s => s.University.name))
                    .ForMember(d => d.Age, m => m.MapFrom(s => s.Birthday.Date == DateTime.Parse("1900-01-01") ? 0 : (DateTime.Now.Year - s.Birthday.Year)))
                    .ForMember(d => d.CityName, m => m.MapFrom(s => s.OpenCity.Name));
                configuration.CreateMap<ResumeObject, Resume>();
                configuration.CreateMap<GetJobsRequestParam, GetJobsOrderByRelaseDateRequestParam>();
                configuration.CreateMap<EnrollPayDetail, EnrollPayDetailModel>();
                configuration.CreateMap<PagedResult<EnrollPayDetail>, PagedResultModel<EnrollPayDetailModel>>();
                configuration.CreateMap<ExtractApply, JobSeekerWalletActionLogModel>();
                configuration.CreateMap<PagedResult<ExtractApply>, PagedResultModel<JobSeekerWalletActionLogModel>>();
                configuration.CreateMap<JobDraft, JobDraftObject>();
                configuration.CreateMap<PagedResult<JobDraft>, PagedResultModel<JobDraftObject>>();
                configuration.CreateMap<EnrollPayDetail, EnrollPayDetail>();
                configuration.CreateMap<JobGroup, GetJobGroupsByCompanyModel>();
                configuration.CreateMap<EnrollDateModel, EnrollDateModel>();
                configuration.CreateMap<EnrollEmployDetail, EnrollDateModel>()
                    .ForMember(d => d.Date, m => m.MapFrom(s => s.Date.ToString("MM-dd")));
                configuration.CreateMap<ExtractApply, ExtractApplyModel>()
                    .ForMember(d => d.Mobile, m => m.MapFrom(s => s.JobSeeker.mobile));
                configuration.CreateMap<PagedResult<Enroll>, PagedResultModel<EnrollObject>>();
            });
        }

        #endregion
    }
}
