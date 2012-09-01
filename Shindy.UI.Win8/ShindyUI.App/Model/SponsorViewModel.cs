public class SponsorViewModel : ViewModelBase {

#region Fields

private int id;
private string name;
private string imageUrl;
private string sponsorUrl;
private ObservableCollection<Event> events;
private ObservableCollection<Giveaway> giveaways;


#endregion

#region Constructors

public SponsorViewModel(){

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
}public string ImageUrl {
	get {
	  return this.imageUrl;
	}	

	set {
	  this.imageUrl = value;
	  this.RaisePropertyChanged("ImageUrl");
	}
}public string SponsorUrl {
	get {
	  return this.sponsorUrl;
	}	

	set {
	  this.sponsorUrl = value;
	  this.RaisePropertyChanged("SponsorUrl");
	}
}public ObservableCollection<Event> Events {
	get {
	  return this.events;
	}	

	set {
	  this.events = value;
	  this.RaisePropertyChanged("Events");
	}
}public ObservableCollection<Giveaway> Giveaways {
	get {
	  return this.giveaways;
	}	

	set {
	  this.giveaways = value;
	  this.RaisePropertyChanged("Giveaways");
	}
}

#endregion

}