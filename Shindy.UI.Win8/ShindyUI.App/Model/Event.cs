namespace ShindyUI.App.Model
{
    using System;
    using System.Collections.ObjectModel;

    using GalaSoft.MvvmLight;

    public class Event : ViewModelBase
    {
        #region Fields

        private string id;
        private string title;
        private DateTime eventDateTime;
        private string registrationUri;
        private string description;
        private bool isActive;
        private Location eventLocation;
        private ObservableCollection<Group> hostedGroups;
        private Group mainHost;
        private ObservableCollection<Sponsor> sponsors;
        private ObservableCollection<Session> sessions;
        private ObservableCollection<Giveaway> giveaways;

        #endregion

        #region Constructors

        public Event()
        {
        
        }

        #endregion

        #region Properties

        public string Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
                this.RaisePropertyChanged("Id");
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.RaisePropertyChanged("Title");
            }
        }

        public DateTime EventDateTime
        {
            get
            {
                return this.eventDateTime;
            }

            set
            {
                this.eventDateTime = value;
                this.RaisePropertyChanged("EventDateTime");
            }
        }

        public Group MainHost
        {
            get
            {
                return this.mainHost;
            }

            set
            {
                this.mainHost = value;
                this.RaisePropertyChanged("MainHost");
            }
        }

        public ObservableCollection<Giveaway> Giveaways
        {
            get
            {
                return this.giveaways;
            }

            set
            {
                this.giveaways = value;
                this.RaisePropertyChanged("Giveaways");
            }
        }

        public ObservableCollection<Session> Sessions
        {
            get
            {
                return this.sessions;
            }

            set
            {
                this.sessions = value;
                this.RaisePropertyChanged("Sessions");
            }
        }

        public ObservableCollection<Sponsor> Sponsors
        {
            get
            {
                return this.sponsors;
            }

            set
            {
                this.sponsors = value;
                this.RaisePropertyChanged("Sponsors");
            }
        }

        public ObservableCollection<Group> HostedGroups
        {
            get
            {
                return this.hostedGroups;
            }

            set
            {
                this.hostedGroups = value;
                this.RaisePropertyChanged("HostedGroups");
            }
        }

        public Location EventLocation
        {
            get
            {
                return this.eventLocation;
            }

            set
            {
                this.eventLocation = value;
                this.RaisePropertyChanged("EventLocation");
            }
        }

        public bool IsActive
        {
            get
            {
                return this.isActive;
            }

            set
            {
                this.isActive = value;
                this.RaisePropertyChanged("IsActive");
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                this.description = value;
                this.RaisePropertyChanged("Description");
            }
        }

        public string RegistrationUri
        {
            get
            {
                return this.registrationUri;
            }

            set
            {
                this.registrationUri = value;
                this.RaisePropertyChanged("RegistrationUri");
            }
        }

        #endregion
    }
}