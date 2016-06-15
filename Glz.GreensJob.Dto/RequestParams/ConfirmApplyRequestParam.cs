using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.Infrastructure.Locking;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 确认报名请求参数
    /// </summary>
    public class ConfirmApplyRequestParam : WeiXinIdentityRequestParam, ILockingJob
    {
        /// <summary>
        /// 职位Id
        /// </summary>
        public int jobId { get; set; }
    }
}
