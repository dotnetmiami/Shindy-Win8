using Nancy;
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using EventLibrary.ServiceBrokers;
using EventLibrary.Entities;
using Nancy.ModelBinding;

namespace EventWebService
{
    /// <summary>
    /// The Person REST Module
    /// </summary>
    public class PersonModule : NancyModule
    {
        public PersonModule() :base("/Person")
        {
            Post["/signin"] = x =>
            {
                var test = this.Bind<JanrainPostData>();
                var user_profile = new JanrainBroker().GetProfile(test.token);

                return this.Response.AsJson(user_profile);
            };

           
        }
    }
}