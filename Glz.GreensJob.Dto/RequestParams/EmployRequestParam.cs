using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.Infrastructure.Locking;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 录用请求参数
    /// </summary>
    public class EmployRequestParam : IdentityRequestParam, ILockingJob
    {
        /// <summary>
        /// 报名信息Id
        /// </summary>
        public int enrollId { get; set; }
        /// <summary>
        /// 录用日期
        /// </summary>
        public IEnumerable<DateTime> applyDateList { get; set; }
        /// <summary>
        /// 职位Id
        /// </summary>
        public int jobId { get; set; }
    }
}
