public class LocationViewModel : ViewModelBase {

#region Fields

private int id;
private string name;
private string locationUrl;
private ObservableCollection<Event> hostedEvents;
private AddressViewModel address;


#endregion

#region Constructors

public LocationViewModel(){

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
}public string Name {
	get {
	  return this.name;
	}	

	set {
	  this.name = value;
	  this.RaisePropertyChanged("Name");
	}
}public string LocationUrl {
	get {
	  return this.locationUrl;
	}	

	set {
	  this.locationUrl = value;
	  this.RaisePropertyChanged("LocationUrl");
	}
}public ObservableCollection<Event> HostedEvents {
	get {
	  return this.hostedEvents;
	}	

	set {
	  this.hostedEvents = value;
	  this.RaisePropertyChanged("HostedEvents");
	}
}public AddressViewModel Address {
	get {
	  return this.address;
	}	

	set {
	  this.address = value;
	  this.RaisePropertyChanged("Address");
	}
}

#endregion

}