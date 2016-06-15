using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.Infrastructure;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 根据坐标获取城市名称请求参数
    /// </summary>
    public class GetCityForCoordinateRequestParam : RequestParamBase, ICoordinateRequestParam
    {
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal lat { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal lng { get; set; }
    }
}
