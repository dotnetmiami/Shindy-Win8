using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventLibrary.Interfaces;
using Raven.Client;
using Raven.Client.Document;


namespace EventLibrary
{
    /// <summary>
    /// Broker interface used to reinforce code standards for service brokers
    /// </summary>
    public interface IServiceBroker
    {
        IRavenSessionProvider Context { get; set; }       
    }
}
