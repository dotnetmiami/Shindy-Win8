using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace EventLibrary.Entities
{

    public class Person
    {
        public int PersonID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PersonURI { get; set; }

        public string TwitterName { get; set; }

        public virtual string Bio { get; set; }

        public virtual Address Address { get; set; }

        public virtual List<Role> Roles { get; set; }

    }

}