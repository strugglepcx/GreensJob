using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Glz.Infrastructure;


namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class InFeedBackModel: RequestParamBase
    {
        [DataMember]
        public int category { get; set; }
        [DataMember]
        public int memberID { get; set; }
        [DataMember]
        public int memberCategory { get; set; }
        [DataMember]
        public string contact { get; set; }
        [DataMember]
        public string content { get; set; }
    }
}