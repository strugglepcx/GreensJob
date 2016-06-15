using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class InJobQueryModel : RequestParamBase
    {
        [DataMember]
        public int distance { get; set; }
        [DataMember]
        public int classify { get; set; }
        [DataMember]
        public int schedule { get; set; }
        [DataMember]
        public int payMethod { get; set; }
    }
}