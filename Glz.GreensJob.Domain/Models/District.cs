using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    //T_District
    public class District : IAggregateRoot<int>
    {

        /// <summary>
        /// 城区Id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 城区名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 城市Id
        /// </summary>
        public int City_ID { get; set; }

    }
}