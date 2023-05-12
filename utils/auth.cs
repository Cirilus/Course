using System.IO;


namespace utils{
    public class Auth{
        // each field divided by ';'
        // the structure of file: 
        // usernmae; password;
        static string file_path = "auth.txt";


        /// <summary>
        /// Retrieves the user ID based on the provided username.
        /// </summary>
        /// <param name="username">The username for which to find the user ID.</param>
        /// <returns>
        /// Returns the user ID as an integer if the username is found. Returns -2 if the username is "guest".
        /// Returns -1 if the username is not found or if a FileNotFoundException occurs.
        /// </returns>
        public static int get_user_id(string username){
            int index = 0;
            if (username == "guest"){
                return -2;
            }
            try{
                using (StreamReader reader = new StreamReader(file_path)){
                    while (!reader.EndOfStream){
                        string[] line = reader.ReadLine().Split(";");                    
                        if (line[0] == username){
                            return index;
                        }
                        index += 1;
                    }
                }       
            } catch (FileNotFoundException){
                return -1;
            }
            
            return -1;
        }

        /// <summary>
        /// Retrieves the username based on the provided index.
        /// </summary>
        /// <param name="index">The index of the username in the file.</param>
        /// <returns>Returns the username as a string.</returns>
        public static string get_username(int index){
            string username = File.ReadAllLines(file_path)[index].Split(";")[0];

            return username;
        }


        /// <summary>
        /// Handles the user registration process.
        /// </summary>
        /// <returns>Returns the registered username as a string.</returns>
        public static string registration(){
            while (true){
                try{
                    Console.Write("Enter your username: ");
                    string? username = Console.ReadLine();
                    
                    if (username == ""){
                        Console.WriteLine("The username can't be an empty, pleasse try again");
                        continue;
                    }
                    
                    int index = get_user_id(username);

                    if (index >= 0 || index == -2){
                        Console.WriteLine("The username is already exist, try please another");
                        continue;
                    }

                    if (username != null && (username.Contains(";") || username.Contains(','))){
                        Console.WriteLine("The sumbol ';' and ',' doesn't allow, pleasse try again");
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
                        writer.WriteLine(username + ";" + password + ";");
                    }
                    
                    return username;
                }
                catch {
                    Console.WriteLine("Something going wrong, try please again");
                    continue;
                }
            }
        }


        /// <summary>
        /// Handles the user login process.
        /// </summary>
        /// <returns>Returns the logged-in username as a string if successful, or an empty string if unsuccessful.</returns>
        public static string login(){
            while (true){
                try{
                    Console.Write("Enter your username: ");
                    string? username = Console.ReadLine();
                    
                    if (username == ""){
                        Console.WriteLine("The username can't be an empty, pleasse try again");
                        continue;
                    }

                    if (username != null && (username.Contains(";") || username.Contains(','))){
                        Console.WriteLine("The sumbol ';' and ',' doesn't allow, pleasse try again");
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
                    try{
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
                    }catch (FileNotFoundException){
                        return "";
                    }

                    return "";
                }
                catch (Exception e) {
                    Console.WriteLine("Something going wrong, try please again" + e);
                    continue;
                }
        }
    }
}
}


