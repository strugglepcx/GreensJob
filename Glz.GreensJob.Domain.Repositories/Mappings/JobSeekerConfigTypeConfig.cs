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
    /// T_JobSeekerConfig
    /// </summary>
    public class JobSeekerConfigTypeConfig : EntityTypeConfiguration<JobSeekerConfig>
    {
        public JobSeekerConfigTypeConfig()
        {
            ToTable("T_JobSeekerConfig");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID");
            Property(x => x.RecruitMessage).HasColumnName("RecruitMessage");
            Property(x => x.UrgentJobMessage).HasColumnName("UrgentJobMessage");
            Property(x => x.CreateTime).HasColumnName("CreateTime");
            Property(x => x.UpdateTime).HasColumnName("UpdateTime");
            HasRequired(x => x.JobSeeker).WithOptional(y => y.JobSeekerConfig).WillCascadeOnDelete(true);
        }
    }
}

