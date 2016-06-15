using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 获取成功支付的支付明细
    /// </summary>
    public class GetSuccessPayDetailRequestParam : IdentityRequestParam, IPageRequestParam
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string keyword { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
