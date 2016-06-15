using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Glz.GreensJob.WebApi.Areas.HelpPage.ModelDescriptions;
using Glz.GreensJob.WebApi.Areas.HelpPage.Models;
using Glz.Infrastructure;
using Glz.Infrastructure.Logging.Models;

namespace Glz.GreensJob.WebApi.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";
        private readonly string _host = ConfigurationManager.AppSettings["LoggingHost"];

        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (!String.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }

        public ActionResult ActionLogList(string actionName = "", int pageIndex = 1, int pageSize = 20)
        {
            using (var proxy = new HttpClient())
            {
                proxy.BaseAddress = new Uri(_host);
                var result = proxy.GetAsync($"api/logs/v1/getActionPageList?actionName={actionName}&pageIndex={pageIndex}&pageSize={pageSize}").Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var resultData = result.Content.ReadAsAsync<ResultBase<PagedResultModel<ActionLogModel>>>().Result;
                    if (resultData.code == StatusCodes.Success)
                    {
                        ViewBag.ActionName = actionName;
                        return View(resultData.Data);
                    }
                }
            }

            return View(new PagedResultModel<ActionLogModel>() { Data = new List<ActionLogModel>(), PageNumber = pageIndex, PageSize = pageSize, TotalRecords = 0, TotalPages = 0 });
        }

        public ActionResult ActionExceptionLogList(string actionName = "", int pageIndex = 1, int pageSize = 20)
        {
            using (var proxy = new HttpClient())
            {
                proxy.BaseAddress = new Uri(_host);
                var result = proxy.GetAsync($"api/logs/v1/GetActionExceptionPageList?actionName={actionName}&pageIndex={pageIndex}&pageSize={pageSize}").Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var resultData = result.Content.ReadAsAsync<ResultBase<PagedResultModel<ActionExceptionLogModel>>>().Result;
                    if (resultData.code == StatusCodes.Success)
                    {
                        ViewBag.ActionName = actionName;
                        return View(resultData.Data);
                    }
                }
            }

            return View(new PagedResultModel<ActionExceptionLogModel>() { Data = new List<ActionExceptionLogModel>(), PageNumber = pageIndex, PageSize = pageSize, TotalRecords = 0, TotalPages = 0 });
        }
    }
}