using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventLibrary.Entities
{
    public class Session
    {
        public int SessionID { get; set; }

        public string Title { get; set; }

        public string Abstract { get; set; }

        public string SessionType { get; set; }

        public virtual string PresentationURI { get; set; }

        public virtual string DemoURI { get; set; }

        public virtual List<Person> Speakers { get; set; }

    }   
}