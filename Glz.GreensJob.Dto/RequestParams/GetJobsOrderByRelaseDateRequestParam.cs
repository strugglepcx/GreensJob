using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 获取职位列表根据发布时间排序请求参数
    /// </summary>
    public class GetJobsOrderByRelaseDateRequestParam : IPageRequestParam
    {
        /// <summary>
        /// 全屏显示所有类型，1 促销 2 文员 3 派发 4 校内 5 店员 6 会展 0不限
        /// </summary>
        public int @class { get; set; }
        /// <summary>
        /// 1急招 2 周末兼职 3 假期  4 短期 0不限
        /// </summary>
        public int schedule { get; set; }
        /// <summary>
        /// 1 日结 2 周结 3 月结 4 完工结 0不限
        /// </summary>
        public int payMethod { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// 城区Id 0 全部
        /// </summary>
        public int district { get; set; }
        /// <summary>
        /// 城市Id
        /// </summary>
        public int city { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int pageIndex { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int pageSize { get; set; }
    }
}
