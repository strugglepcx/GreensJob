using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    public class GetDailyRecruitRequestParam : IdentityRequestParam
    {
        public int JobGroupId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
