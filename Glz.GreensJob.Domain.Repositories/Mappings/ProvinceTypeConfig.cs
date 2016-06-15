using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Domain.Models;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class ProvinceTypeConfig : EntityTypeConfiguration<Province>
    {
        public ProvinceTypeConfig()
        {
            ToTable("T_Province");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("Province_ID");
            Property(x => x.name).HasColumnName("Province_Name");
            HasMany(x => x.Cities).WithRequired(y => y.Province).HasForeignKey(y => y.provinceID).WillCascadeOnDelete(false);
        }
    }
}
