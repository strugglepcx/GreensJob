﻿using System.Web;
using System.Web.Mvc;

namespace Glz.GreensJob.Logs.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
