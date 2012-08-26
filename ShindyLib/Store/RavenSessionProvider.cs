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
    public  class RavenSessionProvider :IRavenSessionProvider
    {

        #region PROPERTIES
         
        //private static fields created to circumvent the configurationmanager in unit tests
         private static bool? isRemote;
         private static string storeName;
         private static string localUrl;
         private static string remoteUrl;

        private static DocumentStore _documentStore;
        public static  DocumentStore DocumentStore
        {
            get { return (_documentStore ?? (_documentStore = CreateDocumentStore())); }
        }
              
        public static bool IsRemote
        {
            get 
            {
                return isRemote ?? ConfigurationManager.AppSettings["environment"] != null && ConfigurationManager.AppSettings["environment"].Equals("remote"); 
            }
            set
            {
                isRemote = value;
            }
        }
       
        public static  string StoreName
        {
            get
            {
                return storeName ?? ConfigurationManager.AppSettings["storename"].ToString();
            }
            set
            {
                storeName = value;
            }
        }
               
        public static string LocalUrl
        {
            get
            {
                return localUrl ?? ConfigurationManager.ConnectionStrings["RavenDBLocal"].ToString();
            }
            set
            {
                localUrl = value;
            }
        }
                
        public static string RemoteUrl
        {
            get
            {
                return remoteUrl;
            }
            set
            {
                remoteUrl = value;
            }
        }

        #endregion

        public RavenSessionProvider() {}
        
        /// <summary>
        /// Static method to create document store to make sure that only one instance of the store is 
        /// created per application lifecycle. Raven needs to make the store init more efficient, so
        /// such compromise is not needed. mongo does that better :P
        /// </summary>
        /// <returns></returns>
        private static  DocumentStore CreateDocumentStore()
        {    
            DocumentStore store = new DocumentStore();
            return store;
        }

        public virtual IDocumentSession OpenSession()
        {
            ConnectionStringParser<RavenConnectionStringOptions> parser = null;
           
            //Allow connection to be established both by name and by value for unit testing purposes
            var connStringNameExists =  ConfigurationManager.ConnectionStrings["RavenDB"] != null;
            if (connStringNameExists)
                parser = ConnectionStringParser<RavenConnectionStringOptions>.FromConnectionStringName("RavenDB");
            
            if(!string.IsNullOrEmpty(RemoteUrl))
                parser = parser = ConnectionStringParser<RavenConnectionStringOptions>.FromConnectionString(RemoteUrl); 
            
            //Only parse for remote API access if settings are available
            if(connStringNameExists || !string.IsNullOrEmpty(RemoteUrl))
                parser.Parse();

            DocumentStore.Url = IsRemote ? parser.ConnectionStringOptions.Url : LocalUrl;
          
            if(IsRemote)
                DocumentStore.ApiKey = parser.ConnectionStringOptions.ApiKey;
            
            var session = DocumentStore.Initialize().OpenSession(IsRemote ? null : StoreName);
            return session;
        }
    }
}
