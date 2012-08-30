using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using EventLibrary.Interfaces;
using Raven.Client;
using Raven.Client.Document;
using Raven.Abstractions.Data;

namespace EventLibrary
{
    /*
     * Adapted from https://github.com/NancyFx/DinnerParty/blob/master/src/Models/RavenDB/RavenSessionProvider.cs
     */
    public class RavenSessionProvider : IRavenSessionProvider
    {

        #region PROPERTIES
        private DocumentStore _documentStore;

        public DocumentStore DocumentStore
        {
            get { return (_documentStore ?? (_documentStore = CreateDocumentStore())); }
        }

        public string LocalUrl
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["RavenDBLocal"].ToString();
            }
        }

        public string StoreName
        {
            get
            {
                return ConfigurationManager.AppSettings["storename"].ToString();
            }
        }

        public bool IsRemote
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["RavenDB"] != null && !string.IsNullOrWhiteSpace(ConfigurationManager.ConnectionStrings["RavenDB"].ToString());
            }
        }

        public ConnectionStringParser<RavenConnectionStringOptions> Parser
        {
            get;
            set;
        }
        #endregion

        #region CONSTRUCTOR
        public RavenSessionProvider()
        {
            this.Parser = ConnectionStringParser<RavenConnectionStringOptions>.FromConnectionStringName(IsRemote ? "RavenDB" : "RavenDBLocal"); 
        }
        #endregion

        #region HELPERS
        private DocumentStore CreateDocumentStore()
        {   
            this.Parser.Parse();

            DocumentStore store = new DocumentStore
            {
                Url = this.Parser.ConnectionStringOptions.Url
            };

            if (IsRemote)
                store.ApiKey = Parser.ConnectionStringOptions.ApiKey;

            store.Initialize();

            return store;
        }
        
        public virtual IDocumentSession OpenSession()
        {
            var session = DocumentStore.OpenSession(!string.IsNullOrWhiteSpace(this.Parser.ConnectionStringOptions.ApiKey) ? null : StoreName);
            return session;
        }
        #endregion
    }
}
