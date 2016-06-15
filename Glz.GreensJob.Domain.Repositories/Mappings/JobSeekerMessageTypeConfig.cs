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
    /// T_JobSeekerMessage
    /// </summary>
    public class JobSeekerMessageTypeConfig : EntityTypeConfiguration<JobSeekerMessage>
    {
        public JobSeekerMessageTypeConfig()
        {
            ToTable("T_JobSeekerMessage");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID");
            Property(x => x.JobSeeker_ID).HasColumnName("JobSeeker_ID");
            Property(x => x.MessageContent).HasColumnName("MessageContent");
            Property(x => x.MessageType).HasColumnName("MessageType");
            Property(x => x.CreateTime).HasColumnName("CreateTime");
            Property(x => x.Job_ID).HasColumnName("Job_ID");
            Property(x => x.JobName).HasColumnName("JobName");

        }
    }
}

