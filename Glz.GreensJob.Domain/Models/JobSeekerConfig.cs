using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    //T_JobSeekerConfig
    public class JobSeekerConfig : IAggregateRoot<int>
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 是否接受录用、招聘消息 1 接受 0 不接受
        /// </summary>
        public bool RecruitMessage { get; set; }
        /// <summary>
        /// 紧急高薪工作消息1 接受 0 不接受
        /// </summary>
        public bool UrgentJobMessage { get; set; }
        /// <summary>
		/// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 求职者
        /// </summary>
        public virtual JobSeeker JobSeeker { get; set; }
    }
}