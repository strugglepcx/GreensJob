using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    //T_OpenCity
    public class OpenCity : IAggregateRoot<int>
    {

        /// <summary>
        /// OpenCity_ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// OpenCity_Name
        /// </summary>
        public string Name { get; set; }

    }
}