using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.Infrastructure.Maps
{
    public interface IMapPlace
    {
        /// <summary>
        /// 获取城市
        /// </summary>
        /// <param name="lat">纬度</param>
        /// <param name="lng">经度</param>
        /// <param name="radius">搜索半径</param>
        /// <returns></returns>
        string GetCity(decimal lat, decimal lng, decimal radius);
    }
}
