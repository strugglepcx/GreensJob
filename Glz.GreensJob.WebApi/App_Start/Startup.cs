using System.Configuration;
using Glz.Infrastructure;
using Hangfire;
using Hangfire.SqlServer.RabbitMQ;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Glz.GreensJob.WebApi.App_Start.Startup))]

namespace Glz.GreensJob.WebApi.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            GlobalConfiguration.Configuration.UseSqlServerStorage("Hangfire").Entry.UseRabbitMq(configuration =>
            {
                configuration.HostName = ConfigurationManager.AppSettings["RabbitMqHost"];
                configuration.Port = ConfigurationManager.AppSettings["RabbitMqPort"].GetIntOrDefault(0);
                configuration.Username = ConfigurationManager.AppSettings["RabbitMqUserName"];
                configuration.Password = ConfigurationManager.AppSettings["RabbitMqPassword"];
            });

            //GlobalConfiguration.Configuration

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            HangfireConfig.Initialize();
        }
    }
}
