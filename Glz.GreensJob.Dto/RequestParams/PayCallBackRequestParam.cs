using Glz.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    public class PayCallBackRequestParam : RequestParamBase
    {
        public string payResult { get; set; }
        public string orderId { get; set; }
    }
}
