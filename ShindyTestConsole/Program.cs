using System;
using System.Net;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using EventLibrary.Entities;
using Raven.Client.Document;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace EventTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadEvents();
        }

        public static void LoadEvents()
        {

            var events = GetJSONData<dnm>("http://localhost/dotnetmiami/event.js");

            var documentStore = new DocumentStore { Url = "http://localhost:8080/" };
            documentStore.Initialize();

            using (var session = documentStore.OpenSession())
            {
                foreach(Event e in events.Events)
                session.Store(e);
                session.SaveChanges(); // will send the change to the database
            }

            //System.Diagnostics.Debug.WriteLine(events.Events.Count);
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
        private static T GetJSONData<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
 
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        } 
 
    }


}
