using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.Infrastructure
{
    /// <summary>
    /// 随机数
    /// </summary>
    public class RandomNumber
    {
        /// <summary>
        /// 获取六位数字
        /// </summary>
        /// <returns></returns>
        public static string GetSixString()
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next(0, 999999).ToString("D6");
        }
    }
}
