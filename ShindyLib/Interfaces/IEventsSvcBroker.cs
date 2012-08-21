using System;
using System.Collections.Generic;
using EventLibrary.Entities;

namespace EventLibrary
{
    public interface IEventsSvcBroker
    {
        EventLibrary.Entities.Event GetEventById(int eventId);
        IEnumerable<EventLibrary.Entities.Event> GetEvents(int pageSize, int pageNumber);
        IEnumerable<EventLibrary.Entities.Event> GetEvents(string groupName, int pageSize, int pageNumber, bool externalEventsOnly = false);
        IEnumerable<EventLibrary.Entities.Event> GetPreviousEvents(int pageSize, int pageNumber);
        IEnumerable<EventLibrary.Entities.Event> GetPreviousEvents(string groupName, int pageSize, int pageNumber, bool externalEventsOnly = false);
        IEnumerable<EventLibrary.Entities.Event> GetUpcomingEvents(int pageSize, int pageNumber);
        IEnumerable<EventLibrary.Entities.Event> GetUpcomingEvents(string groupName, int pageSize, int pageNumber, bool externalEventsOnly = false);
        int VerifyPositiveInt(int value, int defaultValue = 1);
    }
}
