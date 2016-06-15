using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Glz.GreensJob.Dto
{
    /// <summary>
    /// 城市坐标模型
    /// </summary>
    public class CityCoordinateModel
    {
        /// <summary>
        /// 城市名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal Lat { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal Lng { get; set; }
        /// <summary>
        /// 地理位置
        /// </summary>
        [JsonIgnore]
        public DbGeography Location { get; set; }
    }
}
