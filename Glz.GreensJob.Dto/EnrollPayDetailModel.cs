using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto
{
    /// <summary>
    /// 支付明细Model
    /// </summary>
    public class EnrollPayDetailModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 求职者姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 求职者手机号
        /// </summary>
        public string UserMobile { get; set; }
        /// <summary>
        /// 工作开始日期
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 工作结束日期
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 工作天数
        /// </summary>
        public int WorkDays { get; set; }
        /// <summary>
        /// 支付总金额
        /// </summary>
        public decimal AmountSalary { get; set; }
        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal BasePay { get; set; }
        /// <summary>
        /// 奖金
        /// </summary>
        public decimal Bonus { get; set; }
        /// <summary>
        /// 扣款
        /// </summary>
        public decimal Debit { get; set; }
        /// <summary>
        /// 职位Id
        /// </summary>
        public int Job_ID { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 状态（0：未付款，1：已付款）
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayTime { get; set; }
        /// <summary>
        /// 职位组名称
        /// </summary>
        public string JobGroupName { get; set; }
    }
}
