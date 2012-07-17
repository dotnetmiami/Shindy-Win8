using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace EventLibrary.Entities
{
     /// <summary>
    /// Admin - Can administer the web application
    /// Entry - Can enter information in web application
    /// User - Can login to the web application
    /// Member - Has attended at least one event
    /// Speaker - Has spoken at least on event 
    /// </summary>
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}