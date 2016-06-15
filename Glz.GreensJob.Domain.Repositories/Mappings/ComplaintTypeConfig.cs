using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Domain.Models;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class ComplaintTypeConfig : EntityTypeConfiguration<Complaint>
    {
        public ComplaintTypeConfig()
        {
            ToTable("T_Complaint");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("Complaint_ID");
            Property(x => x.jobID).HasColumnName("Job_ID");
            Property(x => x.jobSeekerID).HasColumnName("JobSeeker_ID");
            Property(x => x.category).HasColumnName("Complaint_Category");
            Property(x => x.content).HasColumnName("Complaint_Content");
            Property(x => x.createDate).HasColumnName("Complaint_CreateDate");
        }
    }
}
