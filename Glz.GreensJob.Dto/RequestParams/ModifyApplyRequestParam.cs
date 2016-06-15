using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.Infrastructure.Locking;

namespace Glz.GreensJob.Dto.RequestParams
{
    public class ModifyApplyRequestParam : WeiXinIdentityRequestParam, ILockingJob
    {
        /// <summary>
        /// 职位Id
        /// </summary>
        public int jobId { get; set; }
        /// <summary>
        /// 报名时间列表
        /// </summary>
        public IEnumerable<DateTime> applyDateList { get; set; }
    }
}
