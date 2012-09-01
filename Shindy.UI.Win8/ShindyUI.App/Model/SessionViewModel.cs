public class SessionViewModel : ViewModelBase {

#region Fields

private int id;
private string title;
private string abstract;
private string sessionType;
private string presentationUrl;
private string demoUrl;
private ObservableCollection<Person> speakers;


#endregion

#region Constructors

public SessionViewModel(){

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
}public string Abstract {
	get {
	  return this.abstract;
	}	

	set {
	  this.abstract = value;
	  this.RaisePropertyChanged("Abstract");
	}
}public string SessionType {
	get {
	  return this.sessionType;
	}	

	set {
	  this.sessionType = value;
	  this.RaisePropertyChanged("SessionType");
	}
}public string PresentationUrl {
	get {
	  return this.presentationUrl;
	}	

	set {
	  this.presentationUrl = value;
	  this.RaisePropertyChanged("PresentationUrl");
	}
}public string DemoUrl {
	get {
	  return this.demoUrl;
	}	

	set {
	  this.demoUrl = value;
	  this.RaisePropertyChanged("DemoUrl");
	}
}public ObservableCollection<Person> Speakers {
	get {
	  return this.speakers;
	}	

	set {
	  this.speakers = value;
	  this.RaisePropertyChanged("Speakers");
	}
}

#endregion

}