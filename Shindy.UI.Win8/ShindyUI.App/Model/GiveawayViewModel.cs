public class GiveawayViewModel : ViewModelBase {

#region Fields

private int id;
private string name;
private string description;
private Person winner;
private Sponsor sponsor;


#endregion

#region Constructors

public GiveawayViewModel(){

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
}public Person Winner {
	get {
	  return this.winner;
	}	

	set {
	  this.winner = value;
	  this.RaisePropertyChanged("Winner");
	}
}public Sponsor Sponsor {
	get {
	  return this.sponsor;
	}	

	set {
	  this.sponsor = value;
	  this.RaisePropertyChanged("Sponsor");
	}
}

#endregion

}