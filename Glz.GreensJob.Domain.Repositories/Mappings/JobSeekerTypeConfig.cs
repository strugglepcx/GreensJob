using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Domain.Models;

namespace Glz.GreensJob.Domain.Repositories.Mappings
{
    public class JobSeekerTypeConfig : EntityTypeConfiguration<JobSeeker>
    {
        public JobSeekerTypeConfig()
        {
            ToTable("T_JobSeeker");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("JobSeeker_ID");
            Property(x => x.nickName).HasColumnName("JobSeeker_NickName");
            Property(x => x.virtualImage).HasColumnName("JobSeeker_VirtualImage");
            Property(x => x.mobile).HasColumnName("JobSeeker_Mobile");
            Property(x => x.password).HasColumnName("JobSeeker_Password");
            Property(x => x.wechatToken).HasColumnName("JobSeeker_WechatToken");
            Property(x => x.payWechatAccount).HasColumnName("JobSeeker_PayWechatAccount");
            Property(x => x.weiboToken).HasColumnName("JobSeeker_WeiboToken");
            Property(x => x.SID).HasColumnName("JobSeeker_SID");
            Property(x => x.invitation).HasColumnName("JobSeeker_Invitation");
            Property(x => x.lastLoginDate).HasColumnName("JobSeeker_LastLoginDate");
            Property(x => x.createDate).HasColumnName("JobSeeker_CreateDate");
            Property(x => x.channelId).HasColumnName("JobSeeker_ChannelId");

            HasMany(x => x.Collects).WithRequired(x => x.jobSeeker).HasForeignKey(x => x.jobSeekerID);
            HasMany(x => x.ExtractApplys).WithRequired(x => x.JobSeeker).HasForeignKey(x => x.JobSeeker_ID).WillCascadeOnDelete(true);
            HasMany(x => x.JobSeekerMessages).WithRequired(x => x.JobSeeker).HasForeignKey(x => x.JobSeeker_ID).WillCascadeOnDelete(true);
            HasMany(x => x.Companys).WithMany(y => y.JobSeekers).Map(configuration =>
            {
                configuration.MapLeftKey("Company_ID");
                configuration.MapRightKey("JobSeeker_ID");
                configuration.ToTable("T_CompanyAttention");
            });
        }
    }
}
