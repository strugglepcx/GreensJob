using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;

namespace Domain.Models
{
    public class VirtualInfo : IAggregateRoot<int>
    {
        public int ID { get; set; }

        public int jobSeekerID { get; set; }

        public string nickName { get; set; }

        public string virtualImage { get; set; }
    }
}
