using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 提现请求参数
    /// </summary>
    public class DspTransactionInfoRequestParam : WeiXinIdentityRequestParam, IPageRequestParam
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
