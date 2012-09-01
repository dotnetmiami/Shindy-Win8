namespace ShindyUI.App.Model
{
    using System.Collections.Generic;

    using ShindyUI.App.Common;

    public class Group : BindableBase
    {
        public Group()
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

        private string description;
        public string Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }

        public virtual List<Event> Events { get; set; }

    }
}

