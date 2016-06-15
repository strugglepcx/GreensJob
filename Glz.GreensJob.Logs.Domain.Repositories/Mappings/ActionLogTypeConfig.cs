using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Logs.Domain.Models;

namespace Glz.GreensJob.Logs.Domain.Repositories.Mappings
{
    /// <summary>
    /// ActionLog
    /// </summary>
    public class ActionLogTypeConfig : EntityTypeConfiguration<ActionLog>
    {
        public ActionLogTypeConfig()
        {
            ToTable("ActionLog");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID");
            Property(x => x.ControllerName).HasColumnName("ControllerName");
            Property(x => x.ActionName).HasColumnName("ActionName");
            Property(x => x.ContentType).HasColumnName("ContentType");
            Property(x => x.RequestUrl).HasColumnName("RequestUrl");
            Property(x => x.RequestContent).HasColumnName("RequestContent");
            Property(x => x.ResultCode).HasColumnName("ResultCode");
            Property(x => x.ResultContent).HasColumnName("ResultContent");
            Property(x => x.Duration).HasColumnName("Duration");
            Property(x => x.ExecuteTime).HasColumnName("ExecuteTime");
            Property(x => x.CreateTime).HasColumnName("CreateTime");
        }
    }
}

