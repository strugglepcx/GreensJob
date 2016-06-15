using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    public class Wallet : IAggregateRoot<int>
    {
        public int ID { get; set; }

        public int memberID { get; set; }

        public int memberCategory { get; set; }

        public decimal TotalAmounts { get; set; }

        public int Integral { get; set; }

        public DateTime createDate { get; set; }
    }
}
