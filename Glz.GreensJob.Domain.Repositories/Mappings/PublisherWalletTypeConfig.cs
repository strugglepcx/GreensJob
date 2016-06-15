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
    /// T_PublisherWallet
    /// </summary>
    public class PublisherWalletTypeConfig : EntityTypeConfiguration<PublisherWallet>
    {
        public PublisherWalletTypeConfig()
        {
            ToTable("T_PublisherWallet");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID");
            Property(x => x.TotalAmounts).HasColumnName("TotalAmounts");
            Property(x => x.FrozenAmounts).HasColumnName("FrozenAmounts");
            Property(x => x.ActualAmounts).HasColumnName("ActualAmounts");
            Property(x => x.LastUpdateAmounts).HasColumnName("LastUpdateAmounts");
            Property(x => x.LastUpdateTime).HasColumnName("LastUpdateTime");
            Property(x => x.CreateTime).HasColumnName("CreateTime");
            Property(x => x.Password).HasColumnName("Password");
            Property(x => x.LastExtractTime).HasColumnName("LastExtractTime");
            Property(x => x.LastUpdatePasswordTime).HasColumnName("LastUpdatePasswordTime");
            HasRequired(x => x.Publisher).WithOptional(y => y.PublisherWallet).WillCascadeOnDelete(true);
            HasMany(x => x.PublisherWalletActionLogs).WithRequired(y => y.PublisherWallet).HasForeignKey(y => y.PublisherWallet_ID).WillCascadeOnDelete(true);
        }
    }
}

