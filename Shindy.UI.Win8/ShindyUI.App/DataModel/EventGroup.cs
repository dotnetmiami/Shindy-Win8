using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShindyUI.App.Common;

namespace ShindyUI.App.DataModel
{
    public class EventGroup : BindableBase
    {
        public string title;

        private ObservableCollection<Event> items = new ObservableCollection<Event>();

        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        public ObservableCollection<Event> Items
        {
            get { return this.items; }
            set { this.SetProperty(ref this.items, value); }
        }

        public IEnumerable<Event> TopItems
        {
            // Provides a subset of the full items collection to bind to from a GroupedItemsPage
            // for two reasons: GridView will not virtualize large items collections, and it
            // improves the user experience when browsing through groups with large numbers of
            // items.
            //
            // A maximum of 12 items are displayed because it results in filled grid columns
            // whether there are 1, 2, 3, 4, or 6 rows displayed
            get { return this.items.Take(12); }
        }

        public EventGroup(string title, ObservableCollection<Event> items)
        {
            this.title = title;
            this.Items = items;  
        }
    }
}