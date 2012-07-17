using System;
using System.Linq;
using System.Text;
using Raven.Client;
using System.Configuration;
using Raven.Client.Document;
using System.Collections.Generic;
using EventLibrary.Entities;
using EventLibrary.Extensions;


namespace EventLibrary.ServiceBrokers
{
     public class PersonSvcBroker
    {
        #region PROPERTIES/FIELDS
        private DocumentStore context = null;
        private string ProxyUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ravenproxy"].ToString();
            }
        }
        private string dotnetMiamiStore
        {
            get
            {
                return ConfigurationManager.AppSettings["dnmdb"].ToString();
            }
        }
        #endregion
        
        #region CONSTRUCTOR
        /// <summary>
        /// Initializes the Member data broker and the raven db components
        /// </summary>
        public PersonSvcBroker()
        {
            context = new DocumentStore { Url = ProxyUrl };
            context.Initialize();
        }
        #endregion

        #region DATA READS
        /// <summary>
        /// Retrieve every one from database
        /// </summary>
        /// <returns></returns>
        public List<Person> GetPersons()
        {
            using (var session = context.OpenSession(dotnetMiamiStore))
            {
                return session.Query<Person>().ToList();
            }
        }

        public void ExampleRoleCheck()
        {
            var p = new Person();
             var isInrole = p.IsPersonInRole(r => r.Name.Equals("Member", StringComparison.InvariantCultureIgnoreCase));   
        }
        #endregion
    }
}
