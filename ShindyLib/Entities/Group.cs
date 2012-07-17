using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventLibrary.Entities
{

    public class Group
    {
        public int GroupID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<Event> Events { get; set; }

    }
}

