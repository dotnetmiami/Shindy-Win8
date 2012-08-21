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

namespace EventTest.ShindyWebService
{
    public class EventsModuleTest
    {
        private Browser app;
        private Mock<EventLibrary.IEventsSvcBroker> eventBroker;

        public EventsModuleTest()
        {
            eventBroker = new Mock<EventLibrary.IEventsSvcBroker>();
            eventBroker.Setup(foo => foo.GetEvents(It.IsAny<int>(), It.IsAny<int>())).Returns(EventData());
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.Module<EventsModule>().Dependency(eventBroker.Object);
            });
            app = new Browser(bootstrapper);
        }

        public IEnumerable<Event> EventData()
        {

            IEnumerable<Event> returnEvent = new[] { new Event { Id = "event/1", Title = "Testing Events", EventDateTime = DateTime.Now } }; 
            return returnEvent;
        }

        // Test naming pattern: [MethodName]_[StateUnderTest]_[ExpectedBehavior].

        [Fact]
        public void EventModule_DefaultParams_ValidEvents()
        {
            var response = app.Get("/events/",
                 with => {
                     with.HttpRequest();
                 });

            //Then
            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            //var eventTest = response.Body.DeserializeJson<EventLibrary.Entities.Event>();
            
        }

    }
}
