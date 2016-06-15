using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    public class StopJobRequestParam : IdentityRequestParam
    {
        public int jobGroupId { get; set; }
    }
}
