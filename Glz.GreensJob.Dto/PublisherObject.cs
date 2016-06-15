using System;

namespace Glz.GreensJob.Dto
{
    public class PublisherObject
    {
        public int id { get; set; }

        public int companyID { get; set; }

        public string name { get; set; }

        public string mobile { get; set; }

        public string password { get; set; }

        public bool isAdmin { get; set; }

        public DateTime lastLoginDate { get; set; }

        public DateTime createDate { get; set; }

        public PublisherRightObject PublisherRight { get; set; }
    }
}