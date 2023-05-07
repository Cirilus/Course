using System.IO;
using System.Diagnostics;

namespace utils{
    public class Crowd{
        // each field divided by ';'
        // the structure of file: 
        // name of the crowd; location of the crowd; length of the crowd; the hash keys that in the crowd. Its divided by ',' and the first key automatically considered admin.
        static string file_path = "crowd.txt";
        // each field divided by ';'
        // the structure of file: 
        // usernmae; password;
        static string user_path = "auth.txt";

        // create the crowd and set its info to the file.
        public static string create(string username){
            while (true){
                Console.Write("Enter the name of crowd: ");
                string? name = Console.ReadLine();
                
                if (name == ""){
                    Console.WriteLine("\nThe name can't be an empty, pleasse try again\n");
                    continue;
                }

                if (name != null && name.Contains(";")){
                    Console.WriteLine("\nThe sumbol ';' doesn't allow, pleasse try again\n");
                    continue;
                } 

                Console.Write("Enter location of crowd: ");
                string? location = Console.ReadLine();

                if (location == ""){
                    Console.WriteLine("\nThe location can't be an empty, pleasse try again\n");
                    continue;
                }

                if (location != null && location.Contains(";")){
                    Console.WriteLine("\nThe sumbol ';' doesn't allow, pleasse try again\n");
                    continue;
                }

                const int crowd_length = 0;
                string hash_key = Guid.NewGuid().ToString();
                using (StreamWriter writer = new StreamWriter(file_path, true)){
                    writer.WriteLine(name + ";" + location + ";" + crowd_length + ";" + hash_key + ",");
                }
                return hash_key;
            }
            
        }


        // increase the length of the crowd and add the hash key
        public static void replace_line(int index, string hash_key){
            string[] lines = File.ReadAllLines(file_path);
            string[] newline = lines[index].Split(";");
            newline[2] = (Int32.Parse(newline[2]) + 1).ToString();
            newline[3] += hash_key + ",";
            lines[index] = newline[0] + ";" + newline[1] + ";" + newline[2] + ";" + newline[3];
            File.WriteAllLines(file_path, lines);
        }
        
        // return all hash keys of the crowd
        public static string[] get_hash_keys(int index){
            string[] file = File.ReadAllLines(file_path);
            string[] hash_keys = file[index].Split(";")[3].Split(",");
            return hash_keys;
        }
        // check if the hash key is admin
        public static bool is_admin_hash_key(int crowd_index, string hash_key){

            string admin_hash_key = get_hash_keys(crowd_index)[0];

            if (admin_hash_key == hash_key){
                return true;
            }

            return false;
        }

        // shows the admin panel of the crowds
        public static void admin_panel(string[] crowd, int index){

            while (true){
                
                System.Console.WriteLine("\n1 - show hash keys\n2 - delete the project\n3 - exit");
	            System.Console.Write("your choice: ");
                int choice = 0;
                Utils.saveEnter(ref choice);

                System.Console.WriteLine("	name		location    length of crowd");
                Console.WriteLine("|{0,15:s}|{1,15:s}| {2,16:s}|", crowd[0], crowd[1], crowd[2]);

                switch (choice){
                    case 1: 
                        string[] hash_keys = get_hash_keys(index);
                        System.Console.WriteLine("\nHash keys:");
                        foreach (string hash_key in hash_keys){
                            System.Console.WriteLine(hash_key);
                        }
                        break;

                    case 2:
                        List<string> lines = File.ReadAllLines(file_path).ToList();
                        lines.RemoveAt(index);
                        File.WriteAllLines(file_path, lines);

                        return;

                    case 3:
                        System.Environment.Exit(0);
			            break;

                    default:
                        System.Console.WriteLine("\nUnexpected choice, try please again\n");
			            break;
                }

            }

        }

        // return the crowd id by name
        public static int crowd_id(string crowd_name){
            int index = 0;
            try{
                using (StreamReader reader = new StreamReader(file_path)){
                    while (!reader.EndOfStream){
                        string[] line = reader.ReadLine().Split(";");                    
                        if (line[0] == crowd_name){
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

        // return the crowd name by id
        public static string crowd_name(int index){
            string name = File.ReadAllLines(file_path)[index].Split(";")[0];
            return name;
        }

        // add user to the crowd 
        public static string get_in_crowd(int crowd, string username){
            string hash_key = Guid.NewGuid().ToString();
            replace_line(crowd, hash_key);
            return hash_key;
        }

        // return the crowd index that user has choosen
        public static int choose_the_crowd(int max_index){
            int index = 0;

            while (true){
                System.Console.WriteLine("\nEnter the index of crowd that you want to get in, if you want to get back enter the q");
                Utils.saveEnter(ref index);
                if (index > max_index || index < -1){
                    System.Console.WriteLine("\nThe invalid index, try please again\n");
                    continue;
                }
                break;
            }
            return index;
        }

        // return the all crowds
        public static List<string[]> crowds(){
            List<string[]> crowds = new List<string[]>();
            try{
                using (StreamReader reader = new StreamReader(file_path)){
                    while (!reader.EndOfStream){
                        string[] crowd = reader.ReadLine().Split(";");
                        crowds.Add(crowd);
                    }
                }
            } catch (System.IO.FileNotFoundException){
                return crowds;
            }
            
            return crowds;
        }

    }

}


