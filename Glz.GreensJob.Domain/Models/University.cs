using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    public class University : IAggregateRoot<int>
    {
        public University()
        {
            JobGroups = new HashSet<JobGroup>();
        }
        public int ID { get; set; }

        public string name { get; set; }

        public int cityID { get; set; }

        public virtual ICollection<JobGroup> JobGroups { get; set; }
    }
}
