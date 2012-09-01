namespace ShindyUI.App.Model
{
    using System.Collections.ObjectModel;

    using ShindyUI.App.Common;

    public class Session : BindableBase
    {
        public Session()
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

        private string _abstract;
        public string _Abstract
        {
            get { return this._abstract; }
            set { this.SetProperty(ref this._abstract, value); }
        }

        private string sessionType;
        public string SessionType
        {
            get { return this.sessionType; }
            set { this.SetProperty(ref this.sessionType, value); }
        }

        private string presentationURI;
        public string PresentationURI
        {
            get { return this.presentationURI; }
            set { this.SetProperty(ref this.presentationURI, value); }
        }

        private string demoURI;
        public string DemoURI
        {
            get { return this.demoURI; }
            set { this.SetProperty(ref this.demoURI, value); }
        }
      
        public virtual ObservableCollection<Person> Speakers { get; set; }

    }   
}