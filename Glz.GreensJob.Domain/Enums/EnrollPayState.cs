using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Domain.Enums
{
    /// <summary>
    /// 录用支付状态
    /// </summary>
    public enum EnrollPayState
    {
        /// <summary>
        /// 未处理
        /// </summary>
        Untreated = 10,
        /// <summary>
        /// 已处理
        /// </summary>
        Processed = 20,
        /// <summary>
        /// 已支付
        /// </summary>
        Paid = 30,
    }
}
