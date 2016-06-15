using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    public class Collect : IAggregateRoot<int>
    {
        public int ID { get; set; }

        public int jobSeekerID { get; set; }

        public int jobID { get; set; }

        public Job job { get; set; }

        public JobSeeker jobSeeker { get; set; }

        public bool status { get; set; }
    }
}
