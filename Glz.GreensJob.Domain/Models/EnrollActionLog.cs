using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Apworks;
using Glz.GreensJob.Domain.Enums;

namespace Glz.GreensJob.Domain.Models
{
    /// <summary>
    /// 报名操作记录
    /// </summary>
    public class EnrollActionLog : IAggregateRoot<int>
    {
        /// <summary>
        /// 报名操作记录
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Enroll_ID
        /// </summary>
        public int Enroll_ID { get; set; }
        /// <summary>
        /// Job_ID
        /// </summary>
        public int Job_ID { get; set; }
        /// <summary>
        /// Job_Name
        /// </summary>
        public string Job_Name { get; set; }
        /// <summary>
        /// Job_GroupID
        /// </summary>
        public int Job_GroupID { get; set; }
        /// <summary>
        /// JobSeeker_ID
        /// </summary>
        public int JobSeeker_ID { get; set; }
        /// <summary>
        /// ActionID
        /// </summary>
        public EnrollAction ActionID { get; set; }
        /// <summary>
        /// ActionName
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 职位信息
        /// </summary>
        public virtual Enroll Enroll { get; set; }
    }
}