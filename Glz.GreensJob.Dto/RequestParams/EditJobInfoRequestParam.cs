using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    public class EditJobInfoRequestParam : IdentityRequestParam
    {
        /// <summary>
        /// 职位信息
        /// </summary>
        public JobGroupObject JobGroupObject { get; set; }
    }
}
