using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;
using Glz.GreensJob.Domain.Enums;

namespace Glz.GreensJob.Domain.Models
{
    public class Enroll : IAggregateRoot<int>
    {
        public Enroll()
        {
            EnrollDetails = new HashSet<EnrollDetail>();
            EnrollEmployDetails = new HashSet<EnrollEmployDetail>();
        }
        public int ID { get; set; }

        public int jobSeekerID { get; set; }

        public virtual JobSeeker JobSeeker { get; set; }

        public int jobID { get; set; }

        public virtual Job Job { get; set; }

        public int introducer { get; set; }

        public bool status { get; set; }

        public string name { get; set; }

        public string mobile { get; set; }
        public DateTime CreateTime { get; set; }
        public EmployStatu employStatus { get; set; }

        public bool experienced { get; set; }

        public EnrollMethod enrollMethod { get; set; }

        public virtual ICollection<EnrollDetail> EnrollDetails { get; set; }
        public virtual ICollection<EnrollEmployDetail> EnrollEmployDetails { get; set; }
    }
}
