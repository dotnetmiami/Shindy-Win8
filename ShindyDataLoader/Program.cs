using System;
using System.Net;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Configuration;
using EventLibrary.Entities;
using Raven.Client.Document;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Raven.Client.Extensions;
using CommandLine;
using CommandLine.Text;

namespace EventTestConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            var options = new Options();
            ICommandLineParser parser = new CommandLineParser();
            if (parser.ParseArguments(args, options))
            {
                if (options.DBName == null) { options.DBName = ConfigurationManager.AppSettings["DBName"]; }
                if (options.RavenURL == null) { options.RavenURL = ConfigurationManager.AppSettings["RavenURL"]; }
                if (options.JSONPath == null) { options.JSONPath = ConfigurationManager.AppSettings["JSONPath"]; }
            }

            LoadEvents(options);

            Console.WriteLine("JSON file successfully loaded into {0}.", options.DBName);
        }

        public static void LoadEvents(Options options)
        {
            var events = GetJSONData<dnm>(options.JSONPath);
            using (var documentStore = new DocumentStore { Url = options.RavenURL })
            {
                documentStore.Initialize();

                documentStore.DatabaseCommands.EnsureDatabaseExists(options.DBName);

                List<Group> HostedGroups = new List<Group>();
                List<Person> Speakers = new List<Person>();
                List<Sponsor> Sponsors = new List<Sponsor>();

                using (var session = documentStore.OpenSession(options.DBName))
                {
                    foreach (Event e in events.Events)
                    {
                        if (e.HostedGroups != null)
                        {
                            foreach (Group hg in e.HostedGroups)
                            {
                                if (HostedGroups.Exists(i => i.Name == hg.Name) == false)
                                {
                                    HostedGroups.Add(hg);
                                    session.Store(hg);
                                }
                            }
                        }
                        if (e.Sessions != null)
                        {
                            foreach (Session sess in e.Sessions)
                            {
                                foreach (Person sp in sess.Speakers)
                                {
                                    if (Speakers.Exists(i => i.FirstName  == sp.FirstName && i.LastName == sp.LastName) == false)
                                    {
                                        Speakers.Add(sp);
                                        session.Store(sp);
                                    }
                                }
                            }
                        }
                        if (e.Sponsors != null)
                        {
                            if (e.Sponsors != null)
                            {
                                foreach (Sponsor spon in e.Sponsors)
                                {
                                    if (Sponsors.Exists(i => i.Name == spon.Name) == false)
                                    {
                                        Sponsors.Add(spon);
                                        session.Store(spon);
                                    }
                                }
                            }
                        }
                        session.Store(e);
                    }
                    session.SaveChanges(); // will send the change to the database
                }
            }
        }

        public class EventWrapper
        {
            public Event Event { get; set; }
        }

        public class dnm
        {
            public List<Event> Events { get; set; }
        }


        // From: http://www.codeproject.com/Tips/397574/Use-Csharp-to-get-JSON-data-from-the-web-and-map-i
        private static T GetJSONData<T>(string path) where T : new()
        {
            var json_data = string.Empty;

            if (path.Substring(0, 4) == "http")
            {
                using (var w = new WebClient())
                {
                    // attempt to download JSON data as a string
                    try
                    {
                        json_data = w.DownloadString(path);

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            else
            {
                json_data = System.IO.File.ReadAllText(path);
            }

            // if string with JSON data is not empty, deserialize it to class and return its instance 
            return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
        }

    }


}
