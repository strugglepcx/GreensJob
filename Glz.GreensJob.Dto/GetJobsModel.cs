using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Glz.GreensJob.Dto
{
    /// <summary>
    /// 获取列表模型
    /// </summary>
    public class GetJobsModel
    {
        /// <summary>
        /// 工作Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public int @class { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public int jobClass { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string jobClassName { get; set; }
        /// <summary>
        /// 职位标题
        /// </summary>
        public string JobTitle { get; set; }
        /// <summary>
        /// 职位详情
        /// </summary>
        public string jobDes { get; set; }
        /// <summary>
        /// 审核过的企业发布的职位是认证的 1 认证的 0 没有认证的
        /// </summary>
        public int audit { get; set; }
        /// <summary>
        /// 薪资
        /// </summary>
        public string salary { get; set; }
        /// <summary>
        /// 1 日结 2 周结 3 月结 4 完工结
        /// </summary>
        public string clearanceMethod { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime releaseTime { get; set; }
        /// <summary>
        /// 上班天数
        /// </summary>
        public string dayNum { get; set; }
        /// <summary>
        /// 开始工作时间
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// 距离
        /// </summary>
        public string distance { get; set; }

        /// <summary>
        /// 是否收藏
        /// </summary>
        public bool CollectState { get; set; }
        /// <summary>
        /// 是否报名
        /// </summary>
        public bool ApplyState { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal lng { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal lat { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string districtName { get; set; }
        /// <summary>
        /// 录用状态 0:未报名 1:报名 5:录用未确认 10:录用
        /// </summary>
        public int EmployStatu { get; set; }
        /// <summary>
        ///  当前时间到工作结束时间的天数
        /// </summary>
        public int nowToEndDateDayNum { get; set; }
        /// <summary>
        /// 是否限制男女
        /// </summary>
        public int isSex { get; set; }
        /// <summary>
        /// 性别要求 1 男， 2 女
        /// </summary>
        public int gender { get; set; }

        [JsonIgnore]
        public DbGeography Location { get; set; }
    }
}
