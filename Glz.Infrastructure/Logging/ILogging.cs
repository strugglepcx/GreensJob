using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.Infrastructure.Logging.Models;

namespace Glz.Infrastructure.Logging
{
    public interface ILogging
    {
        void LogAction(ActionLogModel actionLogModel);
        void LogException(ActionExceptionLogModel actionExceptionLogModel);
    }
}
