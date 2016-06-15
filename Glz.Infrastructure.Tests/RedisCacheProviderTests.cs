using Microsoft.VisualStudio.TestTools.UnitTesting;
using Glz.Infrastructure.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.Infrastructure.Caching.Tests
{
    [TestClass()]
    public class RedisCacheProviderTests
    {
        private readonly ICacheProvider _cacheProvider = new RedisCacheProvider();
        [TestMethod()]
        public void AddTest()
        {
            _cacheProvider.Add("a", "a.1", "123123");
            var value = _cacheProvider.Get("a", "a.1")?.ToString();
            Assert.AreEqual("123123", value);
        }

        [TestMethod()]
        public void PutTest()
        {
            _cacheProvider.Put("a", "a.1", "456456");
            var value = _cacheProvider.Get("a", "a.1")?.ToString();
            Assert.AreEqual("456456", value);
        }

        [TestMethod()]
        public void GetTest()
        {
            var value = _cacheProvider.Get("a", "a.1")?.ToString();
            Assert.AreEqual("456456", value);
        }

        [TestMethod()]
        public void RemoveTest()
        {
            _cacheProvider.Remove("a");
            Assert.IsTrue(!_cacheProvider.Exists("a"));
        }

        [TestMethod()]
        public void ExistsTest()
        {
            Assert.IsTrue(!_cacheProvider.Exists("a"));
        }

        [TestMethod()]
        public void ExistsTest1()
        {
            Assert.IsTrue(!_cacheProvider.Exists("a", "a.1"));
        }
    }
}