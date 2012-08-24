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
            eventBroker.Setup(foo => foo.GetUpcomingEvents(It.IsAny<int>(), It.IsAny<int>())).Returns(eventData);
            eventBroker.Setup(foo => foo.GetUpcomingEvents(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(eventData);
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

        [Fact]
        public void EventModule_DefaultParams_GetEventCalled()
        {
            var response = CallWebService("/events/");

            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            eventBroker.Verify(m => m.GetEvents(10, 1));
        }

        [Fact]
        public void EventModule_PageNumberPageSize_GetEventCalled()
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
        public void EventModule_InvalidPageNumberPageSize_GetEventCalled()
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
        public void EventModule_UpcomingDefaultParams_GetEventCalled()
        {
            var response = CallWebService("/events/upcoming/");

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("events/1", response.Body.AsString());
            eventBroker.Verify(m => m.GetUpcomingEvents(10, 1));
        }

        [Fact]
        public void EventModule_UpcomingPageNumberPageSize_GetEventCalled()
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
        public void EventModule_GroupUpcomingDefaultParams_GetEventCalled()
        {
            var response = CallWebService("/events/dotnet miami/upcoming/");

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("events/1", response.Body.AsString());
            eventBroker.Verify(m => m.GetUpcomingEvents("dotnet miami", 10, 1, false));
        }

        [Fact]
        public void EventModule_GroupUpcomingPageNumberPageSize_GetEventCalled()
        {
            var response = CallWebService("/events/dotnet miami/upcoming", "2", "1");

            var events = response.Body.DeserializeJson<IEnumerable<Event>>();

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(events.Count(), eventData.Count());
            Assert.Contains("events/1", response.Body.AsString());
            Assert.Contains("events/2", response.Body.AsString());
            eventBroker.Verify(m => m.GetUpcomingEvents("dotnet miami", 2, 1, false));
        }

    }
}
