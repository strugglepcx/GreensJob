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
    /// T_CompanyAttention
    /// </summary>
    public class CompanyAttentionTypeConfig : EntityTypeConfiguration<CompanyAttention>
    {
        public CompanyAttentionTypeConfig()
        {
            ToTable("T_CompanyAttention");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID");
            Property(x => x.Company_ID).HasColumnName("Company_ID");
            Property(x => x.JobSeeker_ID).HasColumnName("JobSeeker_ID");
            Property(x => x.CreateTime).HasColumnName("CreateTime").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
}

