using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShindyUI.App.Common;

namespace ShindyUI.App.DataModel
{
     /// <summary>
    /// Admin - Can administer the web application
    /// Entry - Can enter information in web application
    /// User - Can login to the web application
    /// Member - Has attended at least one event
    /// Speaker - Has spoken at least on event 
    /// </summary>
    public class Role : BindableBase
    {
        public Role()
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
        
    }
}