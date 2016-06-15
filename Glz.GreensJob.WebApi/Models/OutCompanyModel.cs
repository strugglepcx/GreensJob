using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class OutCompanyModel : ResultBase
    {
        [DataMember]
        public List<CompanyModel> Data { get; set; }
    }

    [DataContract]
    public class CompanyModel
    {
        [DataMember]
        public int companyID { get; set; }

        [DataMember]
        public int cityID { get; set; }

        [DataMember]
        public string cityName { get; set; }

        [DataMember]
        public string companyName { get; set; }

        [DataMember]
        public string companyImage { get; set; }

        [DataMember]
        public string companyIntroduce { get; set; }

        [DataMember]
        public string companyAddr { get; set; }

        [DataMember]
        public string companyContact { get; set; }

        [DataMember]
        public string companyTel { get; set; }
        [DataMember]
        public string certificate { get; set; }
        [DataMember]
        public string license { get; set; }

        [DataMember]
        public int status { get; set; }
    }
}