using System;
using Apworks;

namespace Glz.GreensJob.Domain.Models
{
    public class FeedBack:IAggregateRoot<int>
    {
        public int ID { get; set; }

        public int Category { get; set; }

        public int MemberID { get; set; }

        public int MemberCategory { get; set; }

        public string Contact { get; set; }

        public string Content { get; set; }

        public int Status { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
