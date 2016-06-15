using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Apworks.Repositories;
using Glz.GreensJob.Domain.IApplication;
using Glz.Infrastructure;

namespace Glz.GreensJob.Domain.Application.Services
{
    public class WeiXinMessage : ApplicationService, IWeiXinMessage
    {
        private readonly string _host = ConfigurationManager.AppSettings["MobileHost"];
        public WeiXinMessage(IRepositoryContext context) : base(context)
        {
        }

        public void SendEmployResultNotice(string openId, string workTime, string canceljobId, string canceljobName, string canceljobStatus, string confirmJobName)
        {
            using (var proxy = new HttpClient())
            {
                proxy.BaseAddress = new Uri(_host);
                var result = proxy.GetAsync($"WeiXin/SendEmployResultNotice?openId={openId}&workTime={workTime}&canceljobId={canceljobId}&canceljobName={canceljobName}&canceljobStatus={canceljobStatus}&confirmJobName={confirmJobName}").Result;
                if (result.StatusCode != HttpStatusCode.OK) return;
                var resultData = result.Content.ReadAsAsync<ResultBase>().Result;
                if (resultData.code != StatusCodes.Success)
                {
                    throw new Exception($"发送微信消息失败：{resultData.message}");
                }
            }
        }

        public void SendEmploySuccessNotice(string openId, string jobName, string companyName, string salary, string workTime, string workPlace)
        {
            using (var proxy = new HttpClient())
            {
                proxy.BaseAddress = new Uri(_host);
                var result = proxy.GetAsync($"WeiXin/SendEmploySuccessNotice?openId={openId}&jobName={jobName}&companyName={companyName}&salary={salary}&workTime={workTime}&workPlace={workPlace}").Result;
                if (result.StatusCode != HttpStatusCode.OK) return;
                var resultData = result.Content.ReadAsAsync<ResultBase>().Result;
                if (resultData.code != StatusCodes.Success)
                {
                    throw new Exception($"发送微信消息失败：{resultData.message}");
                }
            }
        }
    }
}
