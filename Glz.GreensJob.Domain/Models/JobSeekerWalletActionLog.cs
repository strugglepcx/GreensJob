using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Apworks;
using Glz.GreensJob.Domain.Enums;

namespace Glz.GreensJob.Domain.Models
{
    //T_JobSeekerWalletActionLog
    public class JobSeekerWalletActionLog : IAggregateRoot<int>
    {

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 钱包Id
        /// </summary>
        public int JobSeekerWallet_ID { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 用户名（电话）
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 操作动作Id
        /// </summary>
        public WalletAction ActionID { get; set; }
        /// <summary>
        /// 操作动作名称
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 职位组Id
        /// </summary>
        public int JobGroup_ID { get; set; }
        /// <summary>
        /// 职位Id
        /// </summary>
        public int Job_ID { get; set; }
        /// <summary>
        /// 录用记录Id
        /// </summary>
        public int Enroll_ID { get; set; }
        /// <summary>
        /// 开发城市Id
        /// </summary>
        public int OpenCity_ID { get; set; }
        /// <summary>
        /// 付款类型（1：支付宝，2：微信，3：银行卡）
        /// </summary>
        public int PayType { get; set; }
        /// <summary>
        /// 付款类型（1：支付宝，2：微信，3：银行卡）
        /// </summary>
        public string PayTypeName { get; set; }
        /// <summary>
        /// 支付凭证
        /// </summary>
        public string PaySn { get; set; }
        /// <summary>
        /// BankCardNo
        /// </summary>
        public string BankCardNo { get; set; }
        /// <summary>
		/// 提现申请Id
        /// </summary>
        public int? ExtractApply_ID { get; set; }
        /// <summary>
        /// 状态（0：未处理，1：成功）
        /// </summary>
        public int State { get; set; }
        public virtual JobSeekerWallet JobSeekerWallet { get; set; }
        /// <summary>
        /// 提现申请
        /// </summary>
        public virtual ExtractApply ExtractApply { get; set; }
        /// <summary>
        /// 职位名称（地址）
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 职位组名称
        /// </summary>
        public string JobGroupName { get; set; }
    }
}