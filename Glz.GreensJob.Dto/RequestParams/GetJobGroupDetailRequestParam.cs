using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    public class GetJobGroupDetailRequestParam : IdentityRequestParam
    {
        /// <summary>
        /// 工作组Id
        /// </summary>
        public int jobGroupID { get; set; }
    }
}
