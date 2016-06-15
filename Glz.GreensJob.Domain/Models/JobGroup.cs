using Apworks;
using System;
using System.Collections.Generic;
using Glz.GreensJob.Domain.Enums;

namespace Glz.GreensJob.Domain.Models
{
    public class JobGroup : IAggregateRoot<int>
    {
        public JobGroup()
        {
            Jobs = new HashSet<Job>();
        }
        /// <summary>
        /// 职位组Id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 发布者Id
        /// </summary>
        public int publisherID { get; set; }
        /// <summary>
        /// 职位类别Id
        /// </summary>
        public int jobCategoryID { get; set; }
        /// <summary>
        /// 职位类型Id
        /// </summary>
        public int jobClassifyID { get; set; }
        /// <summary>
        /// 暂时无效赋值与jobCategoryID一样
        /// </summary>
        public int jobSchduleID { get; set; }
        /// <summary>
        /// 支付类型Id
        /// </summary>
        public int payCategoryID { get; set; }
        /// <summary>
        /// 支付单位Id
        /// </summary>
        public int payUnitID { get; set; }
        /// <summary>
        /// 年龄限制 0：无限制
        /// </summary>
        public int genderLimit { get; set; }
        /// <summary>
        /// 身高限制 0：无限制
        /// </summary>
        public int heightLimit { get; set; }
        /// <summary>
        /// 是否分时
        /// </summary>
        public bool autoTimeShare { get; set; }
        /// <summary>
        /// 报名方式
        /// </summary>
        public int erollMethod { get; set; }
        /// <summary>
        /// 招聘天数
        /// </summary>
        public int recruitNum { get; set; }
        public string addrs { get; set; }
        public string content { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public decimal salary { get; set; }
        public bool urgent { get; set; }
        public bool healthCertificate { get; set; }
        public bool interview { get; set; }
        public string interviewPlace { get; set; }
        public string employer { get; set; }
        public string contactMan { get; set; }
        public string mobileNumber { get; set; }
        public string gatheringPlace { get; set; }
        public DateTime releaseDate { get; set; }
        public DateTime createDate { get; set; }
        public JobStatus status { get; set; }
        public int companyID { get; set; }
        public int continousDays { get; set; }
        /// <summary>
        /// 报名人数
        /// </summary>
        public int applicantNum { get; set; }
        /// <summary>
        /// 录用人数
        /// </summary>
        public int employNum { get; set; }
        /// <summary>
        /// 线上录用人数
        /// </summary>
        public int onlineEmployNum { get; set; }
        /// <summary>
        /// 线下录用人数
        /// </summary>
        public int offineEmployNum { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
        public virtual PayUnit PayUnit { get; set; }
        /// <summary>
        /// 招聘关联大学
        /// </summary>
        public virtual ICollection<University> Universitys { get; set; }
    }
}
