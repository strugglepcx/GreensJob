using System;
using System.Runtime.Serialization;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class OutPhoneStartModel : ResultBase
    {
        [DataMember]
        public int startPageiId { get; set; }
        [DataMember]
        public string startPageUrl { get; set; }
        [DataMember]
        public int version { get; set; }
        [DataMember]
        public string versionUrl { get; set; }
        [DataMember]
        public int editType { get; set; }


    }
}