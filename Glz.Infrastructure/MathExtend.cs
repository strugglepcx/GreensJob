using System;

namespace Glz.Infrastructure
{
    public static class MathExtend
    {
        /// <summary>
        /// 四舍五入(2位）
        /// </summary>
        /// <param name="value"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static decimal Rounding(this decimal value)
        {
            return value.Rounding(2);
        }

        /// <summary>
        /// 四舍五入(1位）
        /// </summary>
        /// <param name="value"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static decimal RoundingOne(this decimal value)
        {
            return value.Rounding(1);
        }

        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="value"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static decimal Rounding(this decimal value, int num)
        {
            return Math.Round(value, num, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 格式化折扣
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DiscountFormat(this decimal value)
        {
            var pointNum = value - (int)value;
            return pointNum > 0
                ? string.Format("{0:F1}", value)
                : string.Format("{0:F0}", value);
        }
    }
}