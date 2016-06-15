using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Domain.Models;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class JobClassifyTypeConfig : EntityTypeConfiguration<JobClassify>
    {
        public JobClassifyTypeConfig()
        {
            ToTable("T_JobClassify");

            HasKey(m => m.ID);

            Property(m => m.ID).HasColumnName("JobClassify_ID");
            Property(m => m.name).HasColumnName("JobClassify_Name");
        }
    }
}
