using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 取消关注公司请求参数
    /// </summary>
    public class CancelAttentionRequestParam : WeiXinIdentityRequestParam
    {
        /// <summary>
        /// 公司Id
        /// </summary>
        public int companyId { get; set; }
    }
}
