using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.Infrastructure;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 支付成功
    /// </summary>
    public class PaymentSuccessRequestParam : RequestParamBase
    {
        /// <summary>
        /// chargeId
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// ping++返回数据
        /// </summary>
        public PingppResultDataModel data { get; set; }
    }
}
