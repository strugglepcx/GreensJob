using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Domain.Models;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class PublisherRightConfig : EntityTypeConfiguration<PublisherRight>
    {
        public PublisherRightConfig()
        {
            ToTable("T_PublisherRight");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("Publisher_ID");
            Property(x => x.AddUserRight).HasColumnName("AddUser_Right");
            Property(x => x.ModifyUserRight).HasColumnName("ModifyUser_Right");
            Property(x => x.DeleteUserRight).HasColumnName("DeleteUser_Right");
            Property(x => x.FinicialRight).HasColumnName("Finicial_Right");
            Property(x => x.ReleaseJobRight).HasColumnName("ReleaseJob_Right");
            Property(x => x.ImportEmployeeRight).HasColumnName("ImportEmployee_Right");
            HasRequired(x => x.Publisher).WithOptional(x => x.PublisherRight).WillCascadeOnDelete(true);
        }
    }
}
