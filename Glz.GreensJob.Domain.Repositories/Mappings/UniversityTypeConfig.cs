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
    /// T_University
    /// </summary>
    public class UniversityTypeConfig : EntityTypeConfiguration<University>
    {
        public UniversityTypeConfig()
        {
            ToTable("T_University");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("University_ID");
            Property(x => x.cityID).HasColumnName("City_ID");
            Property(x => x.name).HasColumnName("University_Name");
        }
    }
}

