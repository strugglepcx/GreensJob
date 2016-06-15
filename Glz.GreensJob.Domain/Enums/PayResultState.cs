using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Domain.Enums
{
    public enum PayResultState
    {
        /// <summary>
        /// 成功
        /// </summary>
        Sucess = 0,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = 1,
        /// <summary>
        /// 正在支付
        /// </summary>
        BeingPaid = 2
    }
}
