using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Domain.Models;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class CountyTypeConfig : EntityTypeConfiguration<County>
    {
        public CountyTypeConfig()
        {
            ToTable("T_County");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("County_ID");
            Property(x => x.name).HasColumnName("County_Name");
            Property(x => x.cityID).HasColumnName("City_ID");
        }
    }
}
