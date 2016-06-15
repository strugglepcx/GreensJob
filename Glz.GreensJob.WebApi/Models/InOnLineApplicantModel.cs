using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glz.GreensJob.Dto.RequestParams;

namespace Glz.GreensJob.WebApi.Models
{
    public class InOnLineApplicantModel : IdentityRequestParam
    {
        public int userID { get; set; }
        public int jobGroupID { get; set; }
        public string keyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}