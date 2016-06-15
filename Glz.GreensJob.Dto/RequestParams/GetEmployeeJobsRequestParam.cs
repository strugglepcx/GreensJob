using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 获取用户相关职位列表请求参数
    /// </summary>
    public class GetEmployeeJobsRequestParam : WeiXinIdentityRequestParam, IPageRequestParam
    {
        /// <summary>
        /// 列表类型 1 收藏 2 报名 3 录用 4.收款情况
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int pageIndex { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int pageSize { get; set; }
    }
}
