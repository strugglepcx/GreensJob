using System;

namespace Glz.GreensJob.Dto
{
    public class ComplaintObject
    {
        public int id { get; set; }

        public int category { get; set; }

        public int jobID { get; set; }

        public int jobSeekerID { get; set; }

        public string content { get; set; }

        public DateTime createDate { get; set; }
    }
}