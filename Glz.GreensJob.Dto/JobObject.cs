using System;

namespace Glz.GreensJob.Dto
{
    public class JobObject
    {
        public int id { get; set; }
        public string name { get; set; }
        public int publisherID { get; set; }
        public string publisherName { get; set; }
        public string publisherMobile { get; set; }
        public int jobCategoryID { get; set; }
        public string jobCategoryName { get; set; }
        public int jobClassifyID { get; set; }
        public string jobClassifyName { get; set; }
        public int jobSchduleID { get; set; }
        public string jobSchduleName { get; set; }
        public int payCategoryID { get; set; }
        public string payCategoryName { get; set; }
        public int payUnitID { get; set; }
        public string payUnitName { get; set; }
        public int genderLimit { get; set; }
        public int heightLimit { get; set; }
        public bool autoTimeShare { get; set; }
        public int erollMethod { get; set; }
        public int recruitNum { get; set; }
        public string addr { get; set; }
        public int groupID { get; set; }
        public decimal lng { get; set; }
        public decimal lat { get; set; }
        public string content { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public decimal salary { get; set; }
        public bool urgent { get; set; }
        public bool healthCertificate { get; set; }
        public bool interview { get; set; }
        public string interviewPlace { get; set; }
        public string employer { get; set; }
        public string contactMan { get; set; }
        public string mobileNumber { get; set; }
        public string gatheringPlace { get; set; }
        public DateTime releaseDate { get; set; }
        public DateTime createDate { get; set; }
        public int status { get; set; }
    }
}