using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.Infrastructure;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 完成申请请求操作
    /// </summary>
    public class CompleteExtractApplysRequestParam : RequestParamBase
    {
        /// <summary>
        /// 申请Id列表
        /// </summary>
        public IEnumerable<int> ExtractApplyIdList { get; set; }
    }
}
