using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public interface IPageRequestParam
    {
        /// <summary>
        /// 页码
        /// </summary>
        int pageIndex { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        int pageSize { get; set; }
    }
}
