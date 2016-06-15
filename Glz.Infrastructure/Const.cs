using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.Infrastructure
{
    /// <summary>
    /// 常量
    /// </summary>
    public class Const
    {
        /// <summary>
        /// 验证码缓存Key
        /// </summary>
        public const string ValidateCodeCacheKey = "greensjob_reg_";
        /// <summary>
        /// 用户Session缓存Key
        /// </summary>
        public const string UserSessionCodeCacheKey = "greensjob_user_session_";
        /// <summary>
        /// 求职者Session缓存Key
        /// </summary>
        public const string SeekerSessionCodeCacheKey = "greensjob_seeker_session_";
        /// <summary>
        /// 手机用户Session缓存Key
        /// </summary>
        public const string MobileUserSessionCodeCacheKey = "greensjob_mobile_user_session_";
        /// <summary>
        /// 手机用户CookiesKey
        /// </summary>
        public const string MobileUserSessionCodeCookiesKey = "greensjob_mobile_user_cookies";
        /// <summary>
        /// 城市坐标缓存Key
        /// </summary>
        public const string CityCoordinateCacheKey = "greensjob_city_coordinate";
        /// <summary>
        /// 用户session过期时间（小时）
        /// </summary>
        public const int UserSessionsLidingExpireTime = 48;
        /// <summary>
        /// 求职者session过期时间（小时）
        /// </summary>
        public const int SeekerSessionsLidingExpireTime = 48;
        /// <summary>
        /// 城市搜索半径
        /// </summary>
        public const int CitySearchRadius = 5000;
    }
}
