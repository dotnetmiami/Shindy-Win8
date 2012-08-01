using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;
using CommandLine.Text;

namespace EventTestConsole
{
    class Options
    {
        [Option("r", "ravenurl", Required = false, HelpText = "The URL for RavenDB.")]
        public string RavenURL { get; set; }

        [Option("j", "jsonpath", Required = false, HelpText = "The URL or the path of the JSON file to be loaded.")]
        public string JSONPath { get; set; }

        [Option("d", "dbname", Required = false, HelpText = "The name of the Raven database.")]
        public string DBName { get; set; }

    }
}
