using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventLibrary.Entities
{
    public class Sponsor
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageURI { get; set; }

        public string SponsorURI { get; set; }

        public virtual List<Event> Events { get; set; }

        public virtual List<Giveaway> Giveaways { get; set; }
    }
}
