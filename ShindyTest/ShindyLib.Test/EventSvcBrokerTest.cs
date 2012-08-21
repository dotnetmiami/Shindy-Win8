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
            eventBroker.defaultPageSize = 1;
            ravenSession.LoadRavenSeedData();
        }

        // Test naming pattern: [MethodName]_[StateUnderTest]_[ExpectedBehavior].

        #region VerifyPositiveInt Tests

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
        #endregion

        #region GetUpcomingEvents Tests

        [Fact]
        public void GetUpcomingEvents_AllUpcomingEvents_ReturnsEvent()
        {
            int eventCount = 0;
            using (var session = ravenSession.OpenSession())
            {
                var ev = session.Query<Event>()
                    .Where(e => e.EventDateTime >= DateTime.Now)
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

        #endregion

        #region GetUpcomingEvents_GroupName Tests

        [Fact]
        public void GetUpcomingEvents_GroupNameAllEvents_ReturnAllEvents()
        {
            string groupName = "TestGroup1";
            int eventCount = 0;
            using (var session = ravenSession.OpenSession())
            {
                var ev = session.Query<Event>()
                    .Where(e => e.HostedGroups.Any(hg => hg.Name == groupName) && e.EventDateTime >= DateTime.Now)
                    .ToList();
                eventCount = ev.Count();
            }
            var events = eventBroker.GetUpcomingEvents(groupName, eventCount + 1, 1, false);
            Assert.Equal(eventCount, events.Count());
        }

        [Fact]
        public void GetUpcomingEvents_GroupNameAllExternalEvents_ReturnAllEvents()
        {
            string groupName = "TestGroup1";
            int eventCount = 0;
            using (var session = ravenSession.OpenSession())
            {
                var ev = session.Query<Event>()
                    .Where(e => e.HostedGroups.Any(hg => hg.Name != groupName) && e.EventDateTime >= DateTime.Now)
                    .ToList();
                eventCount = ev.Count();
            }
            var events = eventBroker.GetUpcomingEvents(groupName, eventCount + 1, 1, true);
            Assert.Equal(eventCount, events.Count());
        }

        [Fact]
        public void GetUpcomingEvents_ValidGroupName_OneEvent()
        {
            var events = eventBroker.GetUpcomingEvents("TestGroup1", 1, 1);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetUpcomingEvents_InvalidGroupName_OneEvent()
        {
            var events = eventBroker.GetUpcomingEvents("", 1, 1);
            Assert.Equal(0, events.Count());
        }

        [Fact]
        public void GetUpcomingEvents_GroupNamePostitivePageNumber_OneEvent()
        {
            var events = eventBroker.GetUpcomingEvents("TestGroup1", 1, 2);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetUpcomingEvents_GroupNameNegativePageNumber_OneEvent()
        {
            var events = eventBroker.GetUpcomingEvents("TestGroup1", 1, -1);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetUpcomingEvents_GroupNameNegativePageSize_OneEvent()
        {
            var events = eventBroker.GetUpcomingEvents("TestGroup1", -1, 1);
            Assert.Equal(1, events.Count());
        }

        #endregion

        #region GetEventId Tests

        [Fact]
        public void GetEventById_ValidEventId_OneEvent()
        {
            var events = eventBroker.GetEventById(1);
            Assert.NotNull(events);
        }

        [Fact]
        public void GetEventById_NegativeEventId_OneEvent()
        {
            var events = eventBroker.GetEventById(-1);
            Assert.Null(events);
        }

        [Fact]
        public void GetEventById_InvalidEventId_OneEvent()
        {
            var events = eventBroker.GetEventById(1000);
            Assert.Null(events);
        }

        #endregion

        #region GetEvents Tests

        [Fact]
        public void GetEvents_AllEvents_ReturnAllEvents()
        {
            int eventCount = 0;
            using (var session = ravenSession.OpenSession())
            {
                var ev = session.Query<Event>()
                    .ToList();
                eventCount = ev.Count();
            }
            var events = eventBroker.GetEvents(eventCount + 1, 1);
            Assert.Equal(eventCount, events.Count());
        }

        [Fact]
        public void GetEvents_PositivePageCount_OneEvent()
        {
            var events = eventBroker.GetEvents(1, 1);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetEvents_PostitivePageNumber_OneEvent()
        {
            var events = eventBroker.GetEvents(1, 2);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetEvents_NegativePageNumber_OneEvent()
        {
            var events = eventBroker.GetEvents(1, -1);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetEvents_NegativePageSize_OneEvent()
        {
            var events = eventBroker.GetEvents(-1, 1);
            Assert.Equal(1, events.Count());
        }

        #endregion

        #region GetEvents_GroupName Tests

        [Fact]
        public void GetEvents_GroupNameAllEvents_ReturnAllEvents()
        {
            string groupName = "TestGroup1";
            int eventCount = 0;
            using (var session = ravenSession.OpenSession())
            {
                var ev = session.Query<Event>()
                    .Where(e => e.HostedGroups.Any(hg => hg.Name == groupName))
                    .ToList();
                eventCount = ev.Count();
            }
            var events = eventBroker.GetEvents(groupName, eventCount + 1, 1, false);
            Assert.Equal(eventCount, events.Count());
        }

        [Fact]
        public void GetEvents_GroupNameAllExternalEvents_ReturnAllEvents()
        {
            string groupName = "TestGroup1";
            int eventCount = 0;
            using (var session = ravenSession.OpenSession())
            {
                var ev = session.Query<Event>()
                    .Where(e => e.HostedGroups.Any(hg => hg.Name != groupName))
                    .ToList();
                eventCount = ev.Count();
            }
            var events = eventBroker.GetEvents(groupName, eventCount + 1, 1, true);
            Assert.Equal(eventCount, events.Count());
        }

        [Fact]
        public void GetEvents_ValidGroupName_OneEvent()
        {
            var events = eventBroker.GetEvents("TestGroup1", 1, 1);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetEvents_InvalidGroupName_OneEvent()
        {
            var events = eventBroker.GetEvents("", 1, 1);
            Assert.Equal(0, events.Count());
        }

        [Fact]
        public void GetEvents_GroupNamePostitivePageNumber_OneEvent()
        {
            var events = eventBroker.GetEvents("TestGroup1", 1, 2);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetEvents_GroupNameNegativePageNumber_OneEvent()
        {
            var events = eventBroker.GetEvents("TestGroup1", 1, -1);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetEvents_GroupNameNegativePageSize_OneEvent()
        {
            var events = eventBroker.GetEvents("TestGroup1", -1, 1);
            Assert.Equal(1, events.Count());
        }

        #endregion

        #region GetPreviousEvents Tests

        [Fact]
        public void GetPreviousEvents_AllUpcomingEvents_ReturnsEvent()
        {
            int eventCount = 0;
            using (var session = ravenSession.OpenSession())
            {
                var ev = session.Query<Event>()
                    .Where(e => e.EventDateTime < DateTime.Now)
                    .ToList();
                eventCount = ev.Count();
            }
            var events = eventBroker.GetPreviousEvents(eventCount + 1, 1);
            Assert.Equal(eventCount, events.Count());
        }

        [Fact]
        public void GetPreviousEventss_PositivePageCount_OneEvent()
        {
            var events = eventBroker.GetPreviousEvents(1, 1);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetPreviousEvents_PostitivePageNumber_OneEvent()
        {
            var events = eventBroker.GetPreviousEvents(1, 2);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetPreviousEvents_NegativePageNumber_OneEvent()
        {
            var events = eventBroker.GetUpcomingEvents(1, -1);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetPreviousEvents_NegativePageSize_OneEvent()
        {
            var events = eventBroker.GetPreviousEvents(-1, 1);
            Assert.Equal(1, events.Count());
        }

        #endregion

        #region GetPreviousEvents_GroupName Tests

        [Fact]
        public void GetPreviousEvents_GroupNameAllEvents_ReturnAllEvents()
        {
            string groupName = "TestGroup1";
            int eventCount = 0;
            using (var session = ravenSession.OpenSession())
            {
                var ev = session.Query<Event>()
                    .Where(e => e.HostedGroups.Any(hg => hg.Name == groupName) && e.EventDateTime < DateTime.Now)
                    .ToList();
                eventCount = ev.Count();
            }
            var events = eventBroker.GetPreviousEvents(groupName, eventCount + 1, 1, false);
            Assert.Equal(eventCount, events.Count());
        }

        [Fact]
        public void GetPreviousEvents_GroupNameAllExternalEvents_ReturnAllEvents()
        {
            string groupName = "TestGroup1";
            int eventCount = 0;
            using (var session = ravenSession.OpenSession())
            {
                var ev = session.Query<Event>()
                    .Where(e => e.HostedGroups.Any(hg => hg.Name != groupName) && e.EventDateTime < DateTime.Now)
                    .ToList();
                eventCount = ev.Count();
            }
            var events = eventBroker.GetPreviousEvents(groupName, eventCount + 1, 1, true);
            Assert.Equal(eventCount, events.Count());
        }

        [Fact]
        public void GetPreviousEvents_ValidGroupName_OneEvent()
        {
            var events = eventBroker.GetPreviousEvents("TestGroup1", 1, 1);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetPreviousEvents_InvalidGroupName_OneEvent()
        {
            var events = eventBroker.GetPreviousEvents("", 1, 1);
            Assert.Equal(0, events.Count());
        }

        [Fact]
        public void GetPreviousEvents_GroupNamePostitivePageNumber_OneEvent()
        {
            var events = eventBroker.GetPreviousEvents("TestGroup1", 1, 2);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetPreviousEvents_GroupNameNegativePageNumber_OneEvent()
        {
            var events = eventBroker.GetPreviousEvents("TestGroup1", 1, -1);
            Assert.Equal(1, events.Count());
        }

        [Fact]
        public void GetPreviousEvents_GroupNameNegativePageSize_OneEvent()
        {
            var events = eventBroker.GetPreviousEvents("TestGroup1", -1, 1);
            Assert.Equal(1, events.Count());
        }

        #endregion

    }
}
