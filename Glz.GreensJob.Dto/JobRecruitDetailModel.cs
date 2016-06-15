using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto
{
    /// <summary>
    /// 报名时间模型
    /// </summary>
    public class JobRecruitDetailModel
    {
        /// <summary>
        /// 报名日期
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 招聘人数
        /// </summary>
        public int RecruitNum { get; set; }
        /// <summary>
        /// 报名人数
        /// </summary>
        public int ApplicantNum { get; set; }
        /// <summary>
        /// 录用人数
        /// </summary>
        public int EmploymentNum { get; set; }
    }
}
