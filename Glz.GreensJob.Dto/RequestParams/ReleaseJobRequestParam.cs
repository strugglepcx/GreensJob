using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Glz.GreensJob.Dto.RequestParams
{
    /// <summary>
    /// 发布职位请求参数
    /// </summary>
    public class ReleaseJobRequestParam : IdentityRequestParam
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 类型Id
        /// </summary>
        public int jobClassifyID { get; set; }
        /// <summary>
        /// 档期Id
        /// </summary>
        public int jobCategoryID { get; set; }
        /// <summary>
        /// 性别 男 1、女 0、男女不限 2
        /// </summary>
        public int genderLimit { get; set; }
        /// <summary>
        /// 身高限制158以上(0)，160以上(1)，165以上(2)，168以上(3)，170以上(4)，175以上(5)，180以上(6)，185以上(7)
        /// </summary>
        public int heightLimit { get; set; }
        /// <summary>
        /// 招聘人数
        /// </summary>
        public int recruitNum { get; set; }
        /// <summary>
        /// 1 按天报名，2及以上表示至少连续的天数
        /// </summary>
        public int erollMethod { get; set; }
        /// <summary>
        /// 招聘起始时间
        /// </summary>
        public DateTime startDate { get; set; }
        /// <summary>
        /// 招聘结束时间
        /// </summary>
        public DateTime endDate { get; set; }
        /// <summary>
        /// 工作开始时间
        /// </summary>
        public DateTime startTime { get; set; }
        /// <summary>
        /// 工作结束时间
        /// </summary>
        public DateTime endTime { get; set; }
        /// <summary>
        /// 工作地点
        /// </summary>
        public List<AddressObject> jobAdrressList { get; set; }
        /// <summary>
        /// 日结 1 周结 2 月结3完工结 4
        /// </summary>
        public int payCategoryID { get; set; }
        /// <summary>
        /// 工资单位，/小时（0） /天（1），/周（2） /月（3）
        /// </summary>
        public int payUnitID { get; set; }
        /// <summary>
        /// 工资
        /// </summary>
        public decimal salary { get; set; }
        /// <summary>
        /// 录用后是否消息提醒
        /// </summary>
        public int urgent { get; set; }
        /// <summary>
        /// 发布短信学校列表
        /// </summary>
        public List<int> msgByUniversityList { get; set; }
        /// <summary>
        /// 是否需要健康证
        /// </summary>
        public int healthCertificate { get; set; }
        /// <summary>
        /// 是否需要面试
        /// </summary>
        public int interview { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string employer { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string contactMan { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string mobileNumber { get; set; }
        /// <summary>
        /// 集合地点
        /// </summary>
        public string gatheringPlace { get; set; }
        /// <summary>
        /// 面试地点
        /// </summary>
        public string interviewPlace { get; set; }
        /// <summary>
        /// 状态 1：草稿，2：待审核
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 职位详情
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 连续工作天数
        /// </summary>
        public int continousDays { get; set; }
        /// <summary>
        /// 是否分时
        /// </summary>
        public bool autoTimeShare { get; set; }
    }
}