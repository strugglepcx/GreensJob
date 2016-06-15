using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Domain.Models;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class JobCategoryTypeConfig : EntityTypeConfiguration<JobCategory>
    {
        public JobCategoryTypeConfig()
        {
            ToTable("T_JobCategory");
            HasKey(m => m.ID);
            Property(m => m.ID).HasColumnName("JobCategory_ID");
            Property(m => m.name).HasColumnName("JobCategory_Name");
            HasMany(e => e.Jobs).WithRequired(e => e.JobCategory).HasForeignKey(e => e.jobCategoryID).WillCascadeOnDelete(false);
        }
    }
}
