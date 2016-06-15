using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Apworks;

namespace Glz.GreensJob.Logs.Domain.Models
{
    //ActionLog
    public class ActionLog : IAggregateRoot<int>
    {
        /// <summary>
        /// ActionId
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// Action名称
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 请求内容类型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 请求Url
        /// </summary>
        public string RequestUrl { get; set; }
        /// <summary>
        /// 请求内容
        /// </summary>
        public string RequestContent { get; set; }
        /// <summary>
        /// 结果状态码
        /// </summary>
        public string ResultCode { get; set; }
        /// <summary>
        /// 结果内容
        /// </summary>
        public string ResultContent { get; set; }
        /// <summary>
        /// 执行时间（毫秒）
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecuteTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}