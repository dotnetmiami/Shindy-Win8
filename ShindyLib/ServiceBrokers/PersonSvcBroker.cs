using System;
using System.Linq;
using System.Text;
using Raven.Client;
using System.Configuration;
using Raven.Client.Document;
using System.Collections.Generic;
using EventLibrary.Entities;
using EventLibrary.Extensions;
using SimpleSocialAuth.MVC3.Handlers;

namespace EventLibrary.ServiceBrokers
{
     public class PersonSvcBroker : IServiceBroker
    {
        #region PROPERTIES/FIELDS
        public DocumentStore Context {get;set;}
        public string ProxyUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ravenproxy"].ToString();
            }
        }
        public string StoreName
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
            Context = new DocumentStore { Url = ProxyUrl };
            Context.Initialize();
        }
        #endregion

        #region DATA READS
        /// <summary>
        /// Queries store and get a person by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Person GetPersonById(string id)
        {
          using (var session = Context.OpenSession(StoreName))
          { 
                return session.Load<Person>(string.Format("people/{0}",id));
          }
        }
        
        /// <summary>
        /// Checks if a user is in the store, if it is, process is bypassed. Otherwhise 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>Will add much more data later on from social request</remarks>
        public bool ValidateUser(BasicUserData socialData)
        {
            using (var session = Context.OpenSession(StoreName))
            {
                var person = session.Query<Person>().Where(p => p.SocialId.Equals(socialData.UserId.ToString())).FirstOrDefault();
                //If there is no match store new record
                if (person.IsNull())
                {
                    person = new Person 
                    { 
                        SocialId = socialData.UserId,
                        FirstName = socialData.UserName.Split(' ')[0],
                        LastName = socialData.UserName.Split(' ')[1],
                        PictureURI = socialData.PictureUrl
                    };
                    session.Store(person);
                    session.SaveChanges();
                }
                return true;
            } 
        }

        /// <summary>
        /// Retrieve every one from database
        /// </summary>
        /// <returns></returns>
        public List<Person> GetPersons()
        {
            using (var session = Context.OpenSession(StoreName))
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
