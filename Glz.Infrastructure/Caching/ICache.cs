using System;

namespace Glz.Infrastructure.Caching
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICache : IDisposable
    {
        /// <summary>
        /// 获取缓存中Key对应的Value
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        [Obsolete("该方法已经过时，请使用该方法的泛型版本", true)]
        object Get(string key);
        /// <summary>
        /// 获取缓存中Key对应的Value
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        T Get<T>(string key) where T : class;
        //object Get(string key, Func<string, object> factory);
        /// <summary>
        /// 保存或设置Key对应的Value
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="slidingExpireTime">过期时间</param>
        void Set(string key, object value, TimeSpan? slidingExpireTime = null);
        /// <summary>
        /// 删除Key对应的Value
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
    }
}