using System;
using System.Runtime.Serialization;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class InComplaintModel : RequestParamBase
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int jobID { get; set; }
        [DataMember]
        public int userID { get; set; }
        [DataMember]
        public int category { get; set; }
        [DataMember]
        public string content { get; set; }
        [DataMember]
        public DateTime createDate { get; set; }
    }
}