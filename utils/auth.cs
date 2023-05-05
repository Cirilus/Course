using System.IO;


namespace utils{
    public class Auth{
        static string file_path = "auth.txt";
        public static string registration(){
            while (true){
                try{
                    Console.Write("Enter your username: ");
                    string? username = Console.ReadLine();
                    
                    if (username == ""){
                        Console.WriteLine("The username can't be an empty, pleasse try again");
                        continue;
                    }

                    if (username != null && username.Contains(";")){
                        Console.WriteLine("The sumbol ';' doesn't allow, pleasse try again");
                        continue;
                    } 

                    Console.Write("Enter your password: ");
                    string? password = Console.ReadLine();

                    if (password == ""){
                        Console.WriteLine("The username can't be an empty, pleasse try again");
                        continue;
                    }

                    if (password != null && password.Contains(";")){
                        Console.WriteLine("The sumbol ';' doesn't allow, pleasse try again");
                        continue;
                    }

                    
                    using (StreamWriter writer = new StreamWriter(file_path, true)){
                            writer.WriteLine(username + ";" + password);
                    }
                    
                    return username;
                }
                catch {
                    Console.WriteLine("Something going wrong, try please again");
                }
            }
        }
        public static string login(){
            while (true){
                try{
                    Console.Write("Enter your username: ");
                    string? username = Console.ReadLine();
                    
                    if (username == ""){
                        Console.WriteLine("The username can't be an empty, pleasse try again");
                        continue;
                    }

                    if (username != null && username.Contains(";")){
                        Console.WriteLine("The sumbol ';' doesn't allow, pleasse try again");
                        continue;
                    } 

                    Console.Write("Enter your password: ");
                    string? password = Console.ReadLine();

                    if (password == ""){
                        Console.WriteLine("The username can't be an empty, pleasse try again");
                        continue;
                    }

                    if (password != null && password.Contains(";")){
                        Console.WriteLine("The sumbol ';' doesn't allow, pleasse try again");
                        continue;
                    }

                    using (StreamReader reader = new StreamReader(file_path)){
                        while (!reader.EndOfStream){
                            string[] line = reader.ReadLine().Split(";");
                            string u = line[0];
                            string p = line[1];
                            if (u == username && p == password){
                                return username;
                            }

                        }
                    }

                    return "";
                }
                catch (Exception e) {
                    Console.WriteLine("Something going wrong, try please again" + e);
                }
        }
    }
}
}


