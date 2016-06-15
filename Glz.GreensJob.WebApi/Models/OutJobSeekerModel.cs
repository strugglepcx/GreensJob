using System;
using System.Web;
using System.Runtime.Serialization;
using Glz.Infrastructure;
using System.Collections.Generic;

namespace Glz.GreensJob.WebApi.Models
{

    [DataContract]
    public class OutJobSeekerModel : ResultBase
    {
        [DataMember]
        public List<JobSeekerModel> Data { get; set; }
    }

    [DataContract]
    public class JobSeekerModel {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string nickName { get; set; }
        [DataMember]
        public string virtualImage { get; set; }
        [DataMember]
        public string mobile { get; set; }
        [DataMember]
        public Guid SID { get; set; }
        [DataMember]
        public int collectNum { get; set; }
        [DataMember]
        public int applyNum { get; set; }
        [DataMember]
        public int employNum { get; set; }
        [DataMember]
        public decimal TotalAmounts { get; set; }
    }
}