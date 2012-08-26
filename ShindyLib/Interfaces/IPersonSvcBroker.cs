using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventLibrary.Interfaces
{
     public interface IPersonSvcBroker
    {
         IRavenSessionProvider SessionProvider { get; set; }
    }
}
