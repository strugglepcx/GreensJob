using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks;
using Apworks.Repositories;
using AutoMapper;
using Glz.GreensJob.Logs.Domain.Models;
using Glz.GreensJob.Logs.Dto;
using Glz.Infrastructure;

namespace Glz.GreensJob.Logs.Application
{
    /// <summary>
    /// 表示应用层服务的抽象类。
    /// </summary>
    public abstract class ApplicationService : DisposableObject
    {
        #region Private Fields
        private readonly IRepositoryContext _context;
        //private static readonly ILog log = LogManager.GetLogger("VvGotHome.Logger");
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个<c>ApplicationService</c>类型的实例。
        /// </summary>
        /// <param name="context">用来初始化<c>ApplicationService</c>类型的仓储上下文实例。</param>
        protected ApplicationService(IRepositoryContext context)
        {
            _context = context;
        }
        #endregion

        #region Protected Properties
        /// <summary>
        /// 获取当前应用层服务所使用的仓储上下文实例。
        /// </summary>
        protected IRepositoryContext Context
        {
            get { return _context; }
        }
        #endregion

        #region Protected Methods

        protected override void Dispose(bool disposing)
        {
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// 对应用层服务进行初始化。
        /// </summary>
        /// <remarks>包含的初始化任务有：
        /// 1. AutoMapper框架的初始化</remarks>
        public static void Initialize()
        {
            Mapper.Initialize(configuration =>
            {
                configuration.CreateMap<ActionLogModel, ActionLog>();
                configuration.CreateMap<ActionLog, ActionLogModel>();
                configuration.CreateMap<PagedResult<ActionLog>, PagedResultModel<ActionLogModel>>();

                configuration.CreateMap<ActionExceptionLogModel, ActionExceptionLog>();
                configuration.CreateMap<ActionExceptionLog, ActionExceptionLogModel>();
                configuration.CreateMap<PagedResult<ActionExceptionLog>, PagedResultModel<ActionExceptionLogModel>>();
            });
        }
        #endregion
    }
}
