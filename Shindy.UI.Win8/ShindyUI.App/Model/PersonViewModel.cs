public class PersonViewModel : ViewModelBase {

#region Fields

private int id;
private string socialId;
private string firstName;
private string lastName;
private string email;
private string personUrl;
private string pictureUrl;
private string twitterName;
private string bio;
private Address address;
private ObservableCollection<Role> roles;


#endregion

#region Constructors

public PersonViewModel(){

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
}public string SocialId {
	get {
	  return this.socialId;
	}	

	set {
	  this.socialId = value;
	  this.RaisePropertyChanged("SocialId");
	}
}public string FirstName {
	get {
	  return this.firstName;
	}	

	set {
	  this.firstName = value;
	  this.RaisePropertyChanged("FirstName");
	}
}public string LastName {
	get {
	  return this.lastName;
	}	

	set {
	  this.lastName = value;
	  this.RaisePropertyChanged("LastName");
	}
}public string Email {
	get {
	  return this.email;
	}	

	set {
	  this.email = value;
	  this.RaisePropertyChanged("Email");
	}
}public string PersonUrl {
	get {
	  return this.personUrl;
	}	

	set {
	  this.personUrl = value;
	  this.RaisePropertyChanged("PersonUrl");
	}
}public string PictureUrl {
	get {
	  return this.pictureUrl;
	}	

	set {
	  this.pictureUrl = value;
	  this.RaisePropertyChanged("PictureUrl");
	}
}public string TwitterName {
	get {
	  return this.twitterName;
	}	

	set {
	  this.twitterName = value;
	  this.RaisePropertyChanged("TwitterName");
	}
}public string Bio {
	get {
	  return this.bio;
	}	

	set {
	  this.bio = value;
	  this.RaisePropertyChanged("Bio");
	}
}public Address Address {
	get {
	  return this.address;
	}	

	set {
	  this.address = value;
	  this.RaisePropertyChanged("Address");
	}
}public ObservableCollection<Role> Roles {
	get {
	  return this.roles;
	}	

	set {
	  this.roles = value;
	  this.RaisePropertyChanged("Roles");
	}
}

#endregion

}