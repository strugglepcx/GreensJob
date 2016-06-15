using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    public class VerificationCode : IAggregateRoot<int>
    {
        public int ID { get; set; }

        public string code { get; set; }

        public string mobile { get; set; }

        public DateTime createDate { get; set; }
    }
}
