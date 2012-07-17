using Nancy;
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using EventLibrary.ServiceBrokers;

namespace EventWebService
{
    /// <summary>
    /// The Person REST Module
    /// </summary>
    public class PersonModule : NancyModule
    {
        public PersonModule() :base("/Person")
        {
            Get["/"] = x =>
            {
                return Response.AsJson(new PersonSvcBroker().GetPersons());
            };
        }
    }
}