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
    /// T_EnrollPayDetail
    /// </summary>
    public class EnrollPayDetailTypeConfig : EntityTypeConfiguration<EnrollPayDetail>
    {
        public EnrollPayDetailTypeConfig()
        {
            ToTable("T_EnrollPayDetail");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID");
            Property(x => x.EnrollPay_ID).HasColumnName("EnrollPay_ID");
            Property(x => x.Enroll_ID).HasColumnName("Enroll_ID");
            Property(x => x.JobSeeker_ID).HasColumnName("JobSeeker_ID");
            Property(x => x.UserName).HasColumnName("UserName");
            Property(x => x.UserMobile).HasColumnName("UserMobile");
            Property(x => x.BankCardNo).HasColumnName("BankCardNo");
            Property(x => x.Job_ID).HasColumnName("Job_ID");
            Property(x => x.JobGroup_ID).HasColumnName("JobGroup_ID");
            Property(x => x.JobName).HasColumnName("JobName");
            Property(x => x.StartDate).HasColumnName("StartDate");
            Property(x => x.EndDate).HasColumnName("EndDate");
            Property(x => x.WorkDays).HasColumnName("WorkDays");
            Property(x => x.AmountSalary).HasColumnName("AmountSalary");
            Property(x => x.BasePay).HasColumnName("BasePay");
            Property(x => x.Bonus).HasColumnName("Bonus");
            Property(x => x.Debit).HasColumnName("Debit");
            Property(x => x.DaySalary).HasColumnName("DaySalary");
            Property(x => x.Publisher_ID).HasColumnName("Publisher_ID");
            Property(x => x.Company_ID).HasColumnName("Company_ID");
            Property(x => x.State).HasColumnName("State");
            Property(x => x.CreateTime).HasColumnName("CreateTime");
            Property(x => x.JobGroupName).HasColumnName("JobGroupName");
            Property(x => x.PayTime).HasColumnName("PayTime");
        }
    }
}

