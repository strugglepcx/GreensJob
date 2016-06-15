using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    public class Complaint : IAggregateRoot<int>
    {
        public int ID { get; set; }

        public int category { get; set; }

        public int jobID { get; set; }

        public int jobSeekerID { get; set; }

        public string content { get; set; }

        public DateTime createDate { get; set; }
    }
}
