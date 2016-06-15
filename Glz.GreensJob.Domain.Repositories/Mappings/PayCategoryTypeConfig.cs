using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Domain.Models;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class PayCategoryTypeConfig : EntityTypeConfiguration<PayCategory>
    {
        public PayCategoryTypeConfig()
        {
            ToTable("T_PayCategory");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("PayCategory_ID");
            Property(x => x.name).HasColumnName("PayCategory_Name");
        }
    }
}
