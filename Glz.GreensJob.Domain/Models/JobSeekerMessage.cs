using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    //T_JobSeekerMessage
    public class JobSeekerMessage : IAggregateRoot<int>
    {

        /// <summary>
        /// 信息Id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 求职者Id
        /// </summary>
        public int JobSeeker_ID { get; set; }
        /// <summary>
        /// 信息内容
        /// </summary>
        public string MessageContent { get; set; }
        /// <summary>
        /// 信息类型（1：系统，2：录用）
        /// </summary>
        public int MessageType { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
		/// 职位Id
        /// </summary>
        public int Job_ID { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 求职者
        /// </summary>
        public virtual JobSeeker JobSeeker { get; set; }
    }
}