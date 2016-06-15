using Microsoft.VisualStudio.TestTools.UnitTesting;
using Glz.Infrastructure.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.Infrastructure.Maps.Tests
{
    [TestClass()]
    public class TencentMapPlaceTest
    {
        [TestMethod()]
        public void GetCityTest()
        {
            IMapPlace mapPlace = new TencentMapPlace();
            var city = mapPlace.GetCity(30.505391m, 114.406085m, 1000);
            Assert.AreEqual("武汉市", city);
        }
    }
}