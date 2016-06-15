using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Logs.Dto
{
    public class ActionLogModel
    {
        /// <summary>
        /// ActionId
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        [Required]
        public string ControllerName { get; set; }
        /// <summary>
        /// Action名称
        /// </summary>
        [Required]
        public string ActionName { get; set; }
        /// <summary>
        /// 请求内容类型
        /// </summary>
        [Required]
        public string ContentType { get; set; }
        /// <summary>
        /// 请求Url
        /// </summary>
        [Required]
        public string RequestUrl { get; set; }
        /// <summary>
        /// 请求内容
        /// </summary>
        [Required]
        public string RequestContent { get; set; }
        /// <summary>
        /// 结果状态码
        /// </summary>
        [Required]
        public string ResultCode { get; set; }
        /// <summary>
        /// 结果内容
        /// </summary>
        [Required]
        public string ResultContent { get; set; }
        /// <summary>
        /// 执行时间（毫秒）
        /// </summary>
        [Required]
        public int Duration { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecuteTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }
    }
}
