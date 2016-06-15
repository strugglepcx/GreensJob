using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto
{
    public class ConfigurationModel
    {
        /// <summary>
        /// 是否接受录用、招聘消息 1 接受 0 不接受
        /// </summary>
        public bool recruitMessage { get; set; }
        /// <summary>
        /// 紧急高薪工作消息1 接受 0 不接受
        /// </summary>
        public bool urgentJobMessage { get; set; }
    }
}
