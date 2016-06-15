using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apworks.Application;
using Apworks.Config.Fluent;
using Apworks.Repositories;
using Apworks.Repositories.EntityFramework;
using Glz.GreensJob.Domain.Repositories;
using Glz.GreensJob.Domain.Application;
using Glz.GreensJob.Domain.Application.Services;
using Glz.GreensJob.Domain.IApplication;
using Glz.Infrastructure.Caching;
using Glz.Infrastructure.InterceptionBehaviors;
using Glz.Infrastructure.Locking;
using Glz.Infrastructure.Logging;
using Glz.Infrastructure.Maps;
using Hangfire;
using Hangfire.Unity;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Unity.WebApi;
using GlobalConfiguration = System.Web.Http.GlobalConfiguration;
using Glz.Infrastructure.Sms;

namespace Glz.GreensJob.WebApi
{
    public class ApworksConfig
    {
        public static void Initialize()
        {
            AppRuntime
                .Instance
                .ConfigureApworks()
                .UsingUnityContainerWithDefaultSettings()
                .Create((sender, e) =>
                {
                    var unityContainer = e.ObjectContainer.GetWrappedContainer<UnityContainer>();
                    unityContainer.AddNewExtension<Interception>();
                    unityContainer.RegisterInstance(new GreensJobDbContext(), new PerResolveLifetimeManager())
                        .RegisterType<IRepositoryContext, EntityFrameworkRepositoryContext>(
                            new HierarchicalLifetimeManager(),
                            new InjectionConstructor(new ResolvedParameter<GreensJobDbContext>()))
                        .RegisterType(typeof(IRepository<,>), typeof(EntityFrameworkRepository<,>))
                        .RegisterType<ICache, RedisCache>()
                        .RegisterType<ICacheProvider, RedisCacheProvider>()
                        .RegisterType<ILogging, HttpLogging>()
                        .RegisterType<IJobCategoryService, JobCategoryService>()
                        .RegisterType(typeof(IDeptService), typeof(DeptService))
                        .RegisterType(typeof(IPublisherService), typeof(PublisherService))
                        .RegisterType(typeof(IVerificationCodeService), typeof(VerificationCodeService))
                        .RegisterType(typeof(IProvinceService), typeof(ProvinceService))
                        .RegisterType(typeof(ICityService), typeof(CityService))
                        .RegisterType(typeof(ICountyService), typeof(CountyService))
                        .RegisterType(typeof(IAgencyRecruitJobService), typeof(AgencyRecruitJobService))
                        .RegisterType<IJobGroupService, JobGroupService>(new Interceptor<InterfaceInterceptor>(),
                            new InterceptionBehavior<CachingBehavior>())
                        .RegisterType<IJobService, JobService>(new Interceptor<InterfaceInterceptor>(),
                            new InterceptionBehavior<CachingBehavior>())
                        .RegisterType(typeof(IJobCategoryService), typeof(JobCategoryService))
                        .RegisterType(typeof(IJobClassifyService), typeof(JobClassifyService))
                        .RegisterType(typeof(IJobSchduleService), typeof(JobSchduleService))
                        .RegisterType(typeof(IPayCategoryService), typeof(PayCategoryService))
                        .RegisterType(typeof(IPayUnitService), typeof(PayUnitService))
                        .RegisterType(typeof(ICollectService), typeof(CollectService))
                        .RegisterType<IEnrollService, EnrollService>(new Interceptor<InterfaceInterceptor>(),
                            new InterceptionBehavior<LockingBehavior>())
                        .RegisterType(typeof(IEnrollDetailService), typeof(EnrollDetailService))
                        .RegisterType(typeof(IEnrollPayService), typeof(EnrollPayService))
                        .RegisterType<ICompanyService, CompanyService>()
                        .RegisterType(typeof(IComplaintService), typeof(ComplaintService))
                        .RegisterType<IFeedBackService, FeedBackService>()
                        .RegisterType<IWalletService, WalletService>()
                        .RegisterType<IResumeService, ResumeService>()
                        .RegisterType<ISearchRecordService, SearchRecordService>()
                        .RegisterType<IMapPlace, TencentMapPlace>()
                        .RegisterType<IUniversityService, UniversityService>()
                        .RegisterType<IJobQueryService, JobQueryService>(new Interceptor<InterfaceInterceptor>(),
                            new InterceptionBehavior<CachingBehavior>())
                        .RegisterType<IWeiXinMessage, WeiXinMessage>()
                        .RegisterType<ISmsMessage, SmsMessage>()
                        .RegisterType<IJobSeekerService, JobSeekerService>()
                        .RegisterType(typeof(IJobDraftService), typeof(JobDraftService))
                        .RegisterType(typeof(IJobRecruitDetailService), typeof(JobRecruitDetailService))
                        .RegisterType<ILocking, RedisLocking>()
                        ;

                    GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(unityContainer);
                    JobActivator.Current = new UnityJobActivator(unityContainer);
                })
                .Start();
            ApplicationService.Initialize();
        }
    }
}