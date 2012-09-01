namespace ShindyUI.App.Model
{
    using ShindyUI.App.Common;

    public class Location : BindableBase
    {
        public Location()
        {
        
        }
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }

        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }

        private string locationURI;
        public string LocationURI
        {
            get { return this.locationURI; }
            set { this.SetProperty(ref this.locationURI, value); }
        }

        public Event HostedEvents { get; set; }

        public virtual Event Event { get; set; }

        public virtual Address Address { get; set; }

    }
}

