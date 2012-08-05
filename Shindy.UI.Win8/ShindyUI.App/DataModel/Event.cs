namespace ShindyUI.App.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Collections.ObjectModel;
    using ShindyUI.App.Common;

    public class Event : BindableBase
    {
        ObservableCollection<Session> sessions = new ObservableCollection<Session>();

        public Event()
        {
        
        }
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }

        private string title;
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        private string eventDateTime;
        public string EventDateTime
        {
            get { return this.eventDateTime; }
            set { this.SetProperty(ref this.eventDateTime, value); }
        }

        private string description;
        public string Description
        {
            get { return this.description; }
            set
            {
                this.SetProperty(ref this.description, Regex.Replace(value, "<.*?>", string.Empty));
            }
        }

        private string registrationURI;
        public string RegistrationURI
        {
            get { return this.registrationURI; }
            set { this.SetProperty(ref this.registrationURI, value); }
        }

        private string isActive;
        public string IsActive
        {
            get { return this.isActive; }
            set { this.SetProperty(ref this.isActive, value); }
        }


        public ObservableCollection<Giveaway> Giveaways { get; set; }

        private Location eventLocation;
        public Location EventLocation
        {
            get { return this.eventLocation; }
            set { this.SetProperty(ref this.eventLocation, value); }
        }

        private ObservableCollection<Group> hostedGroups;
        public ObservableCollection<Group> HostedGroups
        {
            get { return this.hostedGroups; }
            set
            {
                if(value.Count > 1)
                {
                    this.TheHostedGroup = value[0];
                }

                this.SetProperty(ref this.hostedGroups, value);
            }
        }

        private Group theHostedGroup;
        public Group TheHostedGroup
        {
            get { return this.theHostedGroup; }
            set { this.SetProperty(ref this.theHostedGroup, value); }
        }

        public ObservableCollection<Sponsor> Sponsors { get; set; }

        public ObservableCollection<Session> Sessions
        {
            get { return this.sessions; }
            set { this.SetProperty(ref this.sessions, value); }
        }
    }

}