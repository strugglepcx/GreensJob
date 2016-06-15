using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    public class EnrollDetail : IAggregateRoot<int>
    {
        public int ID { get; set; }

        public int enrollID { get; set; }

        public DateTime date { get; set; }

        public DateTime start { get; set; }

        public DateTime end { get; set; }

        public bool isEmploy { get; set; }

        public bool isRetired { get; set; }

        public virtual Enroll Enroll { get; set; }
    }
}
