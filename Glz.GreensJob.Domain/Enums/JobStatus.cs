using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Domain.Enums
{
    /// <summary>
    /// 职位状态
    /// </summary>
    public enum JobStatus
    {
        /// <summary>
        /// 草稿
        /// </summary>
        Draft = 1,
        /// <summary>
        /// 待审核
        /// </summary>
        PendingAudit = 10,
        /// <summary>
        /// 录用
        /// </summary>
        Employ = 20,
        /// <summary>
        /// 过期
        /// </summary>
        Expired = 30,
        /// <summary>
        /// 停止
        /// </summary>
        Stop = 40
    }
}
