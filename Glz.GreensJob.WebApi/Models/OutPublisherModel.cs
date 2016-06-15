using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class OutPublisherModel : ResultBase
    {
        [DataMember]
        public PublisherModel userRecords { get; set; }
    }
    [DataContract]
    public class PublisherModel
    {
        [DataMember]
        public int userID { get; set; }

        [DataMember]
        public bool administor { get; set; }
        [DataMember]
        public List<UserRecord> uerRecord { get; set; }
    }

    [DataContract]
    public class UserRecord
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string userName { get; set; }
        [DataMember]
        public string userMobileNumber { get; set; }

        [DataMember]
        public bool addUserRight { get; set; }
        [DataMember]
        public bool modifyUserRight { get; set; }
        [DataMember]
        public bool deleteUserRight { get; set; }
        [DataMember]
        public bool finicialRight { get; set; }
        [DataMember]
        public bool releaseJobRight { get; set; }
        [DataMember]
        public bool importEmployeeRight { get; set; }
    }
}