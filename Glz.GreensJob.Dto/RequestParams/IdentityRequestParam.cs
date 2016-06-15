using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Glz.Infrastructure;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 带身份证认证的参数
    /// </summary>
    public class IdentityRequestParam : RequestParamBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public int userId { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        [Required]
        public int companyId { get; set; }
        /// <summary>
        /// 会话Id
        /// </summary>
        [Required]
        public string sessionId { get; set; }
    }
}