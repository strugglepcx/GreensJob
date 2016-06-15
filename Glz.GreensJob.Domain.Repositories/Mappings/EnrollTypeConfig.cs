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
    public class EnrollTypeConfig : EntityTypeConfiguration<Enroll>
    {
        public EnrollTypeConfig()
        {
            ToTable("T_Enroll");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("Enroll_ID");
            Property(x => x.jobID).HasColumnName("Job_ID");
            Property(x => x.jobSeekerID).HasColumnName("JobSeeker_ID");
            Property(x => x.introducer).HasColumnName("Enroll_Introducer");
            Property(x => x.experienced).HasColumnName("Enroll_Experienced");
            Property(x => x.name).HasColumnName("Enroll_Name");
            Property(x => x.mobile).HasColumnName("Enroll_Mobile");
            Property(x => x.employStatus).HasColumnName("Enroll_EmployStatus");
            Property(x => x.status).HasColumnName("Enroll_Status");
            Property(x => x.enrollMethod).HasColumnName("Enroll_EnrollMethod");
            Property(x => x.CreateTime).HasColumnName("Enroll_CreateTime");
            HasRequired(x => x.Job).WithMany().HasForeignKey(x => x.jobID);
            HasRequired(x => x.JobSeeker).WithMany().HasForeignKey(x => x.jobSeekerID);
            HasMany(x => x.EnrollDetails).WithRequired(y => y.Enroll).HasForeignKey(x => x.enrollID).WillCascadeOnDelete(true);
            HasMany(x => x.EnrollEmployDetails).WithRequired().HasForeignKey(x => x.Enroll_ID).WillCascadeOnDelete(true);
        }
    }
}
