using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    /// <summary>
    /// 职位招聘明细
    /// </summary>
    public class JobRecruitDetail : IAggregateRoot<int>
    {
        /// <summary>
        /// 职位招聘明细
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime RecruitDate { get; set; }
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
        /// <summary>
        /// 职位Id
        /// </summary>
        public int Job_ID { get; set; }
        /// <summary>
        /// 职位组Id
        /// </summary>
        public int JobGroup_ID { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public virtual Job Job { get; set; }
        /// <summary>
        /// 职位组
        /// </summary>
        public virtual JobGroup JobGroup { get; set; }
    }
}