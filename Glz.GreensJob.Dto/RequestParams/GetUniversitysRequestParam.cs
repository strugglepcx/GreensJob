using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.Infrastructure;

namespace Glz.GreensJob.Dto.RequestParams
{
    public class GetUniversitysRequestParam : RequestParamBase
    {
        public int cityId { get; set; }
    }
}
