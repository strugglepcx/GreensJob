namespace Glz.GreensJob.Dto
{
    /// <summary>
    /// 收款模型
    /// </summary>
    public class PayeeModel
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// jobId
        /// </summary>
        public string jobId { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 工作开始时间
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 工作结束时间
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 工作天数
        /// </summary>
        public string WorkDays { get; set; }
        /// <summary>
        /// 付款金额
        /// </summary>
        public string PaymentTotalAmount { get; set; }
        /// <summary>
        /// 基本工资
        /// </summary>
        public string BasePayAmount { get; set; }
        /// <summary>
        /// 奖金
        /// </summary>
        public string BonusAmount { get; set; }
        /// <summary>
        /// 扣款
        /// </summary>
        public string DebitAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
    }
}