using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    public class PublisherRight : IAggregateRoot<int>
    {
        public int ID { get; set; }

        public bool AddUserRight { get; set; }

        public bool ModifyUserRight { get; set; }

        public bool DeleteUserRight { get; set; }

        public bool FinicialRight { get; set; }

        public bool ReleaseJobRight { get; set; }

        public bool ImportEmployeeRight { get; set; }

        public Publisher Publisher { get; set; }
    }
}
