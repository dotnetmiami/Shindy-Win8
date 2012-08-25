using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventLibrary.ServiceBrokers;
using EventLibrary.Entities;
using EventLibrary;
using EventWebService.Modules;
using Nancy.Json;
using Nancy.Testing;
using Moq;
using Xunit;
using Newtonsoft.Json;

namespace EventTest.ShindyWebService
{
    public class EventsModuleTest
    {
        private Browser app;
        private Mock<EventLibrary.IEventsSvcBroker> eventBroker;
        private IEnumerable<Event> eventData;

        public EventsModuleTest()
        {
            eventData = GetEventData();

            eventBroker = new Mock<EventLibrary.IEventsSvcBroker>();
            eventBroker.Setup(foo => foo.GetEvents(It.IsAny<int>(), It.IsAny<int>())).Returns(eventData);
            eventBroker.Setup(foo => foo.GetEvents(100, 100)).Returns(new List<Event>());
            eventBroker.Setup(foo => foo.GetEvents(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(eventData);
            eventBroker.Setup(foo => foo.GetEvents("dotnet miami", 100, 100, It.IsAny<bool>())).Returns(new List<Event>());
            eventBroker.Setup(foo => foo.GetUpcomingEvents(It.IsAny<int>(), It.IsAny<int>())).Returns(eventData);
            eventBroker.Setup(foo => foo.GetUpcomingEvents(100, 100)).Returns(new List<Event>());
            eventBroker.Setup(foo => foo.GetUpcomingEvents(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(eventData);
            eventBroker.Setup(foo => foo.GetUpcomingEvents("dotnet miami", 100, 100, It.IsAny<bool>())).Returns(new List<Event>());
            eventBroker.Setup(foo => foo.GetPreviousEvents(It.IsAny<int>(), It.IsAny<int>())).Returns(eventData);
            eventBroker.Setup(foo => foo.GetPreviousEvents(100, 100)).Returns(new List<Event>());
            eventBroker.Setup(foo => foo.GetPreviousEvents(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(eventData);
            eventBroker.Setup(foo => foo.GetPreviousEvents("dotnet miami", 100, 100, It.IsAny<bool>())).Returns(new List<Event>());
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.Module<EventsModule>().Dependency(eventBroker.Object);
            });
            app = new Browser(bootstrapper);
        }

        public IEnumerable<Event> GetEventData()
        {
            var group1 = new Group { Id = "groups/1", Name="Group One", IsExternalGroup = false };
            var event1 = new Event { Id = "events/1", Title = "Testing Events One", EventDateTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-15), TimeZoneInfo.Local) };
            event1.HostedGroups.Add(group1);
            var event2 = new Event { Id = "events/2", Title = "Testing Events Two", EventDateTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(15), TimeZoneInfo.Local) };
            event2.HostedGroups.Add(group1);
            var event3 = new Event { Id = "events/3", Title = "Testing Events Three", EventDateTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(45), TimeZoneInfo.Local) };
            event3.HostedGroups.Add(group1);

            List<Event> returnEvent = new List<Event>();
            returnEvent.Add(event1);
            returnEvent.Add(event2);
            returnEvent.Add(event3);
            return returnEvent.AsEnumerable<Event>();
        }

        private BrowserResponse CallWebService(string path)
        {
            var response = app.Get(path,
                 with =>
                 {
                     with.HttpRequest();
                 });
            return response;
        }

        private BrowserResponse CallWebService(string path, string PageSize, string PageNumber)
        {
            var response = app.Get(path,
                 with =>
                 {
                     with.HttpRequest();
                     with.Query("pagenumber", PageNumber);
                     with.Query("pagesize", PageSize);
                 });
            return response;
        }

        // Test naming pattern: [MethodName]_[StateUnderTest]_[ExpectedBehavior].
        #region /events

        [Fact]
        public void EventModule_DefaultParams_GetEventsCalled()
        {
            var response = CallWebService("/events/");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            eventBroker.Verify(m => m.GetEvents(10, 1));
        }

        [Fact]
        public void EventModule_PageNumberPageSize_GetEventsCalled()
        {
            var response = CallWebService("/events/", "2", "1");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetEvents(2, 1));
        }

