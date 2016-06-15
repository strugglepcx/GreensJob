using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    public class PublisherActionRequestParam : IdentityRequestParam
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string userMobile { get; set; }
        public bool addUserRight { get; set; }
        public bool deleteUserRight { get; set; }
        public bool finicialRight { get; set; }
        public bool importEmployeeRight { get; set; }
        public bool modifyUserRight { get; set; }
        public bool releaseJobRight { get; set; }
        public bool isAdmin { get; set; }
    }
}
