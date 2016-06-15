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
    /// ActionExceptionLog
    /// </summary>
    public class ActionExceptionLogTypeConfig : EntityTypeConfiguration<ActionExceptionLog>
    {
        public ActionExceptionLogTypeConfig()
        {
            ToTable("ActionExceptionLog");

            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID");
            Property(x => x.ControllerName).HasColumnName("ControllerName");
            Property(x => x.ActionName).HasColumnName("ActionName");
            Property(x => x.ContentType).HasColumnName("ContentType");
            Property(x => x.RequestUrl).HasColumnName("RequestUrl");
            Property(x => x.RequestContent).HasColumnName("RequestContent");
            Property(x => x.ExceptionType).HasColumnName("ExceptionType");
            Property(x => x.ExceptionMessage).HasColumnName("ExceptionMessage");
            Property(x => x.ExceptionTraceStack).HasColumnName("ExceptionTraceStack");
            Property(x => x.CreateTime).HasColumnName("CreateTime");
        }
    }
}

