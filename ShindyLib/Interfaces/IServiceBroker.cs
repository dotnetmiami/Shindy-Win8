using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;
using Raven.Client.Document;


namespace EventLibrary
{
    /// <summary>
    /// Broker interface used to reinforce code standards for service brokers
    /// </summary>
    internal interface IServiceBroker
    {
         DocumentStore Context {get;set;}
         string ProxyUrl { get;}
         string StoreName { get; }
    }
}
