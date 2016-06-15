using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.Infrastructure;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 微信身份认证参数
    /// </summary>
    public class WeiXinIdentityRequestParam : RequestParamBase
    {
        /// <summary>
        /// 微信OpenId
        /// </summary>
        public string openId { get; set; }
        /// <summary>
        /// 求职者Id
        /// </summary>
        public int jsId { get; set; }
        /// <summary>
        /// 用户凭证
        /// </summary>
        public string sessionId { get; set; }
    }
}
