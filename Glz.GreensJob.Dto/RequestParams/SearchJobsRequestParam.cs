using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 搜索职位请求参数
    /// </summary>
    public class SearchJobsRequestParam : WeiXinIdentityRequestParam
    {
        /// <summary>
        /// 1  1公里内 2  3公里内 3  5公里内 4 10公里内 5 大于10公里及以上 0不限  //和肖工确认公众号方式可以取得位置信息
        /// </summary>
        public IEnumerable<int> distance { get; set; }
        /// <summary>
        /// 全屏显示所有类型，1 促销 2 文员 3 派发 4 校内 5 店员 6 会展 0不限
        /// </summary>
        public IEnumerable<int> @class { get; set; }
        /// <summary>
        /// 1急招 2 周末兼职 3 假期  4 短期 0不限
        /// </summary>
        public IEnumerable<int> schedule { get; set; }
        /// <summary>
        /// 1 日结 2 周结 3 月结 4 完工结 0不限
        /// </summary>
        public IEnumerable<int> payMethod { get; set; }
        /// <summary>
        /// 周几
        /// </summary>
        public IEnumerable<int> WeekDay { get; set; }
        /// <summary>
        /// 日薪区间
        /// </summary>
        public IEnumerable<DailyRangeModel> dailyRange { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string lat { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string lng { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int pageIndex { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string keyWordCondition { get; set; }
    }
}
