public class GroupViewModel : ViewModelBase {

#region Fields

private int id;
private string name;
private string description;
private ObservableCollection<Event> events;


#endregion

#region Constructors

public GroupViewModel(){

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
}public string Description {
	get {
	  return this.description;
	}	

	set {
	  this.description = value;
	  this.RaisePropertyChanged("Description");
	}
}public ObservableCollection<Event> Events {
	get {
	  return this.events;
	}	

	set {
	  this.events = value;
	  this.RaisePropertyChanged("Events");
	}
}

#endregion

}