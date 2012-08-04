﻿using System;
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
 
        public  DocumentStore DocumentStore
        {
            get { return (_documentStore ?? (_documentStore = CreateDocumentStore())); }
        }

        public  string ProxyUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["RavenDB"].ToString();
            }
        }


        public  string StoreName
        {
            get
            {
                return ConfigurationManager.AppSettings["dnmdb"].ToString();
            }
        }

        public ConnectionStringParser<RavenConnectionStringOptions> Parser
        {
            get;
            set;
        }
        #endregion

        public RavenSessionProvider()
        {
            this.Parser = ConnectionStringParser<RavenConnectionStringOptions>.FromConnectionStringName("RavenDB");
        }
       
        private  DocumentStore CreateDocumentStore()
        {
            this.Parser.Parse();

            DocumentStore store = new DocumentStore
            {
                Url = string.IsNullOrWhiteSpace(this.Parser.ConnectionStringOptions.ApiKey) ? ProxyUrl : this.Parser.ConnectionStringOptions.Url,
               ApiKey = Parser.ConnectionStringOptions.ApiKey
            };
            store.Initialize();
            
            return store;
        }

        public virtual IDocumentSession OpenSession()
        {
            var session = DocumentStore.OpenSession(!string.IsNullOrWhiteSpace(this.Parser.ConnectionStringOptions.ApiKey) ? null : StoreName);
            return session;
        }
    }
}
