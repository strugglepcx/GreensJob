using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glz.GreensJob.Logs.Dto;
using Glz.Infrastructure;

namespace Glz.GreensJob.Logs.IApplication
{
    public interface IActionLogService : IApplicationServiceContract
    {
        void LogAction(ActionLogModel actionLogModel);
        PagedResultModel<ActionLogModel> GetActionPageList(string actionName, int pageIndex, int pageSize);
        void LogException(ActionExceptionLogModel actionExceptionLogModel);
        PagedResultModel<ActionExceptionLogModel> GetActionExceptionPageList(string actionName, int pageIndex, int pageSize);
    }
}
