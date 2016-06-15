using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Glz.Infrastructure;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 获取职位详情请求参数
    /// </summary>
    public class GetJobDetailRequestParam : WeiXinIdentityRequestParam
    {
        /// <summary>
        /// 工作Id
        /// </summary>
        public int jobId { get; set; }
    }
}
