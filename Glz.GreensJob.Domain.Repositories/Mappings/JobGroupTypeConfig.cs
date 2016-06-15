using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Domain.Models;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class JobGroupTypeConfig : EntityTypeConfiguration<JobGroup>
    {
        public JobGroupTypeConfig()
        {
            ToTable("T_JobGroup");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("JobGroup_ID");
            Property(x => x.jobCategoryID).HasColumnName("JobCategory_ID");
            Property(x => x.jobClassifyID).HasColumnName("JobClassify_ID");
            Property(x => x.jobSchduleID).HasColumnName("JobSchdule_ID");
            Property(x => x.payCategoryID).HasColumnName("PayCategory_ID");
            Property(x => x.payUnitID).HasColumnName("PayUnit_ID");
            Property(x => x.publisherID).HasColumnName("Publisher_ID");
            Property(x => x.name).HasColumnName("JobGroup_Name");
            Property(x => x.genderLimit).HasColumnName("JobGroup_GenderLimit");
            Property(x => x.heightLimit).HasColumnName("JobGroup_HeightLimit");
            Property(x => x.recruitNum).HasColumnName("JobGroup_RecruitNum");
            Property(x => x.autoTimeShare).HasColumnName("JobGroup_AutoTimeShare");
            Property(x => x.erollMethod).HasColumnName("JobGroup_ErollMethod");
            Property(x => x.startDate).HasColumnName("JobGroup_StartDate");
            Property(x => x.endDate).HasColumnName("JobGroup_EndDate");
            Property(x => x.salary).HasColumnName("JobGroup_Salary");
            Property(x => x.addrs).HasColumnName("JobGroup_Address");
            Property(x => x.content).HasColumnName("JobGroup_Content");
            Property(x => x.urgent).HasColumnName("JobGroup_Urgent");
            Property(x => x.healthCertificate).HasColumnName("JobGroup_HealthCertificate");
            Property(x => x.interview).HasColumnName("JobGroup_Interview");
            Property(x => x.interviewPlace).HasColumnName("JobGroup_InterviewPlace");
            Property(x => x.employer).HasColumnName("JobGroup_Employer");
            Property(x => x.contactMan).HasColumnName("JobGroup_Contact");
            Property(x => x.mobileNumber).HasColumnName("JobGroup_MobileNumber");
            Property(x => x.gatheringPlace).HasColumnName("JobGroup_GatheringPlace");
            Property(x => x.releaseDate).HasColumnName("JobGroup_ReleaseDate");
            Property(x => x.createDate).HasColumnName("JobGroup_CreateDate");
            Property(x => x.status).HasColumnName("JobGroup_Status");
            Property(x => x.companyID).HasColumnName("JobGroup_CompanyID");
            Property(x => x.applicantNum).HasColumnName("JobGroup_ApplicantNum");
            Property(x => x.employNum).HasColumnName("JobGroup_EmployNum");
            Property(x => x.onlineEmployNum).HasColumnName("JobGroup_OnlineEmployNum");
            Property(x => x.offineEmployNum).HasColumnName("JobGroup_OffineEmployNum");
            Property(x => x.continousDays).HasColumnName("JobGroup_ContinousDays");
            HasMany(x => x.Jobs).WithRequired(y => y.JobGroup).HasForeignKey(x => x.groupID).WillCascadeOnDelete(true);
            HasMany(x => x.Universitys).WithMany(y => y.JobGroups).Map(m =>
            {
                m.MapLeftKey("JobGroup_ID");
                m.MapRightKey("University_ID");
                m.ToTable("T_JobGroupToUniversity");
            });
        }
    }
}
