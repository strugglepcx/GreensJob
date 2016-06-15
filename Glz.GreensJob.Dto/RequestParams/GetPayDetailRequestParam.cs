using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.Infrastructure;

namespace Glz.GreensJob.Dto.RequestParams
{
    public class GetPayDetailRequestParam : IdentityRequestParam, IPageRequestParam
    {
        /// <summary>
        /// 工作组Id
        /// </summary>
        public int jobGroupId { get; set; }
        /// <summary>
        /// 工作Id
        /// </summary>
        public int jobId { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
