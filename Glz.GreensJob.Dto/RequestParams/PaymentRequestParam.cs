using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 支付请求参数
    /// </summary>
    public class PaymentRequestParam : IdentityRequestParam
    {
        /// <summary>
        /// 职位组Id
        /// </summary>
        public int jobGroupId { get; set; }
        /// <summary>
        /// 选择的职位支付明细
        /// </summary>
        public List<EnrollPayDetailModel> selectedEnrollPayItems { get; set; }
        /// <summary>
        /// 支付方式 0 支付宝 1 微信
        /// </summary>
        public int payType { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal total { get; set; }
    }
}
