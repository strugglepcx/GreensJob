using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    public class EnrollPayRequestParam : IdentityRequestParam
    {
        public List<PayDetail> Details { get; set; }

        public string channel { get; set; }

        public decimal total { get; set; }
    }

    public class PayDetail {
        public int id { get; set; }

        public decimal salary {get;set;}
    }
}
