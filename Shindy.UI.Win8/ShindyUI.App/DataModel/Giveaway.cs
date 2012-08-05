using ShindyUI.App.Common;

namespace ShindyUI.App.DataModel
{
    public class Giveaway : BindableBase
    {
        public Giveaway()
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

        public virtual Person Winner{get; set;}

        public virtual Sponsor Sponsor { get; set; }
    }

}