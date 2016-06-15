using System;

namespace Glz.GreensJob.Dto
{
    public class EnrollDetailObject
    {
        public int id { get; set; }

        public int enrollID { get; set; }

        public DateTime date { get; set; }

        public DateTime start { get; set; }

        public DateTime end { get; set; }

        public bool isEmploy { get; set; }

        public bool isRetired { get; set; }
    }
}