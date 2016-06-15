using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Dto
{
    /// <summary>
    /// 员工相关职位信息
    /// </summary>
    public class EmployeeJobInfo
    {
        /// <summary>
        /// 职位Id
        /// </summary>
        public int jobId { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string jobName { get; set; }
        /// <summary>
        /// 职位分类Id
        /// </summary>
        public string JobClassId { get; set; }
        /// <summary>
        /// 职位分类
        /// </summary>
        public string JobClass { get; set; }
        /// <summary>
        /// 薪资
        /// </summary>
        public decimal salary { get; set; }
        /// <summary>
        /// 薪资单位
        /// </summary>
        public string salaryUnit { get; set; }
        /// <summary>
        /// 结算类型
        /// </summary>
        public string payMethod { get; set; }
        /// <summary>
        /// 工作地点
        /// </summary>
        public string jobAddress { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// 工作总天数
        /// </summary>
        public string dayNum { get; set; }
        /// <summary>
        /// 起止日期信息
        /// </summary>
        public string actualStartEndDate { get; set; }
        /// <summary>
        /// 实际工作天数
        /// </summary>
        public int actualDayNum { get; set; }
        /// <summary>
        /// 收入金额
        /// </summary>
        public decimal incomeAmount { get; set; }
        /// <summary>
        /// 是否确认
        /// </summary>
        public int isConfirm { get; set; }
        /// <summary>
        /// 是否解雇
        /// </summary>
        public int isFired { get; set; }
        /// <summary>
        /// 开始结束时间 8:00-9:00
        /// </summary>
        public string startEndTime { get; set; }
        /// <summary>
        /// 报名时间列表
        /// </summary>
        public List<EnrollDateModel> EnrollDates { get; set; }
        /// <summary>
        /// 报名时间列表
        /// </summary>
        public List<EnrollDateModel> EmployDates { get; set; }
    }
}
