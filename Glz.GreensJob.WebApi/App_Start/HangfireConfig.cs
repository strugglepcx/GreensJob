using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glz.GreensJob.Domain.IApplication;
using Hangfire;
using Hangfire.Server;

namespace Glz.GreensJob.WebApi
{
    /// <summary>
    /// Hangfire配置
    /// </summary>
    public class HangfireConfig
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize()
        {
            RecurringJob.AddOrUpdate<IJobService>(jobService => jobService.UpdateExpiredJobStatus(), Cron.Daily(0, 0), TimeZoneInfo.Local);
        }
    }
}