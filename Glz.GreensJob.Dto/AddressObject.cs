using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto
{
    /// <summary>
    /// 地址信息
    /// </summary>
    public class AddressObject
    {
        /// <summary>
        /// 地址名称
        /// </summary>
        public string addr { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal lng { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal lat { get; set; }
        /// <summary>
        /// 城市Id
        /// </summary>
        public int cityId { get; set; }
        /// <summary>
        /// 城区Id
        /// </summary>
        public int districtId { get; set; }
    }
}
