using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Glz.Infrastructure
{
    public static class DateTimeExtend
    {
        /// <summary>
        /// 将指定时间对象转换成时间戳
        /// </summary>
        /// <param Name="targetDate"></param>
        /// <returns></returns>
        public static string GenerateTimeStamp(this DateTime targetDate)
        {
            // Default implementation of UNIX time of the current UTC time  
            TimeSpan ts = targetDate.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}