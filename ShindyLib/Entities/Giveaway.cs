using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventLibrary.Entities
{
    public class Giveaway
    {
        public int Id { get; set;}

        public string Name { get; set;}

        public string Description { get; set; }

        public virtual Person Winner{get; set;}

        public virtual Sponsor Sponsor { get; set; }
    }

}