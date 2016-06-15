using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto
{
    /// <summary>
    /// 搜索记录
    /// </summary>
    public class SearchRecordModel
    {
        /// <summary>
        /// 搜索记录Id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 微信OpenId
        /// </summary>
        public int OpenId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 距离范围
        /// </summary>
        public int Distance { get; set; }
        /// <summary>
        /// 职位类型
        /// </summary>
        public int Class { get; set; }
        /// <summary>
        /// 日程类型
        /// </summary>
        public int Schedule { get; set; }
        /// <summary>
        /// 支付周期类型
        /// </summary>
        public int PayMethod { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
