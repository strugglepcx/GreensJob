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
    /// T_OpenCity
    /// </summary>
    public class OpenCityTypeConfig : EntityTypeConfiguration<OpenCity>
    {
        public OpenCityTypeConfig()
        {
            ToTable("T_OpenCity");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("OpenCity_ID");
            Property(x => x.Name).HasColumnName("OpenCity_Name");
        }
    }
}

