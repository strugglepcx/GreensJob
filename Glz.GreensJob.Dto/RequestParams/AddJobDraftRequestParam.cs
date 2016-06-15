using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    public class AddJobDraftRequestParam:IdentityRequestParam
    {
        public string Contents { get; set; }
    }
}
