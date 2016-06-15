using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto
{
    public class JobModel
    {
        /// <summary>
        /// 职位Id
        /// </summary>
        public int jobID { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string jobName { get; set; }
        /// <summary>
        /// 职位类型名称
        /// </summary>
        public string JobClass { get; set; }
        /// <summary>
        /// 所属区县
        /// </summary>
        public string CountyName { get; set; }
        /// <summary>
        /// 薪酬
        /// </summary>
        public decimal salary { get; set; }
        /// <summary>
        /// 结算单位
        /// </summary>
        public string salaryUnit { get; set; }
        /// <summary>
        /// 结算方式名称
        /// </summary>
        public string clearanceMethod { get; set; }
        /// <summary>
        /// 工作地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 距离
        /// </summary>
        public string distance { get; set; }
        /// <summary>
        /// 上班开始时间
        /// </summary>
        public string startTime { get; set; }
        /// <summary>
        /// 上班结束时间
        /// </summary>
        public string endtime { get; set; }
        /// <summary>
        /// 工作日期开始时间
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// 工作日期结束时间
        /// </summary>
        public string endDate { get; set; }
        /// <summary>
        /// 是否需要面试
        /// </summary>
        public bool isInterview { get; set; }
        /// <summary>
        /// 是否限制男女
        /// </summary>
        public int isSex { get; set; }
        /// <summary>
        /// 是否限制身高
        /// </summary>
        public bool isHeight { get; set; }
        /// <summary>
        /// 是否需要安全证
        /// </summary>
        public bool isHealth { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string contactMan { get; set; }
        /// <summary>
        /// 联系方式 仅在客户报名后才能电话按钮可用
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 工作内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 招聘人数（int），B端知道人天，C端不知道，所以仅提供招聘人数要求，等于（招//聘人天-已招聘人天）/每个人连续最短天数（向上去整）
        /// </summary>
        public int Persons { get; set; }
        /// <summary>
        /// 当前 职位申请人数  有10人以上则显示，小于则不显示
        /// </summary>
        public int applicantNumber { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 是否收藏
        /// </summary>
        public bool CollectState { get; set; }
        /// <summary>
        /// 是否报名
        /// </summary>
        public bool ApplyState { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 性别要求 1 男， 2 女
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// 身高要求
        /// </summary>
        public int height { get; set; }
        /// <summary>
        /// 职位经度，在地图时用
        /// </summary>
        public string jobLatitude { get; set; }
        /// <summary>
        /// //职位纬度
        /// </summary>
        public int jobLongitude { get; set; }
        /// <summary>
        /// 要求连续报名的时间
        /// </summary>
        public int continousDays { get; set; }
        /// <summary>
        /// 已经报名日期
        /// </summary>
        public List<DateTime> unvailableDates { get; set; }
        /// <summary>
        /// 分享标题
        /// </summary>
        public string shareTitle { get; set; }
        /// <summary>
        /// 分享图标
        /// </summary>
        public string shareIcon { get; set; }
        /// <summary>
        /// 分享详情
        /// </summary>
        public string shareDes { get; set; }
        /// <summary>
        /// 分享Url
        /// </summary>
        public string shareUrl { get; set; }
        /// <summary>
        /// 是否绑定
        /// </summary>
        public bool isBinding { get; set; }
        /// <summary>
        /// 当前时间到工作开始时间的天数
        /// </summary>
        public int nowToStartDateDayNum { get; set; }
        /// <summary>
        /// 工作天数
        /// </summary>
        public int dayNum { get; set; }
        /// <summary>
        /// 标签列表
        /// </summary>
        public List<string> tags { get; set; }
        /// <summary>
        /// 是否分时
        /// </summary>
        public bool autoTimeShare { get; set; }
        /// <summary>
        /// 招聘日期明细
        /// </summary>
        public List<JobRecruitDetailModel> JobRecruitDetails { get; set; }
    }
}