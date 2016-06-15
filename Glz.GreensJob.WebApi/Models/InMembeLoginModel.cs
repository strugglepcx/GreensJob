using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Glz.Infrastructure;


namespace Glz.GreensJob.WebApi.Models
{
    public class InMembeLoginModel : RequestParamBase
    {
        public int userID { get; set; }
        public string userMobileNumber { get; set; }
        public string userPassword { get; set; }
        public string verificationCode { get; set; }

        public string wechatToken { get; set; }
    }
}