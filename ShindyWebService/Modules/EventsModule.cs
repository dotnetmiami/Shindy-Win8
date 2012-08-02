using System;
using System.Collections.Generic;
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
                IEnumerable<Event> results = eventBroker.GetUpcomingEvents();
                return Response.AsJson(results);
            };

            /// <summary>
            ///  /events/grou
            ///  return events from group
            /// </summary>
            Get["/{groupName}"] = parameters =>
            {
                IEnumerable<Event> results = eventBroker.GetEventsForGroup(parameters.groupName);
                return Response.AsJson(results);
            };

            /// <summary>
            ///  /events/
            ///  return top x upcoming events by group
            /// </summary>
            Get["/{groupName}/upcomming"] = parameters =>
            {
                IEnumerable<Event> results = eventBroker.GetUpcomingEvents(parameters.groupName);
                return Response.AsJson(results);
            };
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