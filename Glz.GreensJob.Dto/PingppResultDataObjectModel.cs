using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto
{
    public class PingppResultDataObjectModel
    {
        /// <summary>
        /// chargeId
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 成功标识
        /// </summary>
        public bool paid { get; set; }
        /// <summary>
        /// 支付渠道
        /// </summary>
        public string channel { get; set; }
    }
}
