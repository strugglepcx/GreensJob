using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    [DataContract]
    public class OutDeptModel : ResultBase
    {
        [DataMember]
        public int DeptID { get; set; }


        [DataMember]
        public string DeptName { get; set; }
    }
}