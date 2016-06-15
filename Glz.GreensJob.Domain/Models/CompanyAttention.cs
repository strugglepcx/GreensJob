using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    /// <summary>
    /// 公司关注
    /// </summary>
    public class CompanyAttention : IAggregateRoot<int>
    {
        /// <summary>
        /// 公司关注Id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        public int Company_ID { get; set; }
        /// <summary>
        /// 求职者Id
        /// </summary>
        public int JobSeeker_ID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public virtual Company Company { get; set; }
    }
}
