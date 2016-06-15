using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.Infrastructure;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 注册请求参数
    /// </summary>
    public class RegisterRequestParam : RequestParamBase
    {
        /// <summary>
        /// 创建一个 <c>RegisterRequestParam</c> 类型实例
        /// </summary>
        public RegisterRequestParam()
        {

        }

        /// <summary>
        /// 验证码
        /// </summary>
        public string verificationCode { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string userMobileNumber { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 微信openId
        /// </summary>
        public string openId { get; set; }
        /// <summary>
        /// 邀请人Id
        /// </summary>
        public int invitation { get; set; }
        /// <summary>
        /// 是否手机登陆（0：其他，1：手机）
        /// </summary>
        public int isMobile { get; set; }
        /// <summary>
        /// baidu推送channelId
        /// </summary>
        public string channelId { get; set; }
    }
}
