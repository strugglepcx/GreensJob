using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto
{
    public class JobDraftObject
    {
        public int ID { get; set; }

        public string Contents { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime EditTime { get; set; }
    }
}
