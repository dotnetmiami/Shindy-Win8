using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Xml.Linq;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ShindyUI.App
{
    using ShindyUI.App.DataModel;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var result = new ObservableCollection<Event>();
            this.itemGridView.ItemsSource = result;
            foreach(var result3 in this.GetResults(e.Parameter.ToString()))
            {
                result.Add(result3);
            }
        }

        private IEnumerable<Event> GetResults(string query)
        {
            
            var matches = ShindyDataSource.GetEvents("AllEvents").Where(e => e.Title.Contains(query)).ToList();
            if(matches != null && matches.Count > 0)
            {
                return matches;
            }

            return  new List<Event>();
        }

        private void ItemViewItemClick(object sender, ItemClickEventArgs e)
        {
            
        }
    }
}
