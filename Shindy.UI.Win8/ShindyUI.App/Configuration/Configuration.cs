using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShindyUI.App
{
    public static class Configuration
    {
        public static string ServiceURL
        {
            get { return "http://shindy-dev.apphb.com"; }
        }

        public static string HostedGroupName
        {
            get { return "dotNet Miami"; }
        }
    }
}
