using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Domain.Models;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class CollectTypeConfig : EntityTypeConfiguration<Collect>
    {
        public CollectTypeConfig()
        {
            ToTable("T_Collect");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("Collect_ID");
            Property(x => x.jobID).HasColumnName("Job_ID");
            Property(x => x.jobSeekerID).HasColumnName("JobSeeker_ID");
            Property(x => x.status).HasColumnName("Collect_Status");
        }
    }
}
