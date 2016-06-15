using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Domain.Models;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class CityTypeConfig : EntityTypeConfiguration<City>
    {
        public CityTypeConfig()
        {
            ToTable("T_City");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("City_ID");
            Property(x => x.name).HasColumnName("City_Name");
            Property(x => x.provinceID).HasColumnName("Province_ID");
        }
    }
}
