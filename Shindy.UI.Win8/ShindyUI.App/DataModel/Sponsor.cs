using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShindyUI.App.Common;

namespace ShindyUI.App.DataModel
{

    public class Sponsor : BindableBase
    {

        public Sponsor()
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

        private string imageURI;
        public string ImageURI
        {
            get { return this.imageURI; }
            set { this.SetProperty(ref this.imageURI, value); }
        }

        private string sponsorURI;
        public string SponsorURI
        {
            get { return this.sponsorURI; }
            set { this.SetProperty(ref this.sponsorURI, value); }
        }

        public virtual List<Event> Events { get; set; }

        public virtual List<Giveaway> Giveaways { get; set; }
    }
}
