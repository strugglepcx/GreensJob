using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 坐标接口
    /// </summary>
    public interface ICoordinateRequestParam
    {
        /// <summary>
        /// 纬度
        /// </summary>
        decimal lat { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        decimal lng { get; set; }
    }
}
