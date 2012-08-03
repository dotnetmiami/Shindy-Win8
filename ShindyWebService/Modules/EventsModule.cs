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
            /// <summary>
            ///  /events/
            ///  return top x events
            /// </summary>
            Get["/upcomming"] = parameters =>
            {
                int pageNumber = GetPageNumber();
                int pageSize = GetPageSize();
                IEnumerable<Event> results = eventBroker.GetUpcomingEvents(pageSize, pageNumber);
                return Response.AsJson(results);
            };

            /// <summary>
            ///  /events/
            ///  return top x upcoming events by group
            /// </summary>
            Get["/{groupName}/upcomming"] = parameters =>
            {
                int pageNumber = GetPageNumber();
                int pageSize = GetPageSize();
                IEnumerable<Event> results = eventBroker.GetUpcomingEvents(parameters.groupName, pageSize, pageNumber);
                return Response.AsJson(results);
            };

            /// <summary>
            ///  /events/grou
            ///  return events from group
            /// </summary>
            Get["/{groupName}"] = parameters =>
            {
                int pageNumber = GetPageNumber();
                int pageSize = GetPageSize();
                IEnumerable<Event> results = eventBroker.GetEventsForGroup(parameters.groupName, pageSize, pageNumber);
                return Response.AsJson(results);
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