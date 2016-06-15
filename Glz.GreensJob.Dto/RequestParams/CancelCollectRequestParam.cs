using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 取消收藏
    /// </summary>
    public class CancelCollectRequestParam : WeiXinIdentityRequestParam
    {
        /// <summary>
        /// 职位Id
        /// </summary>
        public int jobId { get; set; }
    }
}
