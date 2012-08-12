using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace EventLibrary.Entities
{

    public class Event
    {
        public Event()
        {
            Giveaways = new List<Giveaway>();
            HostedGroups = new List<Group>();
            Sponsors = new List<Sponsor>();
            Sessions = new List<Session>();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime EventDateTime { get; set; }

        public string Description { get; set; }

        public string RegistrationURI { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual List<Giveaway> Giveaways { get; set; }

        public virtual Location EventLocation { get; set; }

        public virtual List<Group> HostedGroups { get; set; }

        public virtual List<Sponsor> Sponsors { get; set; }

        public virtual List<Session> Sessions { get; set; }
    }

}