using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventLibrary.ServiceBrokers;
using Raven.Client.Embedded;
using Raven.Client;
using EventLibrary.Entities;
using Xunit;

namespace EventTest
{
    public class EventSvcBrokerTest
    {
        private StubRavenSessionProvider ravenSession;
        private EventsSvcBroker eventBroker;

        public EventSvcBrokerTest()
        {
            ravenSession = new StubRavenSessionProvider();
            eventBroker = new EventsSvcBroker(ravenSession);
            ravenSession.LoadRavenSeedData();
        }

        // Test naming pattern: [MethodName]_[StateUnderTest]_[ExpectedBehavior].

        [Fact]
        public void VerifyPositiveInt_PositiveNumber_ReturnOne()
        {
            Assert.Equal(1, eventBroker.VerifyPositiveInt(1));
        }

        [Fact]
        public void VerifyPositiveInt_Zero_ReturnOne()
        {
            Assert.Equal(1, eventBroker.VerifyPositiveInt(0));
        }

        [Fact]
        public void VerifyPositiveInt_NegativeNumber_ReturnOne()
        {
            Assert.Equal(1, eventBroker.VerifyPositiveInt(-1));
        }

        [Fact]
        public void GetUpcomingEvents_AllUpcomingEvents_ReturnsEvent()
        {
            int eventCount = 0;
            using (var session = ravenSession.OpenSession())
            {
                var ev = session.Query<Event>()
                    .Where(e => e.EventDateTime > DateTime.Now)
                    .ToList();
                eventCount = ev.Count();
            }
            var events = eventBroker.GetUpcomingEvents(eventCount + 1, 1);
            Assert.Equal(eventCount, events.Count());
        }

        [Fact]
        public void GetUpcomingEvents_PositivePageCount_OneEvent()
        {
            var events = eventBroker.GetUpcomingEvents(1, 1);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetUpcomingEvents_PostitivePageNumber_OneEvent()
        {
            var events = eventBroker.GetUpcomingEvents(1, 2);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetUpcomingEvents_NegativePageNumber_OneEvent()
        {
            var events = eventBroker.GetUpcomingEvents(1, -1);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetUpcomingEvents_NegativePageSize_OneEvent()
        {
            var events = eventBroker.GetUpcomingEvents(-1, 1);
            Assert.Equal(1, events.Count());
        }
    }
}
