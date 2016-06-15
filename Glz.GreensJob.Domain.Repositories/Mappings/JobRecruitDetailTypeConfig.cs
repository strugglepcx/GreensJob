using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Domain.Models;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    /// <summary>
    /// 职位招聘明细Mapping
    /// </summary>
    public class JobRecruitDetailTypeConfig : EntityTypeConfiguration<JobRecruitDetail>
    {
        public JobRecruitDetailTypeConfig()
        {
            ToTable("T_JobRecruitDetail");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("JobRecruitDetail_ID");
            Property(x => x.RecruitDate).HasColumnName("JobRecruitDetail_RecruitDate");
            Property(x => x.RecruitNum).HasColumnName("JobRecruitDetail_RecruitNum");
            Property(x => x.ApplicantNum).HasColumnName("JobRecruitDetail_ApplicantNum");
            Property(x => x.EmploymentNum).HasColumnName("JobRecruitDetail_EmploymentNum");
            Property(x => x.Job_ID).HasColumnName("Job_ID");
            Property(x => x.JobGroup_ID).HasColumnName("JobGroup_ID");
            HasRequired(x => x.Job).WithMany(y => y.JobRecruitDetails).HasForeignKey(x => x.Job_ID).WillCascadeOnDelete(true);
            HasRequired(x => x.JobGroup).WithMany().HasForeignKey(x => x.Job_ID).WillCascadeOnDelete(true);
        }
    }
}

