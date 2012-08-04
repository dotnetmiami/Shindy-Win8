using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using EventLibrary.Entities;
using EventLibrary.ServiceBrokers;
using Nancy;

namespace EventWebService.Modules
{
    public class EventsModule : NancyModule
    {
        public EventsModule(EventsSvcBroker eventBroker)
            : base("/events")
        {

            Get[""] = parameters =>
            {
                //return Response.AsJson(eventBroker.GetEventById());
                
                int pageNumber = GetPageNumber();
                int pageSize = GetPageSize();
                IEnumerable<Event> results = eventBroker.GetEvents(pageSize, pageNumber);
                if (results.Any())
                {
                    return Response.AsJson(results);
                }
                return new Response() { StatusCode = HttpStatusCode.NotFound };
            };

            /// <summary>            
            ///  return top x events
            /// </summary>
            Get["/upcoming"] = parameters =>
            {
                int pageNumber = GetPageNumber();
                int pageSize = GetPageSize();
                IEnumerable<Event> results = eventBroker.GetUpcomingEvents(pageSize, pageNumber);
                if (results.Any())
                {
                    return Response.AsJson(results);
                }
                return new Response() { StatusCode = HttpStatusCode.NotFound };
            };

            /// <summary>            
            ///  return top x upcoming events by group
            /// </summary>
            Get["/{groupName}/upcoming"] = parameters =>
            {
                int pageNumber = GetPageNumber();
                int pageSize = GetPageSize();
                IEnumerable<Event> results = eventBroker.GetUpcomingEvents(parameters.groupName, pageSize, pageNumber);
                if (results.Any())
                {
                    return Response.AsJson(results);
                }
                return new Response() { StatusCode = HttpStatusCode.NotFound };
            };

            /// <summary>            
            ///  return events from group
            /// </summary>
            Get["/{groupName}"] = parameters =>
            {
                int pageNumber = GetPageNumber();
                int pageSize = GetPageSize();
                IEnumerable<Event> results = eventBroker.GetEvents(parameters.groupName, pageSize, pageNumber);
                if (results.Any())
                {
                    return Response.AsJson(results);
                }
                return new Response() { StatusCode = HttpStatusCode.NotFound };
            };


            /// <summary>            
            ///  return events from group
            /// </summary>
            Get["/previous"] = parameters =>
            {
                int pageNumber = GetPageNumber();
                int pageSize = GetPageSize();
                IEnumerable<Event> results = eventBroker.GetPreviousEvents(pageSize, pageNumber);
                if (results.Any())
                {
                    return Response.AsJson(results);
                }
                return new Response() { StatusCode = HttpStatusCode.NotFound };
            };

            /// <summary>            
            ///  return previous events from group
            /// </summary>
            Get["/{groupName}/previous"] = parameters =>
            {
                int pageNumber = GetPageNumber();
                int pageSize = GetPageSize();
                IEnumerable<Event> results = eventBroker.GetPreviousEvents(parameters.groupName, pageSize, pageNumber);
                if (results.Any())
                {
                    return Response.AsJson(results);
                }
                return new Response() { StatusCode = HttpStatusCode.NotFound };
            };
        }

        private int GetPageSize()
        {
            int pageSize = 0;
            int.TryParse(ConfigurationManager.AppSettings["default_page_size"], out pageSize);
            return (pageSize == 0) ? 10 : pageSize;
        }

        private int GetPageNumber()
        {
            int pageNumber = 0;
            if (this.Request.Query["PageNumber"] != null)
            {
                int.TryParse(this.Request.Query["PageNumber"], out pageNumber);
            }
            return (pageNumber == 0) ? 1 : pageNumber;
        }
    }
}