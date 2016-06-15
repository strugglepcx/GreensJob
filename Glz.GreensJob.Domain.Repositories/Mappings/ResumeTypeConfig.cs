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
    /// T_Resume
    /// </summary>
    public class ResumeTypeConfig : EntityTypeConfiguration<Resume>
    {
        public ResumeTypeConfig()
        {
            ToTable("T_Resume");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("Resume_ID");
            Property(x => x.City_ID).HasColumnName("City_ID");
            Property(x => x.University_ID).HasColumnName("University_ID");
            Property(x => x.Name).HasColumnName("Resume_Name");
            Property(x => x.Gender).HasColumnName("Resume_Gender");
            Property(x => x.Age).HasColumnName("Resume_Age");
            Property(x => x.Height).HasColumnName("Resume_Height");
            Property(x => x.Weight).HasColumnName("Resume_Weight");
            Property(x => x.HealthCertificate).HasColumnName("Resume_HealthCertificate");
            Property(x => x.IDNumber).HasColumnName("Resume_IDNumber");
            Property(x => x.Major).HasColumnName("Resume_Major");
            Property(x => x.Education).HasColumnName("Resume_Education");
            Property(x => x.Image).HasColumnName("Resume_Image");
            Property(x => x.Birthday).HasColumnName("Resume_Birthday");
            Property(x => x.Description).HasColumnName("Resume_Description");
            Property(x => x.CreateTime).HasColumnName("CreateTime");
            Property(x => x.UpdateTime).HasColumnName("UpdateTime");
            HasRequired(x => x.JobSeeker).WithOptional(y => y.Resume).WillCascadeOnDelete(true);
            HasRequired(x => x.University).WithMany().HasForeignKey(x => x.University_ID);
            HasRequired(x => x.OpenCity).WithMany().HasForeignKey(x => x.City_ID);
        }
    }
}

