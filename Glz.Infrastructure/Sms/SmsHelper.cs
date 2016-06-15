using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.Infrastructure.Sms
{
    public class SmsHelper
    {
        /// <summary>
        /// 发送验证码短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static long SendValidationCode(string mobile, string code)
        {
            return DoRequest.HttpPostWebService("http://api.bjszrk.com/sdk/WS.asmx", "BatchSend",
                mobile, string.Format(DoRequest.ValidationBodyFormat, code), "", "");
        }

        /// <summary>
        /// 发送验证码短信
        /// </summary>
        /// <returns></returns>
        public static long SendEmploySuccessNotice(string mobile, string jobName, string firstWorkDate, string workPlace)
        {
            return DoRequest.HttpPostWebService("http://api.bjszrk.com/sdk/WS.asmx", "BatchSend",
                mobile, string.Format(DoRequest.EmployConfirmBodyFormat, jobName, firstWorkDate, workPlace), "", "");
        }
    }
}
