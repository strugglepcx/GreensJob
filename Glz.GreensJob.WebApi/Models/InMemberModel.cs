using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    public class InMemberModel : RequestParamBase
    {
        public int userID { get; set; }
        public int id { get; set; }
        public int companyID { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string wechatToken { get; set; }
    }
}