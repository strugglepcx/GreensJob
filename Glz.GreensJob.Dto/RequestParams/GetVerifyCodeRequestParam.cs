using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using Glz.Infrastructure;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// GetVerifyCode请求参数
    /// </summary>
    public class GetVerifyCodeRequestParam : RequestParamBase
    {
        /// <summary>
        /// 用户手机号
        /// </summary>
        [Required]
        public string userMobileNumber { get; set; }
        /// <summary>
        /// 1 学生端绑定 2 企业端 3 设置提现密码 4 学生端注册 5学生端登陆 6学生端找回密码
        /// </summary>
        public int platform { get; set; }
    }
}