﻿using System;
using System.Collections.Generic;
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
using BaseDataItem = ShindyUI.App.Data.BaseDataItem;

// The Group Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234229

namespace ShindyUI.App
{
    using ShindyDataSource = ShindyUI.App.Model.DataModel.ShindyDataSource;

    /// <summary>
    /// A page that displays an overview of a single group, including a preview of the items
    /// within the group.
    /// </summary>
    public sealed partial class GroupDetailPage : ShindyUI.App.Common.LayoutAwarePage
    {
        public GroupDetailPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var eventModel = ShindyDataSource.GetEvent((String)navigationParameter);
            this.DefaultViewModel["Event"] = eventModel;
            this.DefaultViewModel["Sessions"] = eventModel.Sessions;
            this.DefaultViewModel["Sponsors"] = eventModel.Sponsors;
        }

        /// <summary>
        /// Invoked when an item is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var itemId = ((BaseDataItem)e.ClickedItem).UniqueId;
            this.Frame.Navigate(typeof(EventDetailPage), itemId);
        }
    }
}
