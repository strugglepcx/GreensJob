using Glz.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Domain.IApplication
{
    /// <summary>
    /// 微信发送消息服务
    /// </summary>
    public interface IWeiXinMessage : IApplicationServiceContract
    {
        /// <summary>
        /// 发送成功录用通知
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="jobName"></param>
        /// <param name="companyName"></param>
        /// <param name="salary"></param>
        /// <param name="workTime"></param>
        /// <param name="workPlace"></param>
        void SendEmploySuccessNotice(string openId, string jobName, string companyName, string salary, string workTime, string workPlace);

        /// <summary>
        /// 发送录用结果通知
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="workTime"></param>
        /// <param name="canceljobId"></param>
        /// <param name="canceljobName"></param>
        /// <param name="canceljobStatus"></param>
        /// <param name="confirmJobName"></param>
        void SendEmployResultNotice(string openId, string workTime, string canceljobId, string canceljobName,
            string canceljobStatus, string confirmJobName);
    }
}
