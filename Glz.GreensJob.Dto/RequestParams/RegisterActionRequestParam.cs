using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models.RequestParams
{
    /// <summary>
    /// 会员注册
    /// </summary>
    public class RegisterActionRequestParam : RequestParamBase
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string verificationCode { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string userMobileNumber { get; set; }
        /// <summary>
        /// 客户输入的密码
        /// </summary>
        public string password { get; set; }
    }
}