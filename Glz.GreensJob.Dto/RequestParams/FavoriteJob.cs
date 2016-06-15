using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.Infrastructure;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 用户收藏职位
    /// </summary>
    public class FavoriteJobRequestParam : WeiXinIdentityRequestParam
    {
        /// <summary>
        /// 工作Id
        /// </summary>
        public int jobId { get; set; }
    }
}
