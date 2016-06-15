using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    public class JobClassify : IAggregateRoot<int>
    {
        public int ID { get; set; }

        public string name { get; set; }
    }
}
