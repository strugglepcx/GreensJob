using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    //T_PublisherWallet
    public class PublisherWallet : IAggregateRoot<int>
    {
        /// <summary>
        /// 钱包编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 金额合计
        /// </summary>
        public decimal TotalAmounts { get; set; }
        /// <summary>
        /// 冻结金额
        /// </summary>
        public decimal FrozenAmounts { get; set; }
        /// <summary>
        /// 实际金额
        /// </summary>
        public decimal ActualAmounts { get; set; }
        /// <summary>
        /// 最后更新金额
        /// </summary>
        public decimal LastUpdateAmounts { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public decimal LastUpdateTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 提款密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 最后提现时间
        /// </summary>
        public DateTime LastExtractTime { get; set; }
        /// <summary>
        /// 最后修改密码时间
        /// </summary>
        public DateTime LastUpdatePasswordTime { get; set; }

        /// <summary>
        /// 发布者
        /// </summary>
        public virtual Publisher Publisher { get; set; }
        /// <summary>
        /// 日志
        /// </summary>
        public virtual ICollection<PublisherWalletActionLog> PublisherWalletActionLogs { get; set; }

    }
}