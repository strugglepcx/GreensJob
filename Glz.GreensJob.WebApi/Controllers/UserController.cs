using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Glz.GreensJob.WebApi.Models;

namespace Glz.GreensJob.WebApi.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("v1/login")]
        public string Login([FromBody] LoginModel loginModel)
        {
            return $"{loginModel.name}, {loginModel.password}";
        }

        [HttpGet]
        [Route("v1/test")]
        public string Test(string name, string password)
        {
            return $"{name}, {password}";
        }

        public OutPublisherModel GetUserInfo(int userID)
        {
            return null;

        }
    }

    public class LoginModel
    {
        public string name { get; set; }
        public string password { get; set; }
    }
}
