using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class OutUploadModel : ResultBase
    {
        [DataMember]
        public List<string> Data { get; set; }
    }

    [DataContract]
    public class UploadResult
    {
        [DataMember]
        public string Url { get; set; }
    }
}