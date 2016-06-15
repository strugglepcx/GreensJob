using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto
{
    public class DailyRangeModel
    {
        /// <summary>
        /// 0：小于，1：区间，2：大于
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 最小
        /// </summary>
        public decimal Min { get; set; }
        /// <summary>
        /// 最大
        /// </summary>
        public decimal Max { get; set; }
    }
}
