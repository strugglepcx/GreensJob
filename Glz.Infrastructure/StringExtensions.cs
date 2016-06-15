using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.Infrastructure
{
    public static class StringExtensions
    {
        /// <summary>
        /// 获取字符串表达的int值，如果转换失败返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetIntOrDefault(this string str, int defaultValue)
        {
            int result;
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return int.TryParse(str, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 获取字符串表达的int值，如果转换失败返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetIntOrDefault(this string str)
        {
            return str.GetIntOrDefault(0);
        }

        /// <summary>
        /// 获取字符串表达的Guid值，如果转换失败返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Guid GetGuidOrDefault(this string str, Guid defaultValue)
        {
            Guid result;
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return Guid.TryParse(str, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 获取字符串表达的Guid值，如果转换失败返回Guid.Empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid GetGuidOrDefault(this string str)
        {
            return str.GetGuidOrDefault(Guid.Empty);
        }

        /// <summary>
        /// 获取字符串表达的DateTime值，如果转换失败返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeOrDefault(this string str, DateTime defaultValue)
        {
            DateTime result;
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return DateTime.TryParse(str, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 获取字符串表达的DateTime值，如果转换失败返回1970.01.01
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeOrDefault(this string str)
        {
            return str.GetDateTimeOrDefault(new DateTime(1970, 1, 1));
        }

        /// <summary>
        /// 获取字符串表达的decimal值，如果转换失败返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal GetDecimalOrDefault(this string str, decimal defaultValue)
        {
            decimal result;
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return decimal.TryParse(str, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 获取字符串表达的decimal值，如果转换失败返回0.00M
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal GetDecimalOrDefault(this string str)
        {
            return str.GetDecimalOrDefault(0.00M);
        }
    }
}
