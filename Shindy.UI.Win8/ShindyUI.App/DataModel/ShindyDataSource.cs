using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShindyUI.App.Data;

namespace ShindyUI.App.DataModel
{
    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// </summary>
    public sealed class ShindyDataSource
    {
        private static ShindyDataSource _shindyDataSource = new ShindyDataSource();

        private ObservableCollection<Event> allEvents = new ObservableCollection<Event>();

        public ObservableCollection<Event> AllEvents
        {
            get { return this.allEvents; }
        }

        public static IEnumerable<Event> GetEvents(string uniqueId)
        {
            if (!uniqueId.Equals("AllEvents")) throw new ArgumentException("Only 'AllEvents' is supported as a collection of groups");

            return _shindyDataSource.AllEvents;
        }

        public static IEnumerable<EventGroup> GetEventsGroup(string uniqueId)
        {
            if (!uniqueId.Equals("AllEvents"))
                throw new ArgumentException("Only 'AllEvents' is supported as a collection of groups");

            var result = new ObservableCollection<EventGroup>();
            result.Add(new EventGroup("EVENTS", _shindyDataSource.allEvents));
            return result;
        }



        public static Event GetEvent(string name)
        {
            var matches = _shindyDataSource.AllEvents.Where((ev) => ev.Title.Equals(name));

            if (matches.Count() == 1)
            {
                return matches.First();
            }

            return null;
        }

        static ShindyDataSource()
        {
            _shindyDataSource = new ShindyDataSource();
        }

        public ShindyDataSource()
        {
            this.Init();
        }

        public async void Init()
        {
            this.allEvents = new ObservableCollection<Event>(await DataService.GetEvents());
            MainPage.Current.DefaultViewModel["Events"] = GetEventsGroup("AllEvents");
        }
    }
}
