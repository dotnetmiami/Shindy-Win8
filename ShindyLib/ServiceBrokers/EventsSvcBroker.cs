﻿using System;
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

        public IEnumerable<Event> GetUpcomingEvents(int pageSize, int pageNumber)
        {
            IEnumerable<Event> results = null;
            using (var session = SessionProvider.OpenSession())
            {
                results = session.Query<Event>()
                    .Where(e => e.EventDateTime > DateTime.Now)
                    .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                    .ToList();
            }
            return results;
        }

        public IEnumerable<Event> GetUpcomingEvents(string groupName, int pageSize, int pageNumber)
        {
            IEnumerable<Event> results = null;
            using (var session = SessionProvider.OpenSession())
            {
                results = session.Query<Event>()
                    .Where(e => e.HostedGroups.Any(hg =>hg.Name.Equals(groupName)) && e.EventDateTime > DateTime.Now)
                    .OrderBy(e => e.EventDateTime)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }
            return results;
        }

        public IEnumerable<Event> GetEvents(int pageSize, int pageNumber)
        {
            IEnumerable<Event> results = null;
            using (var session = SessionProvider.OpenSession())
            {
                results = session.Query<Event>()                    
                    .OrderBy(e => e.EventDateTime)
                    .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            return results;
        }

        public IEnumerable<Event> GetEvents(string groupName, int pageSize, int pageNumber)
        {
            IEnumerable<Event> results = null;
            using (var session = SessionProvider.OpenSession())
            {
                results = session.Query<Event>()
                    .Where(e => e.HostedGroups.Any(hg => hg.Name.Equals(groupName)))
                    .OrderBy(e => e.EventDateTime)
                    .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            return results;
        }

        public IEnumerable<Event> GetPreviousEvents(int pageSize, int pageNumber)
        {
            IEnumerable<Event> results = null;
            using (var session = SessionProvider.OpenSession())
            {
                results = session.Query<Event>()
                    .Where(e => e.EventDateTime < DateTime.Now)
                    .OrderByDescending(e => e.EventDateTime)
                    .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                    .ToList();
            }
            return results;
        }

        public IEnumerable<Event> GetPreviousEvents(string groupName, int pageSize, int pageNumber)
        {
            IEnumerable<Event> results = null;
            using (var session = SessionProvider.OpenSession())
            {
                results = session.Query<Event>()
                    .Where(e => e.HostedGroups.Any(hg => hg.Name.Equals(groupName)) && e.EventDateTime < DateTime.Now)                    
                    .OrderByDescending(e => e.EventDateTime)                    
                    .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            return results;
        }
    }
}