using System;
using System.Runtime.Serialization;


namespace Glz.Infrastructure
{
    public class ResultBase
    {
        public ResultBase()
        {
            timestamp = DateTime.Now;
        }
        /// <summary>
        /// 返回码
        /// </summary>
        public StatusCodes code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime timestamp { get; set; }
    }

    public class ResultBase<T> : ResultBase
    {
        public T Data { get; set; }
    }
}