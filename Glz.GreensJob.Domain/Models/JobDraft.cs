using Apworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Domain.Models
{
    public class JobDraft:IAggregateRoot<int>
    {
        public int ID { get; set; }

        public string Contents { get; set; }
        public int PublisherID { get; set; }
        public int CompanyID { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EditTime { get; set; }
    }
}
