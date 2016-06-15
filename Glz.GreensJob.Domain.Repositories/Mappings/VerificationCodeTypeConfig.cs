using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Domain.Models;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class VerificationCodeTypeConfig : EntityTypeConfiguration<VerificationCode>
    {
        public VerificationCodeTypeConfig()
        {
            ToTable("T_VerificationCode");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("VerificationCode_ID");
            Property(x => x.code).HasColumnName("VerificationCode_Code");
            Property(x => x.mobile).HasColumnName("VerificationCode_Mobile");
            Property(x => x.createDate).HasColumnName("VerificationCode_CreateDate");
        }
    }
}
