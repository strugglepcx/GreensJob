using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 提现请求参数
    /// </summary>
    public class TransferMoneyOutRequestParam : WeiXinIdentityRequestParam
    {
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 卡号（微信号）
        /// </summary>
        public string cardNumber { get; set; }
    }
}
