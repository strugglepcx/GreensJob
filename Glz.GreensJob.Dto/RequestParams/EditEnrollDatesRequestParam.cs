using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 修改录用时间参数
    /// </summary>
    public class EditEnrollDatesRequestParam : IdentityRequestParam
    {
        /// <summary>
        /// 报名信息Id
        /// </summary>
        public int enrollId { get; set; }
        /// <summary>
        /// 录用日期
        /// </summary>
        public IEnumerable<DateTime> enrollDates { get; set; }
    }
}
