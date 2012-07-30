using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using EventLibrary.Entities;
using Newtonsoft.Json;
using System.Configuration;

namespace EventLibrary.ServiceBrokers
{
    public class JanrainBroker
    {
        /// <summary>
        /// RPX Api Key
        /// </summary>
        protected string apiKey = "";

        /// <summary>
        /// Creates a new RPX Login instance
        /// </summary>
        /// <param name="apiKey">RPX Api Key</param>
        public JanrainBroker()
        {
            this.apiKey = ConfigurationManager.AppSettings["janrain_api_key"];
        }

        /// <summary>
        /// Get user profile
        /// </summary>
        /// <param name="token">Token from RPX</param>
        /// <returns>User profile</returns>
        public JanrainUserProfile GetProfile(string token)
        {
            // Fetch authentication info from RPX
            Uri url = new Uri(ConfigurationManager.AppSettings["janrain_api_url"]);
            string data = string.Format(@"apiKey={0}&token={1}", apiKey,token);

            // Auth_info request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
            requestWriter.Write(data);
            requestWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            TextReader responseReader = new StreamReader(response.GetResponseStream());
            string responseString = responseReader.ReadToEnd();
            responseReader.Close();

            // De-serialize JSON
            var authInfo = JsonConvert.DeserializeObject<JanrainRequestData>(responseString);

            // Ok?
            if (authInfo.Stat != "ok")
            {
                //Need to do something that makes more sense
                throw new ApplicationException("Login failed");
            }

            return authInfo.Profile;
        }
    }
}
