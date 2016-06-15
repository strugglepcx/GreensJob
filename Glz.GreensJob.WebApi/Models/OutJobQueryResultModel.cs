using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class OutJobQueryResultModel : ResultBase
    {
        [DataMember]
        public List<JobQueryResult> Data { get; set; }
    }

    [DataContract]
    public class JobQueryResult
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int classifyID { get; set; }
        [DataMember]
        public string classifyName { get; set; }
        [DataMember]
        public string payCategory { get; set; }
        [DataMember]
        public string payUnit { get; set; }
        [DataMember]
        public int recruitNum { get; set; }
        [DataMember]
        public int salary { get; set; }
        [DataMember]
        public DateTime releaseDate { get; set; }
    }
}