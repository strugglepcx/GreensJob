using Apworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Domain.Models
{
    public class City : IAggregateRoot<int>
    {
        public int ID { get; set; }

        public string name { get; set; }

        public int provinceID { get; set; }

        public Province Province { get; set; }
    }
}
