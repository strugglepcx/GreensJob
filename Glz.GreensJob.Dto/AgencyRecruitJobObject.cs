using System;

namespace Glz.GreensJob.Dto
{
    public class AgencyRecruitJobObject
    {
        /// <summary>
        ///     代招职位编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        ///     代招职位名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///     联系人
        /// </summary>
        public string contact { get; set; }

        /// <summary>
        ///     联系电话
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        ///     招聘人数
        /// </summary>
        public int recruitNum { get; set; }

        /// <summary>
        ///     结算单位
        /// </summary>
        public int payUnit { get; set; }

        /// <summary>
        ///     工资
        /// </summary>
        public decimal salary { get; set; }

        /// <summary>
        ///     工作地址
        /// </summary>
        public string addr { get; set; }

        /// <summary>
        ///     审核状态
        /// </summary>
        public int status { get; set; }

        /// <summary>
        ///     开始时间
        /// </summary>
        public DateTime startDate { get; set; }

        /// <summary>
        ///     结束时间
        /// </summary>
        public DateTime endDate { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime createDate { get; set; }
    }
}