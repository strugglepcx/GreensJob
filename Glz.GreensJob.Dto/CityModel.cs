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
    /// 城市模型
    /// </summary>
    public class CityModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
