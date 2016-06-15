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
    public class PublisherTypeConfig : EntityTypeConfiguration<Publisher>
    {
        public PublisherTypeConfig()
        {
            ToTable("T_Publisher");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("Publisher_ID");
            Property(x => x.companyID).HasColumnName("Company_ID");
            Property(x => x.name).HasColumnName("Publisher_Name");
            Property(x => x.mobile).HasColumnName("Publisher_Mobile");
            Property(x => x.password).HasColumnName("Publisher_Password");
            Property(x => x.isAdmin).HasColumnName("Publisher_IsAdmin");
            Property(x => x.lastLoginDate).HasColumnName("Publisher_LastLoginDate");
            Property(x => x.createDate).HasColumnName("Publisher_CreateDate");
        }
    }
}
