using utils;



int choice = -1;
// if the user authorized in system
bool auth = false;
// if the user guest that logged without username and password. Guest cannot to create the crowd
bool guest = false;
string username = "guest";

while (true){
	System.Console.WriteLine("1 - registration \n2 - login \n3 - login without password and username \n4 - exit");
	System.Console.Write("your choice:");
	Utils.saveEnter(ref choice);
	// the Authorisation panel that conatins:
	// the registration panel
	// the login panel
	// the login as a guest
	// exit from application
	switch (choice){
		case 1:
			username = Auth.registration();
			auth = true;
			break;
		case 2:
			string cred = Auth.login();
			if (cred == ""){
				System.Console.WriteLine("\nThe error credentials\n");
			} else{
				username = cred;
				auth = true;
			}
			break;
		case 3:
			auth = true;
			guest = true;
			break;
		case 4:
			System.Environment.Exit(0);
			break;
		default:
			System.Console.WriteLine("\nUnexpected choice, try please again\n");
			break;
	}

	if (auth == true){
		break;
	}

}

System.Console.WriteLine("\n");

do{
	System.Console.WriteLine("\n1 - list of all crowds\n2 - create the crowd\n3 - go to the admin panel of crowd\n4 - exit");
	System.Console.Write("your choice " + username + " :");
	Utils.saveEnter(ref choice);
	if (choice == -1){
		continue;
	}

	// the crowd panel that contains:
	// the list of the all allowed crowds and in this panel you also can enter one of these crowd.
	// creating the new crowd
	// enter to the admin panel of the crowd
	// exit from application
	switch (choice){
		case 1:
			List<string[]> crowds = Crowd.crowds();
			if (crowds.Count <= 0){
				System.Console.WriteLine("\n There are no avaliable crowds, create it first\n");
				break;
			}
			System.Console.WriteLine("	name		location    length of crowd");
			for (int i = 0; i < crowds.Count; ++i){
				Console.WriteLine(i + ". " + "|{0,15:s}|{1,15:s}| {2,16:s}|", crowds[i][0], crowds[i][1], crowds[i][2]);
			}
			int index = Crowd.choose_the_crowd(crowds.Count - 1);
			if (index == -1){
				break;
			}
            string hash_key = Crowd.get_in_crowd(index, username);
			System.Console.WriteLine("\nYour hash key = {0:s}", hash_key);
			break;
		case 2:
			if (guest){
				System.Console.WriteLine("\nOnly registered user can create the crowd\n");
				break;
			}
			string admin_hash_key = Crowd.create(username);
			System.Console.WriteLine("\nYour admin hash key = {0:s}", admin_hash_key);
			break;
		case 3:
			List<string[]> admin_crowds = Crowd.crowds();
			if (admin_crowds.Count <= 0){
				System.Console.WriteLine("\n There are no avaliable crows, create if first\n");
				break;
			}
			System.Console.WriteLine("	name		location    length of crowd");
			for (int i = 0; i < admin_crowds.Count; ++i){
				Console.WriteLine(i + ". " + "|{0,15:s}|{1,15:s}| {2,16:s}|", admin_crowds[i][0], admin_crowds[i][1], admin_crowds[i][2]);
			}
			int admin_index = Crowd.choose_the_crowd(admin_crowds.Count - 1);
			if (admin_index == -1){
				break;
			}

			System.Console.WriteLine("Enter the hash key of the {0:s} crowd", admin_crowds[admin_index][0]);
			string a_hash_key = Console.ReadLine();
			if (!Crowd.is_admin_hash_key(admin_index, a_hash_key)){
				System.Console.WriteLine("\nThe hash key isn't admin\n");
				break;
			}
			Crowd.admin_panel(admin_crowds[admin_index], admin_index);
			break;
		case 4:
			System.Environment.Exit(0);
			break;
		default:
			System.Console.WriteLine("\nUnexpected choice, try please again\n");
			break;
	}
}while (true);







