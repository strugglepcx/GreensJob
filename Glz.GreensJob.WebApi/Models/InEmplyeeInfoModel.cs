using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class InEmplyeeInfoModel : IdentityRequestParam
    {
        [DataMember]
        public int jobGroupID { get; set; }
        [DataMember]
        public int[] showMethod { get; set; }
        [DataMember]
        public int PageIndex { get; set; }
        [DataMember]
        public int PageSize { get; set; }
    }
}