using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 关注请求参数
    /// </summary>
    public class AttentionRequestParam : WeiXinIdentityRequestParam
    {
        /// <summary>
        /// 公司Id
        /// </summary>
        public int companyId { get; set; }
    }
}
