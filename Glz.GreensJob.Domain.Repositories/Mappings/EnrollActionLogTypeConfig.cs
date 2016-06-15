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
    /// 报名操作记录Mapping
    /// </summary>
    public class EnrollActionLogTypeConfig : EntityTypeConfiguration<EnrollActionLog>
    {
        public EnrollActionLogTypeConfig()
        {
            ToTable("T_EnrollActionLog");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID");
            Property(x => x.Enroll_ID).HasColumnName("Enroll_ID");
            Property(x => x.Job_ID).HasColumnName("Job_ID");
            Property(x => x.Job_Name).HasColumnName("Job_Name");
            Property(x => x.Job_GroupID).HasColumnName("Job_GroupID");
            Property(x => x.JobSeeker_ID).HasColumnName("JobSeeker_ID");
            Property(x => x.ActionID).HasColumnName("ActionID");
            Property(x => x.ActionName).HasColumnName("ActionName");
            Property(x => x.CreateTime).HasColumnName("CreateTime");
            HasRequired(x => x.Enroll).WithMany().HasForeignKey(x => x.Enroll_ID).WillCascadeOnDelete(false);
        }
    }
}

