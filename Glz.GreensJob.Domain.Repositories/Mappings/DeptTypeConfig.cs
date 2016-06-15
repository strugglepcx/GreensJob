using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Domain.Models;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class DeptTypeConfig : EntityTypeConfiguration<Dept>
    {
        public DeptTypeConfig()
        {
            ToTable("T_Dept");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("Dept_ID");
            Property(x => x.name).HasColumnName("Dept_Name");
        }
    }
}
