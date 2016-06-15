using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class OutMemberLoginModel : ResultBase
    {
        [DataMember]
        public List<MemberLoginData> Data { get; set; }
    }

    [DataContract]
    public class MemberLoginData
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string mobile { get; set; }

        [DataMember]
        public bool isAdmin { get; set; }

        [DataMember]
        public DateTime lastLoginDate { get; set; }

        [DataMember]
        public int companyID { get; set; }

        [DataMember]
        public string companyName { get; set; }

        [DataMember]
        public string companyImage { get; set; }

        [DataMember]
        public string companyAddr { get; set; }

        [DataMember]
        public int status { get; set; }
    }
}