using System.Web.Http;
using Apworks.Application;
using Apworks.Config.Fluent;
using Apworks.Repositories;
using Apworks.Repositories.EntityFramework;
using Glz.GreensJob.Logs.Application;
using Glz.GreensJob.Logs.Application.Services;
using Glz.GreensJob.Logs.Domain.Repositories;
using Glz.GreensJob.Logs.IApplication;
using Glz.Infrastructure.Caching;
using Glz.Infrastructure.Maps;
using Microsoft.Practices.Unity;
using Unity.WebApi;

namespace Glz.GreensJob.Logs.WebApi
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
                    unityContainer.RegisterInstance(new GreensJobLogDbContext(), new PerResolveLifetimeManager())
                        .RegisterType<IRepositoryContext, EntityFrameworkRepositoryContext>(
                            new HierarchicalLifetimeManager(),
                            new InjectionConstructor(new ResolvedParameter<GreensJobLogDbContext>()))
                        .RegisterType(typeof(IRepository<,>), typeof(EntityFrameworkRepository<,>))
                        .RegisterType<IActionLogService, ActionLogService>()
                        .RegisterType<ICache, RedisCache>()
                        .RegisterType<IMapPlace, TencentMapPlace>();

                    GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(unityContainer);
                })
                .Start();
            ApplicationService.Initialize();
        }
    }
}