using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventLibrary.Entities;
using EventLibrary.Interfaces;
using Raven.Client;
using Raven.Client.Document;
using Raven.Abstractions.Data;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EventTest")]

namespace EventLibrary.ServiceBrokers
{
    public class EventsSvcBroker : EventLibrary.IEventsSvcBroker
    {
        private IRavenSessionProvider SessionProvider;
        internal int defaultPageSize = 10;

        public EventsSvcBroker(IRavenSessionProvider sessionProvider)
        {
            SessionProvider = sessionProvider;
        }

        public int VerifyPositiveInt(int value, int defaultValue = 1)
        {
            if (value <= 0) { value = defaultValue; }
            return value;
        }

        public IEnumerable<Event> GetUpcomingEvents(int pageSize, int pageNumber) 
        {
            pageSize = VerifyPositiveInt(pageSize, defaultPageSize);
            pageNumber = VerifyPositiveInt(pageNumber);

            IEnumerable<Event> results = null;
            using (var session = SessionProvider.OpenSession())
            {
                results = session.Query<Event>()
                    .Where(e => e.EventDateTime >= DateTime.Now)
                    .OrderBy(e => e.EventDateTime)
                    .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                    .ToList();
            }
            return results;
        }

        public IEnumerable<Event> GetUpcomingEvents(string groupName, int pageSize, int pageNumber, bool externalEventsOnly = false)
        {
            pageSize = VerifyPositiveInt(pageSize, defaultPageSize);
            pageNumber = VerifyPositiveInt(pageNumber);

            IEnumerable<Event> results = null;
            if (externalEventsOnly)
            {
                results = GetUpcomingEventsExternalGroup(groupName, pageSize, pageNumber);
            }
            else
            {
                results = GetUpcomingEventsGroup(groupName, pageSize, pageNumber);
            }

            return results;
        }

        internal IEnumerable<Event> GetUpcomingEventsGroup(string groupName, int pageSize, int pageNumber)
        {
            IEnumerable<Event> results = null;
            using (var session = SessionProvider.OpenSession())
            {
                results = session.Query<Event>()
                    .Where(e => e.HostedGroups.Any(hg => hg.Name == groupName) && e.EventDateTime >= DateTime.Now)
                    .OrderBy(e => e.EventDateTime)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }
            return results;
        }

        internal IEnumerable<Event> GetUpcomingEventsExternalGroup(string groupName, int pageSize, int pageNumber)
        {
            IEnumerable<Event> results = null;
            using (var session = SessionProvider.OpenSession())
            {
                results = session.Query<Event>()
                    .Where(e => e.HostedGroups.Any(hg => hg.Name != groupName) && e.EventDateTime >= DateTime.Now)
                    .OrderBy(e => e.EventDateTime)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }
            return results;
        }

        public Event GetEventById(int eventId)
        {
            using (var session = SessionProvider.OpenSession())
            {
                return session.Load<Event>(string.Format("events/{0}", eventId));
            }
        }

        public IEnumerable<Event> GetEvents(int pageSize, int pageNumber)
        {
            pageSize = VerifyPositiveInt(pageSize, defaultPageSize);
            pageNumber = VerifyPositiveInt(pageNumber);

            IEnumerable<Event> results = null;
            using (var session = SessionProvider.OpenSession())
            {
                results = session.Query<Event>()
                   .OrderBy(e => e.EventDateTime)
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize).ToList();
            }
            return results;
        }

        public IEnumerable<Event> GetEvents(string groupName, int pageSize, int pageNumber, bool externalEventsOnly = false)
        {
            pageSize = VerifyPositiveInt(pageSize, defaultPageSize);
            pageNumber = VerifyPositiveInt(pageNumber);

            IEnumerable<Event> results = null;
            if (externalEventsOnly)
            {
                results = GetEventsExternalGroup(groupName, pageSize, pageNumber);
            }
            else
            {
                results = GetEventsGroup(groupName, pageSize, pageNumber);
            }
            return results;
        }
        
        internal IEnumerable<Event> GetEventsGroup(string groupName, int pageSize, int pageNumber)
        {
            IEnumerable<Event> results = null;
            using (var session = SessionProvider.OpenSession())
            {
                results = session.Query<Event>()
                    .Where(e => e.HostedGroups.Any(hg => hg.Name == groupName))
                    .OrderBy(e => e.EventDateTime)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }
            return results;
        }

        internal IEnumerable<Event> GetEventsExternalGroup(string groupName, int pageSize, int pageNumber)
        {
            IEnumerable<Event> results = null;
            using (var session = SessionProvider.OpenSession())
            {
                results = session.Query<Event>()
                    .Where(e => e.HostedGroups.Any(hg => hg.Name != groupName))
                    .OrderBy(e => e.EventDateTime)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }
            return results;
        }

        public IEnumerable<Event> GetPreviousEvents(int pageSize, int pageNumber)
        {
            pageSize = VerifyPositiveInt(pageSize, defaultPageSize);
            pageNumber = VerifyPositiveInt(pageNumber);

            IEnumerable<Event> results = null;
            using (var session = SessionProvider.OpenSession())
            {
                results = session.Query<Event>()
                    .Where(e => e.EventDateTime < DateTime.Now)
                    .OrderByDescending(e => e.EventDateTime)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            return results;
        }

        public IEnumerable<Event> GetPreviousEvents(string groupName, int pageSize, int pageNumber, bool externalEventsOnly = false)
        {
            pageSize = VerifyPositiveInt(pageSize, defaultPageSize);
            pageNumber = VerifyPositiveInt(pageNumber);

            IEnumerable<Event> results = null;
            if (externalEventsOnly)
            {
                results = GetPreviousEventsExternalGroup(groupName, pageSize, pageNumber);
            }
            else
            {
                results = GetPreviousEventsGroup(groupName, pageSize, pageNumber);
            }
            return results;
        }

        private IEnumerable<Event> GetPreviousEventsGroup(string groupName, int pageSize, int pageNumber)
        {
            IEnumerable<Event> results = null;
            using (var session = SessionProvider.OpenSession())
            {
                results = session.Query<Event>()
                    .Where(e => e.HostedGroups.Any(hg => hg.Name == groupName) && e.EventDateTime < DateTime.Now)
                    .OrderByDescending(e => e.EventDateTime)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }
            return results;
        }

        private IEnumerable<Event> GetPreviousEventsExternalGroup(string groupName, int pageSize, int pageNumber)
        {
            IEnumerable<Event> results = null;
            using (var session = SessionProvider.OpenSession())
            {
                results = session.Query<Event>()
                    .Where(e => e.HostedGroups.Any(hg => hg.Name != groupName) && e.EventDateTime < DateTime.Now)
                    .OrderByDescending(e => e.EventDateTime)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }
            return results;
        }
    }
}