        [Fact]
        public void EventModule_InvalidPageNumberPageSize_GetEventsCalled()
        {
            var response = CallWebService("/events/", "abcd", "efgh");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetEvents(10, 1));
        }

        [Fact]
        public void EventModule_HttpNotFound_GetEventsCalled()
        {
            var response = CallWebService("/events/", "100", "100");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(events);
            eventBroker.Verify(m => m.GetEvents(100, 100));
        }

        #endregion

        #region /events/upcoming

        [Fact]
        public void EventModule_UpcomingDefaultParams_GetUpcomingEventsCalled()
        {
            var response = CallWebService("/events/upcoming/");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            eventBroker.Verify(m => m.GetUpcomingEvents(10, 1));
        }

        [Fact]
        public void EventModule_UpcomingPageNumberPageSize_GetUpcomingEventsCalled()
        {
            var response = CallWebService("/events/upcoming", "2", "1");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetUpcomingEvents(2, 1));
        }

        [Fact]
        public void EventModule_UpcomingInvalidPageNumberPageSize_GetUpcomingEventsCalled()
        {
            var response = CallWebService("/events/upcoming", "abcd", "efgh");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetUpcomingEvents(10, 1));
        }

        [Fact]
        public void EventModule_UpcomingHttpNotFound_GetUpcomingEventsCalled()
        {
            var response = CallWebService("/events/upcoming", "100", "100");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(events);
            eventBroker.Verify(m => m.GetUpcomingEvents(100, 100));
        }

        #endregion

        #region /events/{groupname}/upcoming

        [Fact]
        public void EventModule_GroupUpcomingDefaultParams_GetUpcomingEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/upcoming/");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            eventBroker.Verify(m => m.GetUpcomingEvents("dotnet miami", 10, 1, false));
        }

        [Fact]
        public void EventModule_GroupUpcomingPageNumberPageSize_GetUpcomingEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/upcoming", "2", "1");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetUpcomingEvents("dotnet miami", 2, 1, false));
        }

        [Fact]
        public void EventModule_GroupUpcomingInvalidPageNumberPageSize_GetUpcomingEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/upcoming", "abcd", "efgh");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetUpcomingEvents("dotnet miami", 10, 1, false));
        }

        [Fact]
        public void EventModule_GroupUpcomingHttpNotFound_GetUpcomingEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/upcoming", "100", "100");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(events);
            eventBroker.Verify(m => m.GetUpcomingEvents("dotnet miami", 100, 100, false));
        }

        #endregion

        #region /events/{groupname}/upcoming/external

        [Fact]
        public void EventModule_GroupUpcomingExternalDefaultParams_GetUpcomingEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/upcoming/external");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            eventBroker.Verify(m => m.GetUpcomingEvents("dotnet miami", 10, 1, true));
        }

        [Fact]
        public void EventModule_GroupUpcomingExternalPageNumberPageSize_GetUpcomingEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/upcoming/external", "2", "1");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetUpcomingEvents("dotnet miami", 2, 1, true));
        }

        [Fact]
        public void EventModule_GroupUpcomingExternalInvalidPageNumberPageSize_GetUpcomingEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/upcoming/external", "abcd", "efgh");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetUpcomingEvents("dotnet miami", 10, 1, true));
        }

        [Fact]
        public void EventModule_GroupUpcomingExternalHttpNotFound_HttpNotFound()
        {
            var response = CallWebService("/events/dotnet miami/upcoming/external", "100", "100");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(events);
            eventBroker.Verify(m => m.GetUpcomingEvents("dotnet miami", 100, 100, true));
        }

        #endregion

        #region /events/{groupname}

        [Fact]
        public void EventModule_GroupDefaultParams_GetEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            eventBroker.Verify(m => m.GetEvents("dotnet miami", 10, 1, false));
        }

        [Fact]
        public void EventModule_GroupPageNumberPageSize_GetEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/", "2", "1");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetEvents("dotnet miami", 2, 1, false));
        }

        [Fact]
        public void EventModule_GroupInvalidPageNumberPageSize_GetEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/", "abcd", "efgh");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetEvents("dotnet miami", 10, 1, false));
        }

        [Fact]
        public void EventModule_GroupHttpNotFound_HttpNotFound()
        {
            var response = CallWebService("/events/dotnet miami/", "100", "100");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(events);
            eventBroker.Verify(m => m.GetEvents("dotnet miami", 100, 100, false));
        }

        #endregion

        #region /events/{groupname}/external

        [Fact]
        public void EventModule_GroupExternalDefaultParams_GetEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/external");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            eventBroker.Verify(m => m.GetEvents("dotnet miami", 10, 1, true));
        }

        [Fact]
        public void EventModule_GroupExternalPageNumberPageSize_GetEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/external", "2", "1");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetEvents("dotnet miami", 2, 1, true));
        }

        [Fact]
        public void EventModule_GroupExternalInvalidPageNumberPageSize_GetEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/external", "abcd", "efgh");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetEvents("dotnet miami", 10, 1, true));
        }

        [Fact]
        public void EventModule_GroupExternalHttpNotFound_HttpNotFound()
        {
            var response = CallWebService("/events/dotnet miami/external", "100", "100");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(events);
            eventBroker.Verify(m => m.GetEvents("dotnet miami", 100, 100, true));
        }

        #endregion

        #region /events/previous

        [Fact]
        public void EventModule_PreviouslDefaultParams_GetPreviousEventsCalled()
        {
            var response = CallWebService("/events/previous");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            eventBroker.Verify(m => m.GetPreviousEvents(10, 1));
        }

        [Fact]
        public void EventModule_PreviousPageNumberPageSize_GetPreviousEventsCalled()
        {
            var response = CallWebService("/events/previous", "2", "1");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetPreviousEvents(2, 1));
        }

        [Fact]
        public void EventModule_PreviousInvalidPageNumberPageSize_GetPreviousEventsCalled()
        {
            var response = CallWebService("/events/previous", "abcd", "efgh");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetPreviousEvents(10, 1));
        }

        [Fact]
        public void EventModule_PreviousHttpNotFound_HttpNotFound()
        {
            var response = CallWebService("/events/previous", "100", "100");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(events);
            eventBroker.Verify(m => m.GetPreviousEvents(100, 100));
        }

        #endregion

        #region /events/{groupname}/previous

        [Fact]
        public void EventModule_GroupPreviouslDefaultParams_GetPreviousEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/previous");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            eventBroker.Verify(m => m.GetPreviousEvents("dotnet miami", 10, 1, false));
        }

        [Fact]
        public void EventModule_GroupPreviousPageNumberPageSize_GetPreviousEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/previous", "2", "1");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetPreviousEvents("dotnet miami", 2, 1, false));
        }

        [Fact]
        public void EventModule_GroupPreviousInvalidPageNumberPageSize_GetPreviousEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/previous", "abcd", "efgh");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetPreviousEvents("dotnet miami", 10, 1, false));
        }

        [Fact]
        public void EventModule_GroupPreviousHttpNotFound_HttpNotFound()
        {
            var response = CallWebService("/events/dotnet miami/previous", "100", "100");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(events);
            eventBroker.Verify(m => m.GetPreviousEvents("dotnet miami", 100, 100, false));
        }

        #endregion

        #region /events/{groupname}/previous/external

        [Fact]
        public void EventModule_GroupPreviousExternallDefaultParams_GetEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/previous/external");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            eventBroker.Verify(m => m.GetPreviousEvents("dotnet miami", 10, 1, true));
        }

        [Fact]
        public void EventModule_GroupPreviousExternalPageNumberPageSize_GetEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/previous/external", "2", "1");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetPreviousEvents("dotnet miami", 2, 1, true));
        }

        [Fact]
        public void EventModule_GroupPreviousExternalInvalidPageNumberPageSize_GetEventsCalled()
        {
            var response = CallWebService("/events/dotnet miami/previous/external", "abcd", "efgh");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetPreviousEvents("dotnet miami", 10, 1, true));
        }

        [Fact]
        public void EventModule_GroupPreviousExternalHttpNotFound_HttpNotFound()
        {
            var response = CallWebService("/events/dotnet miami/previous/external", "100", "100");
            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(events);
            eventBroker.Verify(m => m.GetPreviousEvents("dotnet miami", 100, 100, true));
        }

        #endregion

    }
}
