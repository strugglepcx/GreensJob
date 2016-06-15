using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.Infrastructure
{
    /// <summary>
    /// 全局错误码
    /// </summary>
    public enum StatusCodes
    {
        /// <summary>
        /// 错误
        /// </summary>
        Failure = 0,
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 无效参数
        /// </summary>
        InvalidParameter = 10,
        /// <summary>
        /// 执行访问被禁止
        /// </summary>
        Forbidden = 403,
        /// <summary>
        /// 服务器内部错误
        /// </summary>
        InternalError = 500,
        /// <summary>
        /// 登录超时
        /// </summary>
        Timeout = 502

    }
}
