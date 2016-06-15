using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto
{
    /// <summary>
    /// 账户明细
    /// </summary>
    public class JobSeekerWalletActionLogModel
    {
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime transactionTime { get; set; }
        /// <summary>
        /// 交易类型，1 转出 2 转入
        /// </summary>
        public int transactionType { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal transactionAmount { get; set; }
        /// <summary>
        /// 交易是否成功
        /// </summary>
        public bool successTrans { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string jobName { get; set; }
    }
}
