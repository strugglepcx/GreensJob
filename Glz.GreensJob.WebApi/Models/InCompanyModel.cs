using System.Runtime.Serialization;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class InCompanyModel
    {
        [DataMember]
        public int userID { get; set; }

        [DataMember]
        public int companyID { get; set; }

        [DataMember]
        public int cityID { get; set; }

        [DataMember]
        public string companyName { get; set; }

        [DataMember]
        public string companyImage { get; set; }

        [DataMember]
        public string companyIntroduce { get; set; }

        [DataMember]
        public string companyAddr { get; set; }

        [DataMember]
        public int status { get; set; }
    }
}