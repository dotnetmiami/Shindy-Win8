using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;

namespace EventLibrary.Interfaces
{
    public interface IRavenSessionProvider
    {
        IDocumentSession OpenSession();
    }
}
