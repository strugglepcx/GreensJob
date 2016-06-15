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
    /// T_ExtractApply
    /// </summary>
    public class ExtractApplyTypeConfig : EntityTypeConfiguration<ExtractApply>
    {
        public ExtractApplyTypeConfig()
        {
            ToTable("T_ExtractApply");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID");
            Property(x => x.JobSeeker_ID).HasColumnName("JobSeeker_ID");
            Property(x => x.Amount).HasColumnName("Amount");
            Property(x => x.BankCardNo).HasColumnName("BankCardNo");
            Property(x => x.CreateTime).HasColumnName("CreateTime");
            Property(x => x.ExecuteTime).HasColumnName("ExecuteTime");
            Property(x => x.Executor_ID).HasColumnName("Executor_ID");
            Property(x => x.State).HasColumnName("State");
        }
    }
}

