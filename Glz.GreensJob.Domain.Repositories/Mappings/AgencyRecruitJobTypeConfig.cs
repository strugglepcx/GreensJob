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
    public class AgencyRecruitJobTypeConfig : EntityTypeConfiguration<AgencyRecruitJob>
    {
        public AgencyRecruitJobTypeConfig()
        {
            ToTable("T_AgencyRecruitJob");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("AgencyRecruitJob_ID");
            Property(x => x.name).HasColumnName("AgencyRecruitJob_Name");
            Property(x => x.contact).HasColumnName("AgencyRecruitJob_Contact");
            Property(x => x.phone).HasColumnName("AgencyRecruitJob_Phone");
            Property(x => x.recruitNum).HasColumnName("AgencyRecruitJob_RecruitNum");
            Property(x => x.payUnit).HasColumnName("PayUnit_ID");
            Property(x => x.salary).HasColumnName("AgencyRecruitJob_Salary");
            Property(x => x.addr).HasColumnName("AgencyRecruitJob_Addr");
            Property(x => x.status).HasColumnName("AgencyRecruitJob_Status");
            Property(x => x.startDate).HasColumnName("AgencyRecruitJob_StartDate");
            Property(x => x.endDate).HasColumnName("AgencyRecruitJob_EndDate");
            Property(x => x.createDate).HasColumnName("AgencyRecruitJob_CreateDate").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
}
