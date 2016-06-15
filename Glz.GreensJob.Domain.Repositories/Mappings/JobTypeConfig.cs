using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Domain.Models;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class JobTypeConfig : EntityTypeConfiguration<Job>
    {
        public JobTypeConfig()
        {
            ToTable("T_Job");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("Job_ID");
            Property(x => x.jobCategoryID).HasColumnName("JobCategory_ID");
            Property(x => x.jobClassifyID).HasColumnName("JobClassify_ID");
            Property(x => x.jobSchduleID).HasColumnName("JobSchdule_ID");
            Property(x => x.payCategoryID).HasColumnName("PayCategory_ID");
            Property(x => x.payUnitID).HasColumnName("PayUnit_ID");
            Property(x => x.publisherID).HasColumnName("Publisher_ID");
            Property(x => x.name).HasColumnName("Job_Name");
            Property(x => x.genderLimit).HasColumnName("Job_GenderLimit");
            Property(x => x.heightLimit).HasColumnName("Job_HeightLimit");
            Property(x => x.recruitNum).HasColumnName("Job_RecruitNum");
            Property(x => x.autoTimeShare).HasColumnName("Job_AutoTimeShare");
            Property(x => x.erollMethod).HasColumnName("Job_ErollMethod");
            Property(x => x.startDate).HasColumnName("Job_StartDate");
            Property(x => x.endDate).HasColumnName("Job_EndDate");
            Property(x => x.salary).HasColumnName("Job_Salary");
            Property(x => x.addr).HasColumnName("Job_Address");
            Property(x => x.groupID).HasColumnName("JobGroup_ID");
            Property(x => x.lng).HasColumnName("Job_Lng").HasPrecision(18, 6);
            Property(x => x.lat).HasColumnName("Job_Lat").HasPrecision(18, 6);
            Property(x => x.content).HasColumnName("Job_Content");
            Property(x => x.urgent).HasColumnName("Job_Urgent");
            Property(x => x.healthCertificate).HasColumnName("Job_HealthCertificate");
            Property(x => x.interview).HasColumnName("Job_Interview");
            Property(x => x.interviewPlace).HasColumnName("Job_InterviewPlace");
            Property(x => x.employer).HasColumnName("Job_Employer");
            Property(x => x.contactMan).HasColumnName("Job_Contact");
            Property(x => x.mobileNumber).HasColumnName("Job_MobileNumber");
            Property(x => x.gatheringPlace).HasColumnName("Job_GatheringPlace");
            Property(x => x.releaseDate).HasColumnName("Job_ReleaseDate");
            Property(x => x.createDate).HasColumnName("Job_CreateDate");
            Property(x => x.status).HasColumnName("Job_Status");
            Property(x => x.companyID).HasColumnName("Job_CompanyID");
            Property(x => x.Location).HasColumnName("Job_Location");
            Property(x => x.countyName).HasColumnName("Job_CountyName");
            Property(x => x.applicantNum).HasColumnName("Job_ApplicantNum");
            Property(x => x.employNum).HasColumnName("Job_EmployNum");
            Property(x => x.onlineEmployNum).HasColumnName("Job_OnlineEmployNum");
            Property(x => x.offineEmployNum).HasColumnName("Job_OffineEmployNum");
            Property(x => x.browseNum).HasColumnName("Job_BrowseNum");
            Property(x => x.collectNum).HasColumnName("Job_CollectNum");
            Property(x => x.continousDays).HasColumnName("Job_ContinousDays");
            Property(x => x.canEnroll).HasColumnName("Job_CanEnroll");

            HasMany(x => x.Collects).WithRequired(x => x.job).HasForeignKey(x => x.jobID).WillCascadeOnDelete(true);
            HasMany(x => x.Enrolls).WithRequired(y => y.Job).HasForeignKey(y => y.jobID).WillCascadeOnDelete(true);
            HasRequired(x => x.District).WithMany().HasForeignKey(x => x.District_ID);
        }
    }
}
