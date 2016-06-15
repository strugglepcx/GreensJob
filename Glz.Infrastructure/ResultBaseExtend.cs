using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Glz.Infrastructure
{
    public class ResultBaseExtend
    {
        /// <summary>
        /// 创建OutCode实例对象
        /// </summary>
        /// <typeparam Name="T">必须继承自OutCode</typeparam>
        /// <param Name="code">返回码</param>
        /// <param Name="message">返回消息</param>
        /// <returns>OutCode实例对象</returns>
        public static T CreateResult<T>(StatusCodes code, string message)
            where T : ResultBase, new()
        {
            T errorOutCode = new T();
            errorOutCode.code = code;
            errorOutCode.message = message;
            return errorOutCode;
        }

        /// <summary>
        /// 创建OutCode实例对象
        /// </summary>
        /// <typeparam Name="T">必须继承自OutCode</typeparam>
        /// <param Name="code">返回码,错误信息会根据Resource中的配置自动填充</param>
        /// <returns>OutCode实例对象</returns>
        public static T CreateResult<T>(StatusCodes code)
            where T : ResultBase, new()
        {
            return CreateResult<T>(code, ResourceExtend.GetResourceForName($"_{code}") ?? string.Empty);
        }
    }
}