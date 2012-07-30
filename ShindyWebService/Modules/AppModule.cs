using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace EventWebService
{
    public class AppModule : NancyModule
    {
        public AppModule()
        {
            Get["/"] = x =>
            {
                return View["Index"]; 
            };
        }
    }
}