using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    /// <summary>
    /// 提现申请
    /// </summary>
    public class ExtractApply : IAggregateRoot<int>
    {

        /// <summary>
        /// 提现申请Id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 求职者Id
        /// </summary>
        public int JobSeeker_ID { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankCardNo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecuteTime { get; set; }
        /// <summary>
        /// 执行人
        /// </summary>
        public int Executor_ID { get; set; }
        /// <summary>
        /// 状态（0：未处理，1：已处理）
        /// </summary>
        public int State { get; set; }

        public virtual JobSeeker JobSeeker { get; set; }
    }
}