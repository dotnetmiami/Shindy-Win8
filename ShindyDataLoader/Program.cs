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

namespace ShindyDataLoader
{
    public class Program
    {

        static void Main(string[] args)
        {
            var options = new Options();
            ICommandLineParser parser = new CommandLineParser();

            parser.ParseArguments(args, options);

            if (options.DBName == null) { options.DBName = ConfigurationManager.AppSettings["DBName"]; }
            if (options.RavenURL == null) { options.RavenURL = ConfigurationManager.AppSettings["RavenURL"]; }
            if (options.JsonPath == null) { options.JsonPath = ConfigurationManager.AppSettings["JSONPath"]; }
            if (options.ApiKey == null) { options.ApiKey = ConfigurationManager.AppSettings["RavenApi"]; }

            LoadEvents(options);

            Console.WriteLine("JSON file successfully loaded into {0}.", options.DBName);
        }

        public static void LoadEvents(Options options)
        {
            var events = GetJsonData<dnm>(options.JsonPath);
            using (var documentStore = new DocumentStore { Url = options.RavenURL })
            {

                if (options.ApiKey != null && options.ApiKey != "")
                {
                    documentStore.ApiKey = options.ApiKey;
                    options.DBName = null;
                }

                documentStore.Initialize();

                if (options.ApiKey == null || options.ApiKey == "")
                {
                    documentStore.DatabaseCommands.EnsureDatabaseExists(options.DBName);
                }

                List<Group> HostedGroups = new List<Group>();
                List<Person> Speakers = new List<Person>();
                List<Sponsor> Sponsors = new List<Sponsor>();
                List<Location> Locations = new List<Location>();

                foreach (Event e in events.Events)
                {
                    using (var session = documentStore.OpenSession(options.DBName))
                    {
                        if (e.EventLocation != null)
                        {
                            Location loc = session.Query<Location>()
                                                .Where(x => x.Name == e.EventLocation.Name)
                                                .FirstOrDefault();
                            if (loc == null)
                            {
                                session.Store(e.EventLocation);
                            }
                            else
                            {
                                e.EventLocation.Id = loc.Id;
                            }
                        }
                        if (e.HostedGroups != null)
                        {
                            foreach (Group hg in e.HostedGroups)
                            {
                                Group dbGroup = session.Query<Group>()
                                                    .Where(x => x.Name == hg.Name)
                                                    .FirstOrDefault();
                                if (dbGroup == null)
                                {
                                    session.Store(hg);
                                }
                                else
                                {
                                    hg.Id = dbGroup.Id;
                                }
                            }
                        }
                        if (e.Sessions != null)
                        {
                            foreach (Session sess in e.Sessions)
                            {
                                foreach (Person sp in sess.Speakers)
                                {
                                    Person dbPerson = session.Query<Person>()
                                                        .Where(i => i.FirstName == sp.FirstName && i.LastName == sp.LastName)
                                                        .FirstOrDefault();
                                    if (dbPerson == null)
                                    {
                                        Speakers.Add(sp);
                                        session.Store(sp);
                                    }
                                    else
                                    {
                                        sp.Id = dbPerson.Id;
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
                                    Sponsor dbSponsor = session.Query<Sponsor>()
                                                        .Where(i => i.Name == spon.Name)
                                                        .FirstOrDefault();
                                    if (dbSponsor == null)
                                    {
                                        Sponsors.Add(spon);
                                        session.Store(spon);
                                    }
                                    else
                                    {
                                        spon.Id = dbSponsor.Id;
                                    }
                                }
                            }
                        }
                        session.Store(e);
                        session.SaveChanges(); // will send the change to the database
                    }
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

        public enum TransportType { http, file }

        public static T GetJsonData<T>(string path) where T : new()
        {
            var jsonData = string.Empty;

            if (DetermineTransport(path) == TransportType.http)
            {
                jsonData = GetURLJsonData(path);
            }
            else
            {
                jsonData = GetFileJsonData(path);
            }
            return LoadObjectFromJson<T>(jsonData);
        }

        public static T LoadObjectFromJson<T>(string jsonData) where T : new()
        {
            // if string with JSON data is not empty, deserialize it to class and return its instance 
            return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<T>(jsonData) : new T();
        }

        public static TransportType DetermineTransport(string path)
        {
            TransportType transportType = TransportType.file;

            if (path.Substring(0, 4) == "http")
            {
                transportType = TransportType.http;
            }
            return transportType;
        }

        public static string GetURLJsonData(string url)
        {
            string urlData = string.Empty;

            using (var web = new WebClient())
            {
                // attempt to download JSON data as a string
                try
                {
                    urlData = web.DownloadString(url);

                }
                catch (Exception)
                {
                    throw;
                }

            }
            return urlData;
        }

        // Comment
        public static string GetFileJsonData(string path)
        {
            string pathData = string.Empty;

            pathData = System.IO.File.ReadAllText(path);

            return pathData;
        }
    }


}
