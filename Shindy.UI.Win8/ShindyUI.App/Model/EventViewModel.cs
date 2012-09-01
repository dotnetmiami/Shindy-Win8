public class EventViewModel : ViewModelBase {

#region Fields

private int id;
private string title;
private string description;
private DateTime eventDateTime;
private string registrationUrl;
private bool isActive;
private Location eventLocation;
private ObservableCollection<Group> hostedGroups;
private ObservableCollection<Giveaway> giveaways;
private ObservableCollection<Session> sessions;
private ObservableCollection<Sponsor> sponsors;


#endregion

#region Constructors

public EventViewModel(){

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
}public string Title {
	get {
	  return this.title;
	}	

	set {
	  this.title = value;
	  this.RaisePropertyChanged("Title");
	}
}public string Description {
	get {
	  return this.description;
	}	

	set {
	  this.description = value;
	  this.RaisePropertyChanged("Description");
	}
}public DateTime EventDateTime {
	get {
	  return this.eventDateTime;
	}	

	set {
	  this.eventDateTime = value;
	  this.RaisePropertyChanged("EventDateTime");
	}
}public string RegistrationUrl {
	get {
	  return this.registrationUrl;
	}	

	set {
	  this.registrationUrl = value;
	  this.RaisePropertyChanged("RegistrationUrl");
	}
}public bool IsActive {
	get {
	  return this.isActive;
	}	

	set {
	  this.isActive = value;
	  this.RaisePropertyChanged("IsActive");
	}
}public Location EventLocation {
	get {
	  return this.eventLocation;
	}	

	set {
	  this.eventLocation = value;
	  this.RaisePropertyChanged("EventLocation");
	}
}public ObservableCollection<Group> HostedGroups {
	get {
	  return this.hostedGroups;
	}	

	set {
	  this.hostedGroups = value;
	  this.RaisePropertyChanged("HostedGroups");
	}
}public ObservableCollection<Giveaway> Giveaways {
	get {
	  return this.giveaways;
	}	

	set {
	  this.giveaways = value;
	  this.RaisePropertyChanged("Giveaways");
	}
}public ObservableCollection<Session> Sessions {
	get {
	  return this.sessions;
	}	

	set {
	  this.sessions = value;
	  this.RaisePropertyChanged("Sessions");
	}
}public ObservableCollection<Sponsor> Sponsor {
	get {
	  return this.sponsors;
	}	

	set {
	  this.sponsors = value;
	  this.RaisePropertyChanged("Sponsor");
	}
}

#endregion

}