using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.Infrastructure;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 导入支付明细请求参数
    /// </summary>
    public class ImportPayDetailRequestParam : IdentityRequestParam//IdentityRequestParam
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 工作组Id
        /// </summary>
        public int jobGroupId { get; set; }
    }
}
