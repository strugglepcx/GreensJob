using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    public class GetJobDraftListRequestParam : IdentityRequestParam
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
