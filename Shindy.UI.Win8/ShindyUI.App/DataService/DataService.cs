namespace ShindyUI.App
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using ShindyUI.App.Model;
    using ShindyUI.App.Model.ViewModel;

    public static class DataService
    {
        public static string ServiceURL = Configuration.Configuration.ServiceUrl + "/events";

        public async static Task<IEnumerable<Event>> GetEvents()
        {
             using (var crawler = new HttpClient())
            {
                var json_data = string.Empty;
                try
                {
                    var content =  await crawler.GetStringAsync(ServiceURL);
                    var results = JsonConvert.DeserializeObject<Event[]>(content);
                    return results;
                }
                catch (Exception)
                {
                    // TODO: Why are we throwing a new exception?
                    throw new Exception(string.Format("An error occured communicating with {0}"));
                }
            }
        } 
        
        public async static Task<IEnumerable<Event>> GetSession()
        {
             using (var crawler = new HttpClient())
            {
                var json_data = string.Empty;
                try
                {
                    var content =  await crawler.GetStringAsync(ServiceURL);
                    var results = JsonConvert.DeserializeObject<Event[]>(content);
                    return results;
                }
                catch (Exception)
                {
                    // TODO: Why are we throwing a new exception?
                    throw new Exception(string.Format("An error occured communicating with {0}"));
                }
            }
        }

    }
}
