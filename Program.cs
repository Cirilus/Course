using utils;



int choice;
bool auth = false;
bool guest = false;
string username = "guest";

while (true){
	System.Console.WriteLine("1 - registration \n2 - login \n3 - login without password and username \n4 - exit");
	System.Console.Write("your choice:");
	choice = Utils.saveEnter();
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

while (true){
	System.Console.WriteLine("\n1 - list of all crowds\n2 - create the crowd \n3 - exit");
	System.Console.Write("your choice " + username + " :");
	choice = Utils.saveEnter();
	if (choice == -1){
		continue;
	}
	switch (choice){
		case 1:
			List<string[]> crowds = Crowd.crowds();
			System.Console.WriteLine("	name		location    length of crowd");
			for (int i = 0; i < crowds.Count; ++i){
				Console.WriteLine(i + ". " + "|{0,15:s}|{1,15:s}| {2,16:s}|", crowds[i][0], crowds[i][1], crowds[i][2]);
			}
			int index = Crowd.choose_the_crowd(crowds.Count - 1);
			if (index == -1){
				break;
			}
            string hash_key = Crowd.get_in_crowd(index);

			break;
		case 2:
			Crowd.create();
			break;
		case 3:
			System.Environment.Exit(0);
			break;
		default:
			System.Console.WriteLine("\nUnexpected choice, try please again\n");
			break;
	}
}







