using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Glz.Infrastructure;

namespace Glz.GreensJob.WebApi.Models
{
    public class OutOnLineApplicantModel : ResultBase
    {
        public List<Employer> employerList { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int? TotalPages { get; set; }
        public int? TotalRecords { get; set; }
    }
    /// <summary>
    /// 员工信息
    /// </summary>
    public class Employer
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        public int userID { get; set; }
        /// <summary>
        /// 报名记录Id
        /// </summary>
        public int enrollID { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string employerName { get; set; }
        /// <summary>
        /// 工作天数
        /// </summary>
        public int workDays { get; set; }
        /// <summary>
        /// 是否做过
        /// </summary>
        public bool Experienced { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string endDate { get; set; }
        /// <summary>
        /// 报名日期列表
        /// </summary>
        public IEnumerable<DateTime> enrollDates { get; set; }
        /// <summary>
        /// 录用状态
        /// </summary>
        public int employStatus { get; set; }
        /// <summary>
        /// 工作地点
        /// </summary>
        public string jobAddress { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string phoneNum { get; set; }
        /// <summary>
        /// 职位Id
        /// </summary>
        public int jobId { get; set; }
    }
}