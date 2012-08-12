using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventLibrary.ServiceBrokers;
using Raven.Client.Embedded;
using Raven.Client;
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

        [Fact]
        public void GetUpcomingEvents_AllUpcomingEvents_ReturnsEvent()
        {
            var events = eventBroker.GetUpcomingEvents(1, 1);
            Assert.NotEqual(ravenSession.NumEvents, events.Count());
        }
    }
}
