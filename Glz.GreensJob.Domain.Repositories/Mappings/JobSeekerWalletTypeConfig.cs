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
    /// T_JobSeekerWallet
    /// </summary>
    public class JobSeekerWalletTypeConfig : EntityTypeConfiguration<JobSeekerWallet>
    {
        public JobSeekerWalletTypeConfig()
        {
            ToTable("T_JobSeekerWallet");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID");
            Property(x => x.TotalAmounts).HasColumnName("TotalAmounts").HasPrecision(18, 2);
            Property(x => x.FrozenAmounts).HasColumnName("FrozenAmounts").HasPrecision(18, 2);
            Property(x => x.ActualAmounts).HasColumnName("ActualAmounts").HasPrecision(18, 2);
            Property(x => x.LastUpdateAmounts).HasColumnName("LastUpdateAmounts");
            Property(x => x.LastUpdateTime).HasColumnName("LastUpdateTime");
            Property(x => x.CreateTime).HasColumnName("CreateTime");
            Property(x => x.Password).HasColumnName("Password");
            Property(x => x.LastExtractTime).HasColumnName("LastExtractTime");
            Property(x => x.LastUpdatePasswordTime).HasColumnName("LastUpdatePasswordTime");
            HasRequired(x => x.JobSeeker).WithOptional(y => y.JobSeekerWallet).WillCascadeOnDelete(true);
            HasMany(x => x.JobSeekerWalletActionLogs).WithRequired(y => y.JobSeekerWallet).HasForeignKey(y => y.JobSeekerWallet_ID).WillCascadeOnDelete(true);
        }
    }
}

