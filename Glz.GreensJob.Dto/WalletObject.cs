using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto
{
    public class WalletObject
    {
        public int ID { get; set; }

        public int memberID { get; set; }

        public int memberCategory { get; set; }

        public decimal TotalAmounts { get; set; }

        public int Integral { get; set; }

        public DateTime createDate { get; set; }
    }
}
