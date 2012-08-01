using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using EventLibrary.Interfaces;
using Raven.Client;
using Raven.Client.Document;

namespace EventLibrary
{
    /*
     * Adapted from https://github.com/NancyFx/DinnerParty/blob/master/src/Models/RavenDB/RavenSessionProvider.cs
     */
    public class RavenSessionProvider : IRavenSessionProvider
    {
        private static DocumentStore _documentStore;

        public static DocumentStore DocumentStore
        {
            get { return (_documentStore ?? (_documentStore = CreateDocumentStore())); }
        }

        public static string ProxyUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ravenproxy"].ToString();
            }
        }
        public static string StoreName
        {
            get
            {
                return ConfigurationManager.AppSettings["dnmdb"].ToString();
            }
        }

        private static DocumentStore CreateDocumentStore()
        {
            DocumentStore store = new DocumentStore
            {
               Url = ProxyUrl
            };

            store.Initialize();

            return store;
        }

        public virtual IDocumentSession OpenSession()
        {
            var session = DocumentStore.OpenSession(StoreName);
            return session;
        }
    }
}
