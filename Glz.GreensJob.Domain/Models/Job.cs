using Apworks;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using Glz.GreensJob.Domain.Enums;

namespace Glz.GreensJob.Domain.Models
{
    public class Job : IAggregateRoot<int>
    {
        public Job()
        {
            JobRecruitDetails = new HashSet<JobRecruitDetail>();
            Enrolls = new HashSet<Enroll>();
        }
        public int ID { get; set; }
        public string name { get; set; }
        public int publisherID { get; set; }
        public int jobCategoryID { get; set; }
        public int jobClassifyID { get; set; }
        public int jobSchduleID { get; set; }
        public int payCategoryID { get; set; }
        public int payUnitID { get; set; }
        public int genderLimit { get; set; }
        public int heightLimit { get; set; }
        public bool autoTimeShare { get; set; }
        public int erollMethod { get; set; }
        public int recruitNum { get; set; }
        public string addr { get; set; }
        public int groupID { get; set; }
        public decimal lng { get; set; }
        public decimal lat { get; set; }
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
        public string countyName { get; set; }
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
        public int browseNum { get; set; }
        public int collectNum { get; set; }
        public DbGeography Location { get; set; }
        /// <summary>
        /// 城市Id
        /// </summary>
        public int City_ID { get; set; }
        /// <summary>
        /// 城区Id
        /// </summary>
        public int District_ID { get; set; }
        /// <summary>
        /// 是否能够报名
        /// </summary>
        public int canEnroll { get; set; }

        public virtual JobCategory JobCategory { get; set; }

        public virtual JobClassify JobClassify { get; set; }

        public virtual PayCategory PayCategory { get; set; }

        public virtual PayUnit PayUnit { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual List<Collect> Collects { get; set; }
        public virtual Company Company { get; set; }
        public virtual JobGroup JobGroup { get; set; }
        public virtual District District { get; set; }

        public virtual ICollection<JobRecruitDetail> JobRecruitDetails { get; set; }

        public virtual ICollection<Enroll> Enrolls { get; set; }
    }
}
