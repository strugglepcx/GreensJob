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
    /// T_District
    /// </summary>
    public class DistrictTypeConfig : EntityTypeConfiguration<District>
    {
        public DistrictTypeConfig()
        {
            ToTable("T_District");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.City_ID).HasColumnName("City_ID");
        }
    }
}

