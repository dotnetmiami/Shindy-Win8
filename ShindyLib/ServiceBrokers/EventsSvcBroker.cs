using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventLibrary.Entities;
using EventLibrary.Interfaces;

namespace EventLibrary.ServiceBrokers
{
    public class EventsSvcBroker
    {
        private IRavenSessionProvider SessionProvider;
        
        public EventsSvcBroker(IRavenSessionProvider sessionProvider)
        {
            SessionProvider = sessionProvider;
        }
       
        public IEnumerable<Event> GetUpcomingEvents()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetUpcomingEvents(string groupName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetEventsForGroup(string groupName)
        {
            throw new NotImplementedException();
        }
    }
}
