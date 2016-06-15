using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 获取工作组列表（录用，过期）
    /// </summary>
    public class GetJobGroupsByCompanyRequestParam : IdentityRequestParam
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
