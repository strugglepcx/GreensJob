using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class InPhoneGethomepage : RequestParamBase
    {
        [DataMember]
        public int pageIndex { get; set; }
    }

    [DataContract]
    public class InPhoneAdvertsList : RequestParamBase
    {
        [DataMember]
        public int pageIndex { get; set; }
        [DataMember]
        public int advertsType { get; set; }

    }
}