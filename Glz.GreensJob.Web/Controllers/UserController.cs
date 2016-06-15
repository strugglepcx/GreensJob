using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Glz.GreensJob.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Glz.GreensJob.Web.Controllers
{
    public class UserController : Controller
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: User
        public ActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        public ActionResult Logon(LoginModel model)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "123"));
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
            return Redirect(HttpUtility.HtmlDecode(model.ReturnUrl));
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Redirect(Request.ApplicationPath);
        }
    }
}