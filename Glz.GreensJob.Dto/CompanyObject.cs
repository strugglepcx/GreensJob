using System;

namespace Glz.GreensJob.Dto
{
    public class CompanyObject
    {
        public int id { get; set; }

        public int cityID { get; set; }

        public string cityName { get; set; }

        public string name { get; set; }

        public string image { get; set; }

        public string introduce { get; set; }

        public string addr { get; set; }

        public int status { get; set; }

        public string license { get; set; }

        public string certificates { get; set; }

        public string contact { get; set; }

        public string tel { get; set; }

        public DateTime createDate { get; set; }
    }
}