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
                Name = "TestGroup1",
                Description = "Test Group One Description",
                IsExternalGroup = false
            };

            var group2 = new Group()
            {
                Name = "TestGroup2",
                Description = "Test Group Two Description",
                IsExternalGroup = true
            };

            var location = new Location()
            {
                Name = "Test Location",
                Address = new Address() { Street = "777 Main Street", City = "Magic City", State = "FL", ZipCode = "33133" }
            };

            var speaker1 = new Person() { FirstName = "Testy", LastName = "Speaker", Bio = "Testy Speaker's Bio" };

            // Event One
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

            // Event Two
            var speaker2 = new Person() { FirstName = "Testy", LastName = "SpeakerTwo", Bio = "Testy SpeakerTwo's Bio" };

            var session2 = new Session()
            {
                SessionType = "main",
                Title = "Test Session Two",
                Abstract = "Test Session Two Abstract",
            };
            session2.Speakers.Add(speaker2);

            var event2 = new Event()
            {
                Title = "Test Event Two",
                Description = "Test Event Two Description",
                EventDateTime = DateTime.Today.AddDays(-15),
                IsActive = true,
                EventLocation = location,
            };
            event2.HostedGroups.Add(group);
            event2.Sessions.Add(session2);
            event2.Sponsors.Add(sponsor1);

            using (var session = OpenSession())
            {
                session.Store(speaker2);
                session.Store(event2);
                session.SaveChanges();
            }

            // Event Three
            var speaker3 = new Person() { FirstName = "Testy", LastName = "SpeakerThree", Bio = "Testy SpeakerThree's Bio" };

            var session3 = new Session()
            {
                SessionType = "main",
                Title = "Test Session Three",
                Abstract = "Test Session Three Abstract",
            };
            session3.Speakers.Add(speaker3);

            var event3 = new Event()
            {
                Title = "Test Event Three",
                Description = "Test Event Three Description",
                EventDateTime = DateTime.Today.AddDays(-45),
                IsActive = true,
                EventLocation = location,
            };
            event3.HostedGroups.Add(group);
            event3.Sessions.Add(session3);
            event3.Sponsors.Add(sponsor1);

            using (var session = OpenSession())
            {
                session.Store(speaker3);
                session.Store(event3);
                session.SaveChanges();
            }

            // Event Four
            var session4 = new Session()
            {
                SessionType = "main",
                Title = "Test Session Four",
                Abstract = "Test Session Four Abstract",
            };
            session4.Speakers.Add(speaker1);

            var event4 = new Event()
            {
                Title = "Test Event Four",
                Description = "Test Event Four Description",
                EventDateTime = DateTime.Today.AddDays(45),
                IsActive = true,
                EventLocation = location,
            };
            event4.HostedGroups.Add(group2);
            event4.Sessions.Add(session4);
            event4.Sponsors.Add(sponsor1);

            using (var session = OpenSession())
            {
                session.Store(speaker1);
                session.Store(event4);
                session.SaveChanges();
            }

            // Event Five
            var session5 = new Session()
            {
                SessionType = "main",
                Title = "Test Session Five",
                Abstract = "Test Session Five Abstract",
            };
            session5.Speakers.Add(speaker1);

            var event5 = new Event()
            {
                Title = "Test Event Five",
                Description = "Test Event Five Description",
                EventDateTime = DateTime.Today.AddDays(60),
                IsActive = true,
                EventLocation = location,
            };
            event5.HostedGroups.Add(group);
            event5.Sessions.Add(session5);
            event5.Sponsors.Add(sponsor1);

            using (var session = OpenSession())
            {
                session.Store(speaker1);
                session.Store(event5);
                session.SaveChanges();
            }
        }

    }
}
