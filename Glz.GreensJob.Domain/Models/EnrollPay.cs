using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Apworks;
using Glz.GreensJob.Domain.Enums;

namespace Glz.GreensJob.Domain.Models
{
    //T_EnrollPay
    public class EnrollPay : IAggregateRoot<int>
    {
        public EnrollPay()
        {
            EnrollPayDetails = new HashSet<EnrollPayDetail>();
        }
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 发布者Id
        /// </summary>
        public int Publisher_ID { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        public int Company_ID { get; set; }
        /// <summary>
        /// 付款金额
        /// </summary>
        public decimal PayAmount { get; set; }
        /// <summary>
        /// 付款类型（1：支付宝，2：微信，3：余额）
        /// </summary>
        public int PayType { get; set; }
        /// <summary>
        /// 支付SN（凭证）
        /// </summary>
        public string PaySn { get; set; }
        /// <summary>
        /// 支付订单号
        /// </summary>
        public string orderID { get; set; }
        /// <summary>
        /// 支付结果（0：支付失败，1：支付成功, 2:正在支付）
        /// </summary>
        public PayResultState PayResult { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        public virtual ICollection<EnrollPayDetail> EnrollPayDetails { get; set; }

    }
}