using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto
{
    public class JobSeekerMessageModel
    {
        /// <summary>
        /// 信息Id
        /// </summary>
        public int Id { get; set; }
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
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }
    }
}
