using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    public class InEnrollModel : RequestParamBase
    {
        public int ID { get; set; }
        public int userID { get; set; }
        public int jobID { get; set; }
        public int introducer { get; set; }

        public int pageIndex { get; set; }
        public List<EnrollDateList> DateList { get; set; }
    }

    public class EnrollDateList
    {
        public DateTime date { get; set; }
    }
}