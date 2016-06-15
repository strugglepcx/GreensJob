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
    /// T_EnrollPay
    /// </summary>
    public class EnrollPayTypeConfig : EntityTypeConfiguration<EnrollPay>
    {
        public EnrollPayTypeConfig()
        {
            ToTable("T_EnrollPay");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID");
            Property(x => x.Publisher_ID).HasColumnName("Publisher_ID");
            Property(x => x.Company_ID).HasColumnName("Company_ID");
            Property(x => x.PayAmount).HasColumnName("PayAmount");
            Property(x => x.PayType).HasColumnName("PayType");
            Property(x => x.PaySn).HasColumnName("PaySn");
            Property(x => x.orderID).HasColumnName("orderID");
            Property(x => x.CreateTime).HasColumnName("CreateTime");
            Property(x => x.PayResult).HasColumnName("PayResult");
            HasMany(x => x.EnrollPayDetails).WithOptional(y => y.EnrollPay).HasForeignKey(y => y.EnrollPay_ID);
        }
    }
}

