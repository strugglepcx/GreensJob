using System.Collections.Generic;

namespace Glz.GreensJob.Dto
{
    /// <summary>
    /// 省信息
    /// </summary>
    public class ProvinceObject
    {
        /// <summary>
        /// 省Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 省名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 城市列表
        /// </summary>
        public List<CityObject> Cities { get; set; }
    }
}