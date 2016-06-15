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
    public class CompanyTypeConfig : EntityTypeConfiguration<Company>
    {
        public CompanyTypeConfig()
        {
            ToTable("T_Company");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("Company_ID");
            Property(x => x.cityID).HasColumnName("City_ID");
            Property(x => x.name).HasColumnName("Company_Name");
            Property(x => x.image).HasColumnName("Company_Image");
            Property(x => x.introduce).HasColumnName("Company_Introduce");
            Property(x => x.addr).HasColumnName("Company_Addr");
            Property(x => x.status).HasColumnName("Company_Status");
            Property(x => x.license).HasColumnName("Company_License");
            Property(x => x.certificates).HasColumnName("Company_Certificates");
            Property(x => x.contact).HasColumnName("Company_Contact");
            Property(x => x.tel).HasColumnName("Company_Tel");
            Property(x => x.createDate).HasColumnName("Company_CreateDate").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
}
