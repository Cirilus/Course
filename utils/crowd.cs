using System.IO;
using System.Diagnostics;

namespace utils{
    public class Crowd{
        static string file_path = "crowd.txt";
        static string user_path = "auth.txt";
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

        public static void replace_line(int index, string hash_key){
            string[] lines = File.ReadAllLines(file_path);
            string[] newline = lines[index].Split(";");
            newline[2] = (Int32.Parse(newline[2]) + 1).ToString();
            newline[3] += hash_key + ",";
            lines[index] = newline[0] + ";" + newline[1] + ";" + newline[2] + ";" + newline[3];
            File.WriteAllLines(file_path, lines);
        }
        

        public static string[] get_hash_keys(int index){
            string[] file = File.ReadAllLines(file_path);
            string[] hash_keys = file[index].Split(";")[3].Split(",");
            return hash_keys;
        }

        public static bool is_admin_hash_key(int crowd_index, string hash_key){

            string admin_hash_key = get_hash_keys(crowd_index)[0];

            if (admin_hash_key == hash_key){
                return true;
            }

            return false;
        }


        public static void admin_panel(string[] crowd, int index){

            while (true){
                
                System.Console.WriteLine("\n1 - show hash keys\n2 - delete the project\n3 - exit");
	            System.Console.Write("your choice: ");
	            int choice = Utils.saveEnter();

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

        public static int crowd_id(string crowd_name){
            int index = 0;
            using (StreamReader reader = new StreamReader(file_path)){
                while (!reader.EndOfStream){
                    string[] line = reader.ReadLine().Split(";");                    
                    if (line[0] == crowd_name){
                        return index;
                    }
                    index += 1;
                }
            }
            return -1;
        }

        public static string crowd_name(int index){
            string name = File.ReadAllLines(file_path)[index].Split(";")[0];
            return name;
        }


        public static void add_hash_key(string hash_key, string crowd_name = "", int index = -1){
            if (index == -1){
                index = crowd_id(crowd_name);
            }
            string[] crowd_file = File.ReadAllLines(file_path);
            crowd_file[index] += hash_key + ",";
            File.WriteAllLines(user_path, crowd_file);
        }

        public static string get_in_crowd(int crowd, string username){
            string hash_key = Guid.NewGuid().ToString();
            replace_line(crowd, hash_key);
            return hash_key;
        }

        public static int choose_the_crowd(int max_index){
            int index;

            while (true){
                System.Console.WriteLine("\nEnter the index of crowd that you want to get in, if you want to get back enter the q");
                index = Utils.saveEnter();
                if (index > max_index || index < -1){
                    System.Console.WriteLine("\nThe invalid index, try please again\n");
                    continue;
                }
                break;
            }
            return index;
        }


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

        // public static List<string[]> show_my_crowds(string username){
            
        // }

    }

}


