using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Domain.Models;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class PayUnitTypeConfig : EntityTypeConfiguration<PayUnit>
    {
        public PayUnitTypeConfig()
        {
            ToTable("T_PayUnit");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("PayUnit_ID");
            Property(x => x.name).HasColumnName("PayUnit_Name");
        }
    }
}
