using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    public class DspMsgListRequestParam : WeiXinIdentityRequestParam, IPageRequestParam
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
