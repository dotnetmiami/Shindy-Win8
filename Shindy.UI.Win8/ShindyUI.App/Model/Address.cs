namespace ShindyUI.App.Model
{
    using GalaSoft.MvvmLight;

    using ShindyUI.App.Common;

    public class Address : ViewModelBase
    {
        #region Fields

        #endregion

        public Address()
        {
        
        }
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }

        private string street;
        public string Street
        {
            get { return this.street; }
            set { this.SetProperty(ref this.street, value); }
        }

        private string street2;
        public string Street2
        {
            get { return this.street2; }
            set { this.SetProperty(ref this.street2, value); }
        }

        private string city;
        public string City
        {
            get { return this.city; }
            set { this.SetProperty(ref this.city, value); }
        }

        private string state;
        public string State
        {
            get { return this.state; }
            set { this.SetProperty(ref this.state, value); }
        }

        private string zipCode;
        public string ZipCode
        {
            get { return this.zipCode; }
            set { this.SetProperty(ref this.zipCode, value); }
        }

        private string addressURL;
        public string AddressURL
        {
            get { return this.addressURL; }
            set { this.SetProperty(ref this.addressURL, value); }
        }
        
    }
}

