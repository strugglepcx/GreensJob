using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    public class InRefreshJobModel : RequestParamBase
    {
        public int jobGroupID { get; set; }
    }
}