using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class InJobModel : RequestParamBase
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int jobGroupID { get; set; }
        [DataMember]
        public int userID { get; set; }
        [DataMember]
        public int jobCategoryID { get; set; }
        [DataMember]
        public int jobClassifyID { get; set; }
        [DataMember]
        public int jobSchduleID { get; set; }
        [DataMember]
        public int payCategoryID { get; set; }
        [DataMember]
        public int payUnitID { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int genderLimit { get; set; }
        [DataMember]
        public int heightLimit { get; set; }
        [DataMember]
        public bool autoTimeShare { get; set; }
        [DataMember]
        public int erollMethod { get; set; }
        [DataMember]
        public int recruitNum { get; set; }
        [DataMember]
        public decimal salary { get; set; }
        [DataMember]
        public string content { get; set; }
        [DataMember]
        public bool urgent { get; set; }
        [DataMember]
        public bool healthCertificate { get; set; }
        [DataMember]
        public bool interview { get; set; }
        [DataMember]
        public string interviewPlace { get; set; }
        [DataMember]
        public string employer { get; set; }
        [DataMember]
        public List<Address> addrList { get; set; }
        [DataMember]
        public string addr { get; set; }
        [DataMember]
        public DateTime startDate { get; set; }
        [DataMember]
        public DateTime endDate { get; set; }
        [DataMember]
        public string contact { get; set; }
        [DataMember]
        public string phone { get; set; }
        [DataMember]
        public string gatheringPlace { get; set; }
        [DataMember]
        public DateTime releaseDate { get; set; }
        [DataMember]
        public DateTime createDate { get; set; }
        [DataMember]
        public int status { get; set; }
    }

    [DataContract]
    public class Address
    {
        [DataMember]
        public string addr { get; set; }
        [DataMember]
        public decimal lng { get; set; }
        [DataMember]
        public decimal lat { get; set; }
    }
}