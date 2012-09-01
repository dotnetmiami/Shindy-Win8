namespace ShindyUI.App.Model
{
    using System.Collections.Generic;

    using ShindyUI.App.Common;

    public class Person : BindableBase
    {
        public Person()
        {
        
        }
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }

        private string socialId;
        public string SocialId
        {
            get { return this.socialId; }
            set { this.SetProperty(ref this.socialId, value); }
        }

        private string firstName;
        public string FirstName
        {
            get { return this.firstName; }
            set { this.SetProperty(ref this.firstName, value); }
        }

        private string lastName;
        public string LastName
        {
            get { return this.lastName; }
            set { this.SetProperty(ref this.lastName, value); }
        }

        private string email;
        public string Email
        {
            get { return this.email; }
            set { this.SetProperty(ref this.email, value); }
        }

        private string personURI;
        public string PersonURI
        {
            get { return this.personURI; }
            set { this.SetProperty(ref this.personURI, value); }
        }

        private string pictureURI;
        public string PictureURI
        {
            get { return this.pictureURI; }
            set { this.SetProperty(ref this.pictureURI, value); }
        }

        private string twitterName;
        public string TwitterName
        {
            get { return this.twitterName; }
            set { this.SetProperty(ref this.twitterName, value); }
        }

        private string bio;
        public string Bio
        {
            get { return this.bio; }
            set { this.SetProperty(ref this.bio, value); }
        }
        
        public virtual Address Address { get; set; }

        public virtual List<Role> Roles { get; set; }

    }

}