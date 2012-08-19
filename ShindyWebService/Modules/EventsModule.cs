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
        internal int defaultPageSize = 10;
        internal int defaultPageNumber = 1;

        public EventsModule(EventsSvcBroker eventBroker)
            : base("/events")
        {

            Get[""] = parameters =>
            {
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
            ///  Return top x events.
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
            ///  Return upcoming events for a group
            /// </summary>
            Get["/{groupName}/upcoming"] = parameters =>
            {
                int pageNumber = GetPageNumber();
                int pageSize = GetPageSize();
                IEnumerable<Event> results = eventBroker.GetUpcomingEvents(parameters.groupName, pageSize, pageNumber, false);
                if (results.Any())
                {
                    return Response.AsJson(results);
                }
                return new Response() { StatusCode = HttpStatusCode.NotFound };
            };

            /// <summary>            
            ///  Return upcoming events excluding a group
            /// </summary>
            Get["/{groupName}/upcoming/external"] = parameters =>
            {
                int pageNumber = GetPageNumber();
                int pageSize = GetPageSize();
                IEnumerable<Event> results = eventBroker.GetUpcomingEvents(parameters.groupName, pageSize, pageNumber, true);
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
                IEnumerable<Event> results = eventBroker.GetEvents(parameters.groupName, pageSize, pageNumber, false);
                if (results.Any())
                {
                    return Response.AsJson(results);
                }
                return new Response() { StatusCode = HttpStatusCode.NotFound };
            };

            /// <summary>            
            ///  Return events excluding a group.
            /// </summary>
            Get["/{groupName}/external"] = parameters =>
            {
                int pageNumber = GetPageNumber();
                int pageSize = GetPageSize();
                IEnumerable<Event> results = eventBroker.GetEvents(parameters.groupName, pageSize, pageNumber, true);
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
            ///  Return previous events for a group
            /// </summary>
            Get["/{groupName}/previous"] = parameters =>
            {
                int pageNumber = GetPageNumber();
                int pageSize = GetPageSize();
                IEnumerable<Event> results = eventBroker.GetPreviousEvents(parameters.groupName, pageSize, pageNumber, false);
                if (results.Any())
                {
                    return Response.AsJson(results);
                }
                return new Response() { StatusCode = HttpStatusCode.NotFound };
            };

            /// <summary>            
            ///  Return previous events excluding the defined group events.
            /// </summary>
            Get["/{groupName}/previous/external"] = parameters =>
            {
                int pageNumber = GetPageNumber();
                int pageSize = GetPageSize();
                IEnumerable<Event> results = eventBroker.GetPreviousEvents(parameters.groupName, pageSize, pageNumber, true);
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
            if (this.Request.Query["pagesize"] != null)
            {
                int.TryParse(this.Request.Query["pagesize"], out pageSize);
            }
            else
            {
                int.TryParse(ConfigurationManager.AppSettings["default_page_size"], out pageSize);
            }
            return (pageSize == 0) ? defaultPageSize : pageSize;
        }

        private int GetPageNumber()
        {
            int pageNumber = 0;
            if (this.Request.Query["pagenumber"] != null)
            {
                int.TryParse(this.Request.Query["pagenumber"], out pageNumber);
            }
            return (pageNumber == 0) ? defaultPageNumber : pageNumber;
        }

    }
}