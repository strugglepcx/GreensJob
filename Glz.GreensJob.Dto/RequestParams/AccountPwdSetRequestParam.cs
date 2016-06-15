using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 提现密码设置请求参数
    /// </summary>
    public class AccountPwdSetRequestParam : WeiXinIdentityRequestParam
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string verificationCode { get; set; }
        /// <summary>
        /// 设置密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string userMobileNumber { get; set; }
    }
}
