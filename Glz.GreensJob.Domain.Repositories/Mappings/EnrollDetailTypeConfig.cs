using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Domain.Models;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class EnrollDetailTypeConfig : EntityTypeConfiguration<EnrollDetail>
    {
        public EnrollDetailTypeConfig()
        {
            ToTable("T_EnrollDetail");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("EnrollDetail_ID");
            Property(x => x.enrollID).HasColumnName("Enroll_ID");
            Property(x => x.date).HasColumnName("EnrollDetail_Date");
            Property(x => x.start).HasColumnName("EnrollDetail_Start");
            Property(x => x.end).HasColumnName("EnrollDetail_End");
            Property(x => x.isEmploy).HasColumnName("EnrollDetail_IsEmploy");
            Property(x => x.isRetired).HasColumnName("EnrollDetail_IsRetired");
        }
    }
}
