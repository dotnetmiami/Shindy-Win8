using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventLibrary.Entities;
using EventLibrary.Interfaces;
using Raven.Client;
using Raven.Client.Document;
using Raven.Abstractions.Data;


namespace EventLibrary.ServiceBrokers
{
    public class EventsSvcBroker
    {
        private IRavenSessionProvider SessionProvider;

        public EventsSvcBroker()
        {
            SessionProvider = new RavenSessionProvider();
        }

        public EventsSvcBroker(IRavenSessionProvider sessionProvider)
        {
            SessionProvider = sessionProvider;
        }

        public int VerifyPositiveInt(int value)
        {
            if (value <= 0) { value = 1; }
            return value;
        }

        public IEnumerable<Event> GetUpcomingEvents(int pageSize, int pageNumber)
        {
            pageSize = VerifyPositiveInt(pageSize);
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

        public IEnumerable<Event> GetUpcomingEvents(string groupName, int pageSize, int pageNumber)
        {
            pageSize = VerifyPositiveInt(pageSize);
            pageNumber = VerifyPositiveInt(pageNumber);

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

        public Event GetEventById(int eventId)
        {
            using (var session = SessionProvider.OpenSession())
            {
                return session.Load<Event>(string.Format("events/{0}", eventId));
            }
        }

        public IEnumerable<Event> GetEvents(int pageSize, int pageNumber)
        {
            pageSize = VerifyPositiveInt(pageSize);
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

        public IEnumerable<Event> GetEvents(string groupName, int pageSize, int pageNumber)
        {
            pageSize = VerifyPositiveInt(pageSize);
            pageNumber = VerifyPositiveInt(pageNumber);

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

        public IEnumerable<Event> GetPreviousEvents(int pageSize, int pageNumber)
        {
            pageSize = VerifyPositiveInt(pageSize);
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

        public IEnumerable<Event> GetPreviousEvents(string groupName, int pageSize, int pageNumber)
        {
            pageSize = VerifyPositiveInt(pageSize);
            pageNumber = VerifyPositiveInt(pageNumber);

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
    }
}
