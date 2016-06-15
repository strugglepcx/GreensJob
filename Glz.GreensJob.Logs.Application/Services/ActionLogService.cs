using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks.Repositories;
using Apworks.Specifications;
using Apworks.Storage;
using AutoMapper;
using Glz.GreensJob.Logs.Domain.Models;
using Glz.GreensJob.Logs.Dto;
using Glz.GreensJob.Logs.IApplication;
using Glz.Infrastructure;

namespace Glz.GreensJob.Logs.Application.Services
{
    public class ActionLogService : ApplicationService, IActionLogService
    {
        private readonly IRepository<int, ActionLog> _actionLogRepository;
        private readonly IRepository<int, ActionExceptionLog> _actionExceptionRepository;

        public ActionLogService(IRepositoryContext context, IRepository<int, ActionLog> actionLogRepository,
            IRepository<int, ActionExceptionLog> actionExceptionRepository) : base(context)
        {
            _actionLogRepository = actionLogRepository;
            _actionExceptionRepository = actionExceptionRepository;
        }

        public void LogAction(ActionLogModel actionLogModel)
        {
            if (actionLogModel == null) throw new ArgumentNullException(nameof(actionLogModel));
            var actionLog = Mapper.Instance.Map<ActionLog>(actionLogModel);
            actionLog.CreateTime = DateTime.Now;
            _actionLogRepository.Add(actionLog);
            Context.Commit();
        }

        public PagedResultModel<ActionLogModel> GetActionPageList(string actionName, int pageIndex, int pageSize)
        {
            return
                Mapper.Instance.Map<PagedResultModel<ActionLogModel>>(
                    _actionLogRepository.FindAll(Specification<ActionLog>.Eval(log => log.ActionName == actionName), log => log.CreateTime,
                        SortOrder.Descending, pageIndex, pageSize));
        }

        public void LogException(ActionExceptionLogModel actionExceptionLogModel)
        {
            if (actionExceptionLogModel == null) throw new ArgumentNullException(nameof(actionExceptionLogModel));
            var actionExceptionLog = Mapper.Instance.Map<ActionExceptionLog>(actionExceptionLogModel);
            actionExceptionLog.CreateTime = DateTime.Now;
            _actionExceptionRepository.Add(actionExceptionLog);
            Context.Commit();
        }

        public PagedResultModel<ActionExceptionLogModel> GetActionExceptionPageList(string actionName, int pageIndex, int pageSize)
        {
            return
                Mapper.Instance.Map<PagedResultModel<ActionExceptionLogModel>>(
                    _actionExceptionRepository.FindAll(Specification<ActionExceptionLog>.Eval(log => log.ActionName == actionName), log => log.CreateTime,
                        SortOrder.Descending, pageIndex, pageSize));
        }
    }
}
