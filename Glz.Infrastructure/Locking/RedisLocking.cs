using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Redlock.CSharp;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Configuration;

namespace Glz.Infrastructure.Locking
{
    public class RedisLocking : ILocking
    {
        private readonly Redlock.CSharp.Redlock _redlock;
        private readonly TimeSpan _lockInvalidSpan = new TimeSpan(0, 0, 0, 30);
        private readonly IEnumerable<ConnectionMultiplexer> _connectionMultiplexers;

        /// <summary>
        /// 重试次数（1秒1次）
        /// </summary>
        private const int RetryCount = 10;

        public RedisLocking()
        {
            var redisCachingSectionHandler = RedisCachingSectionHandler.GetConfig();
            if (redisCachingSectionHandler.RedisHosts.Count <= 0)
            {
                throw new Exception("未配置Redis节点");
            }
            _connectionMultiplexers = from RedisHost redisHost in redisCachingSectionHandler.RedisHosts select ConnectionMultiplexer.Connect($"{redisHost.Host}:{redisHost.CachePort},allowAdmin={redisCachingSectionHandler.AllowAdmin},ssl={redisCachingSectionHandler.Ssl},password={redisCachingSectionHandler.Password}");
            _redlock = new Redlock.CSharp.Redlock(_connectionMultiplexers.ToArray());
        }

        public bool Lock(string resourceName, out Lock @lock)
        {
            var currentRetry = 0;
            @lock = new Lock("", "", TimeSpan.MinValue);
            while (currentRetry < RetryCount)
            {
                var result = _redlock.Lock(resourceName, _lockInvalidSpan, out @lock);
                if (result)
                {
                    return true;
                }
                Thread.Sleep(1000);
                currentRetry++;
            }
            return false;
        }

        public void UnLock(Lock @lock)
        {
            _redlock.Unlock(@lock);
        }
    }
}
