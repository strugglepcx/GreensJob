using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto
{
    /// <summary>
    /// 职位管理工作对象
    /// </summary>
    public class JobInfo
    {
        /// <summary>
        /// jobGroupID
        /// </summary>
        public int jobGroupID { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string jobName { get; set; }
        /// <summary>
        /// 发布状态
        /// </summary>
        public int releaseStatus { get; set; }
        /// <summary>
        /// 类型（本地有图片，对应显示）
        /// </summary>
        public int jobClass { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public int jobSchedule { get; set; }
        /// <summary>
        /// 单位价格
        /// </summary>
        public decimal unitPrice { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string unitName { get; set; }
        /// <summary>
        /// 招聘人数
        /// </summary>
        public int recruitmentNum { get; set; }
        /// <summary>
        /// 招聘天数
        /// </summary>
        public int startdate { get; set; }
        /// <summary>
        /// 用人单位
        /// </summary>
        public string employer { get; set; }
        /// <summary>
        /// 报名/申请人数
        /// </summary>
        public int applicantNum { get; set; }
        /// <summary>
        /// 录用人数
        /// </summary>
        public int employNum { get; set; }
        /// <summary>
        /// 线上录用人数
        /// </summary>
        public int onlineEmployNum { get; set; }
        /// <summary>
        /// 线下录用人数
        /// </summary>
        public int offineEmployNum { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime releaseDate { get; set; }
    }
}
