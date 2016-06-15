using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    public class InDeleteUserModel:RequestParamBase
    {
        public int userID { get; set; }
    }
}