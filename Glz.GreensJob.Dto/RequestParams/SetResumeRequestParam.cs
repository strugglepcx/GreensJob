using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 设置简历请求参数
    /// </summary>
    public class SetResumeRequestParam : WeiXinIdentityRequestParam
    {
        public ResumeObject ResumeObject { get; set; }
    }
}
