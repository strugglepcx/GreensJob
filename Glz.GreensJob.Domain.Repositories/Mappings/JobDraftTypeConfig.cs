using Glz.GreensJob.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class JobDraftTypeConfig: EntityTypeConfiguration<JobDraft>
    {
        public JobDraftTypeConfig()
        {
            ToTable("T_JobDraft");

            HasKey(x => x.ID);
        }
    }
}
