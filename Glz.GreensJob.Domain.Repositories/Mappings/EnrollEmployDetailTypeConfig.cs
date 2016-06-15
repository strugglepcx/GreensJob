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
    /// T_EnrollEmployDetail
    /// </summary>
    public class EnrollEmployDetailTypeConfig : EntityTypeConfiguration<EnrollEmployDetail>
    {
        public EnrollEmployDetailTypeConfig()
        {
            ToTable("T_EnrollEmployDetail");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID");
            Property(x => x.Enroll_ID).HasColumnName("Enroll_ID");
            Property(x => x.Date).HasColumnName("Date");
            Property(x => x.StartTime).HasColumnName("StartTime");
            Property(x => x.EndTime).HasColumnName("EndTime");
        }
    }
}

