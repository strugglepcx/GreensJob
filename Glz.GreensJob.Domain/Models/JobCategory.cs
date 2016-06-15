using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    public class JobCategory : IAggregateRoot<int>
    {
        public JobCategory()
        {
            Jobs = new HashSet<Job>();
        }
        public int ID { get; set; }
        public string name { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
