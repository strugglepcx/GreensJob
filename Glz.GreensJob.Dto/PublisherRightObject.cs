using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto
{
    public class PublisherRightObject
    {
        public int ID { get; set; }
        
        public bool AddUser { get; set; }

        public bool ModifyUser { get; set; }
        public bool DeleteUser { get; set; }

        public bool Finicial { get; set; }

        public bool ReleaseJob { get; set; }

        public bool ImportEmployee { get; set; }
    }
}
