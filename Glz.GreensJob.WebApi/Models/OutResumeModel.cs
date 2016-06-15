using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glz.Infrastructure;
using System.Runtime.Serialization;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class OutResumeModel: ResultBase
    {
        [DataMember]
        public List<ResumeModel> Data { get; set; }
    }

    [DataContract]
    public class ResumeModel {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int userID { get; set; }
        [DataMember]
        public int cityID { get; set; }
        [DataMember]
        public string cityName { get; set; }
        [DataMember]
        public int provinceID { get; set; }
        [DataMember]
        public string provinceName { get; set; }
        [DataMember]
        public int universityID { get; set; }
        public string universityName { get; set; }
        [DataMember]
        public string Major { get; set; }
        [DataMember]
        public string Education { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Image { get; set; }
        [DataMember]
        public int height { get; set; }
        [DataMember]
        public int weight { get; set; }
        [DataMember]
        public bool Gender { get; set; }
        [DataMember]
        public string IDNumber { get; set; }
        [DataMember]
        public bool HealthCertificate { get; set; }
    }
}