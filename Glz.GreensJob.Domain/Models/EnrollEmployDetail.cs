using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    //T_EnrollEmployDetail
    public class EnrollEmployDetail : IAggregateRoot<int>
    {

        /// <summary>
        /// 报名录用明细Id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 报名Id
        /// </summary>
        public int Enroll_ID { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

    }
}