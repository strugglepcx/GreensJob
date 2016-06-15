using System;
using System.Runtime.Serialization;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class OutPhoneGameControl : ResultBase
    {
        [DataMember]
        public int isOpen { get; set; }
        [DataMember]
        public string gameUrl { get; set; }
    }
}