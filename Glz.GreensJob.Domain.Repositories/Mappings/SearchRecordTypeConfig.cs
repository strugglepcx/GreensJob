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
    /// T_SearchRecord
    /// </summary>
    public class SearchRecordTypeConfig : EntityTypeConfiguration<SearchRecord>
    {
        public SearchRecordTypeConfig()
        {
            ToTable("T_SearchRecord");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("SearchRecord_ID");
            Property(x => x.OpenId).HasColumnName("SearchRecord_OpenId");
            Property(x => x.Description).HasColumnName("SearchRecord_Description");
            Property(x => x.Distance).HasColumnName("SearchRecord_Distance");
            Property(x => x.Class).HasColumnName("SearchRecord_Class");
            Property(x => x.Schedule).HasColumnName("SearchRecord_Schedule");
            Property(x => x.PayMethod).HasColumnName("SearchRecord_PayMethod");
            Property(x => x.Keyword).HasColumnName("SearchRecord_Keyword");
            Property(x => x.CreateTime).HasColumnName("SearchRecord_CreateTime");
        }
    }
}

