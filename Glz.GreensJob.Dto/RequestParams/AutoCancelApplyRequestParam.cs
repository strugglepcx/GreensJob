using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    public class AutoCancelApplyRequestParam : IdentityRequestParam
    {
        public int enrollId { get; set; }
        public int jobSeekerId { get; set; }
        public double hour { get; set; }
    }
}
