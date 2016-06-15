using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Apworks;

namespace Glz.GreensJob.Logs.Domain.Models
{
    //ActionExceptionLog
    public class ActionExceptionLog : IAggregateRoot<int>
    {
        /// <summary>
        /// Action异常Id
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
        /// 错误类型
        /// </summary>
        public string ExceptionType { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ExceptionMessage { get; set; }
        /// <summary>
        /// 错误堆栈信息
        /// </summary>
        public string ExceptionTraceStack { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}