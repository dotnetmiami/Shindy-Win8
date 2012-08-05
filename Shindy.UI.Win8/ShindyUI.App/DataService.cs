using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using ShindyUI.App.DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ShindyUI.App
{
    public static class DataService
    {
        public const string ServiceURL = "http://shindy.apphb.com/Events/?pagenumber=1";

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
                    throw new Exception(string.Format("An error occured communicating with {0}"));
                }
            }
        }

    }
}
