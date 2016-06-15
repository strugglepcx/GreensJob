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
    /// T_PublisherWalletActionLog
    /// </summary>
    public class PublisherWalletActionLogTypeConfig : EntityTypeConfiguration<PublisherWalletActionLog>
    {
        public PublisherWalletActionLogTypeConfig()
        {
            ToTable("T_PublisherWalletActionLog");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID");
            Property(x => x.PublisherWallet_ID).HasColumnName("PublisherWallet_ID");
            Property(x => x.Amount).HasColumnName("Amount");
            Property(x => x.CreateTime).HasColumnName("CreateTime");
            Property(x => x.UserName).HasColumnName("UserName");
            Property(x => x.ActionID).HasColumnName("ActionID");
            Property(x => x.ActionName).HasColumnName("ActionName");
            Property(x => x.JobGroup_ID).HasColumnName("JobGroup_ID");
            Property(x => x.Job_ID).HasColumnName("Job_ID");
            Property(x => x.Enroll_ID).HasColumnName("Enroll_ID");
            Property(x => x.OpenCity_ID).HasColumnName("OpenCity_ID");
            Property(x => x.PayType).HasColumnName("PayType");
            Property(x => x.PayTypeName).HasColumnName("PayTypeName");
            Property(x => x.PaySn).HasColumnName("PaySn");
            Property(x => x.BankCardNo).HasColumnName("BankCardNo");
            Property(x => x.ExtractApply_ID).HasColumnName("ExtractApply_ID");
            Property(x => x.State).HasColumnName("State");
            Property(x => x.JobName).HasColumnName("JobName");
            Property(x => x.JobGroupName).HasColumnName("JobGroupName");
            HasOptional(x => x.ExtractApply).WithMany().HasForeignKey(x => x.ExtractApply_ID);
        }
    }
}

