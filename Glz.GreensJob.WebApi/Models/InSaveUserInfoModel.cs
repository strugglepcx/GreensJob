using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    public class InSaveUserInfoModel : RequestParamBase
    {
        public int userID { get; set; }

        public string userName { get; set; }

        public string userMobile { get; set; }

        public string password { get; set; }

        public bool finicialRight { get; set; }

        public bool releaseJobRight { get; set; }

        public bool importEmployeeRight { get; set; }

        public bool addUserRight { get; set; }

        public bool modifyUserRight { get; set; }

        public bool deleteUserRight { get; set; }
    }
}