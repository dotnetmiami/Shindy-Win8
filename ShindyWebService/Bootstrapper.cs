using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace EventWebService
{
    public class Bootstrapper: DefaultNancyBootstrapper
    {
        
        protected override void ApplicationStartup(TinyIoC.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
           base.ApplicationStartup(container, pipelines);
           StaticConfiguration.DisableErrorTraces = false;
        }
    }
}