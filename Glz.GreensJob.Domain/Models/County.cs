using Apworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Domain.Models
{
    public class County : IAggregateRoot<int>
    {
        public int ID { get; set; }

        public string name { get; set; }

        public int cityID { get; set; }
    }
}
