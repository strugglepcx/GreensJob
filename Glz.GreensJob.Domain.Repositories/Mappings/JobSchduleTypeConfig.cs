using Glz.GreensJob.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class JobSchduleTypeConfig : EntityTypeConfiguration<JobSchdule>
    {
        public JobSchduleTypeConfig()
        {
            ToTable("T_JobSchdule");

            HasKey(x => x.ID);
            Property(x => x.ID).HasColumnName("JobSchdule_ID");
            Property(x => x.name).HasColumnName("JobSchdule_Name");
        }
    }
}
