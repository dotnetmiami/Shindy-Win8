using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Embedded;
using Raven.Client;
using EventLibrary.Interfaces;
using EventLibrary.Entities;

namespace EventTest
{
    public class StubRavenSessionProvider : IRavenSessionProvider
    {
        public EmbeddableDocumentStore Store { get; set; }

        public int NumEvents
        {
            get
            {
                using (var session = OpenSession())
                {
                    var events = session.Load<Event>();
                    return events.Count();
                }
            }
        }

        public StubRavenSessionProvider()
        {
            Store = new EmbeddableDocumentStore { RunInMemory = true };
            Store.Initialize();
        }

        public virtual IDocumentSession OpenSession()
        {
            return Store.OpenSession();
        }

        public void LoadRavenSeedData()
        {
            var group = new Group()
            {
                Name = "Test Group",
                Description = "Test Group Description",
                IsExternalGroup = false
            };

            var location = new Location()
            {
                Name = "Test Location",
                Address = new Address() { Street = "777 Main Street", City = "Magic City", State = "FL", ZipCode = "33133" }
            };

            var speaker1 = new Person() { FirstName = "Testy", LastName = "Speaker", Bio = "Testy Speaker's Bio" };

            var session1 = new Session()
            {
                SessionType = "main",
                Title = "Test Session One",
                Abstract = "Test Session One Abstract",
            };
            session1.Speakers.Add(speaker1);

            var sponsor1 = new Sponsor() { Name = "Testy Sponsor" };

            var event1 = new Event()
            {
                Title = "Test Event One",
                Description = "Test Event One Description",
                EventDateTime = DateTime.Today.AddDays(14),
                IsActive = true,
                EventLocation = location,
            };
            event1.HostedGroups.Add(group);
            event1.Sessions.Add(session1);
            event1.Sponsors.Add(sponsor1);

            using (var session = OpenSession())
            {
                session.Store(group);
                session.Store(location);
                session.Store(speaker1);
                session.Store(sponsor1);
                session.Store(event1);
                session.SaveChanges();

            }


        }

    }
}
