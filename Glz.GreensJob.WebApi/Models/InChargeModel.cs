using System.Runtime.Serialization;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class InChargeModel: RequestParamBase
    {
        /// <summary>
        /// 支付订单编号
        /// </summary>
        [DataMember]
        public string OrderID { get; set; }

        /// <summary>
        /// 支付金额（单位：分）
        /// </summary>
        [DataMember]
        public int Money { get; set; }

        /// <summary>
        /// 支付渠道
        /// </summary>
        [DataMember]
        public string Channel { get; set; }

        /// <summary>
        /// 商品的标题
        /// </summary>
        [DataMember]
        public string Subject { get; set; }

        /// <summary>
        /// 商品的描述信息
        /// </summary>
        [DataMember]
        public string Body { get; set; }
    }
}