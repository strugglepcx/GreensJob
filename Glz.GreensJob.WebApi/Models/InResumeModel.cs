using System.Runtime.Serialization;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class InResumeModel : RequestParamBase
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int userID { get; set; }
        [DataMember]
        public int cityID { get; set; }
        [DataMember]
        public int universityID { get; set; }
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