using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using Glz.GreensJob.Domain.IApplication;

namespace Glz.GreensJob.WebApi.Controllers
{
    [RoutePrefix("api/jobcategory")]
    public class JobCategoryController : ApiController
    {
        private readonly IJobCategoryService _service;

        public JobCategoryController(IJobCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("v1/getalllist")]
        public IHttpActionResult GetAllList()
        {
            return Ok(_service.GetObjectByPaged(1));
        }
    }
}