using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Glz.Infrastructure.Logging.Models;

namespace Glz.Infrastructure.Logging
{

    public class HttpLogging : ILogging
    {
        private readonly string _host = ConfigurationManager.AppSettings["LoggingHost"];

        public void LogAction(ActionLogModel actionLogModel)
        {
            using (var proxy = new HttpClient())
            {
                proxy.BaseAddress = new Uri(_host);
                var result = proxy.PostAsJsonAsync("api/logs/v1/logAction", actionLogModel).Result;
                if (result.StatusCode != HttpStatusCode.OK) return;
                var resultData = result.Content.ReadAsAsync<ResultBase>().Result;
                if (resultData.code != StatusCodes.Success)
                {
                    throw new Exception($"写日志失败：{resultData.message}");
                }
            }
        }

        public void LogException(ActionExceptionLogModel actionExceptionLogModel)
        {
            using (var proxy = new HttpClient())
            {
                proxy.BaseAddress = new Uri(_host);
                var result = proxy.PostAsJsonAsync("api/logs/v1/LogException", actionExceptionLogModel).Result;
                if (result.StatusCode != HttpStatusCode.OK) return;
                var resultData = result.Content.ReadAsAsync<ResultBase>().Result;
                if (resultData.code != StatusCodes.Success)
                {
                    throw new Exception($"写日志失败：{resultData.message}");
                }
            }
        }
    }
}
