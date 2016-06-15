using Glz.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class InQueryJobModel : RequestParamBase
    {
        [DataMember]
        public string Keyword { get; set; }
        [DataMember]
        public int cityID { get; set; }
        [DataMember]
        public int jobCategory { get; set; }
        [DataMember]
        public int jobClassify { get; set; }
        [DataMember]
        public int jobSchdule { get; set; }
        [DataMember]
        public int payCategory { get; set; }
        [DataMember]
        public int payUnit { get; set; }
        [DataMember]
        public int pageIndex { get; set; }
    }
}