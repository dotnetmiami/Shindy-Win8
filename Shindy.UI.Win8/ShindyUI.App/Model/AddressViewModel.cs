public class AddressViewModel : ViewModelBase {

#region Fields

private int id;
private string street;
private string street2;
private string city;
private string state;
private string zipCode;
private string addressUrl;


#endregion

#region Constructors

public AddressViewModel(){

}

#endregion

#region Properties

public int Id {
	get {
	  return this.id;
	}	

	set {
	  this.id = value;
	  this.RaisePropertyChanged("Id");
	}
}public string Street {
	get {
	  return this.street;
	}	

	set {
	  this.street = value;
	  this.RaisePropertyChanged("Street");
	}
}public string Street2 {
	get {
	  return this.street2;
	}	

	set {
	  this.street2 = value;
	  this.RaisePropertyChanged("Street2");
	}
}public string City {
	get {
	  return this.city;
	}	

	set {
	  this.city = value;
	  this.RaisePropertyChanged("City");
	}
}public string State {
	get {
	  return this.state;
	}	

	set {
	  this.state = value;
	  this.RaisePropertyChanged("State");
	}
}public string ZipCode {
	get {
	  return this.zipCode;
	}	

	set {
	  this.zipCode = value;
	  this.RaisePropertyChanged("ZipCode");
	}
}public string AddressUrl {
	get {
	  return this.addressUrl;
	}	

	set {
	  this.addressUrl = value;
	  this.RaisePropertyChanged("AddressUrl");
	}
}

#endregion

}