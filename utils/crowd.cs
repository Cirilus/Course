using System.IO;
using System.Diagnostics;

namespace utils{
    public class Crowd{
        static string file_path = "crowd.txt";
        public static void create(){
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

                using (StreamWriter writer = new StreamWriter(file_path, true)){
                    writer.WriteLine(name + ";" + location + ";" + crowd_length);
                }
                break;   
            }
            
        }

        public static void replace_line(int index){
            string[] lines = File.ReadAllLines(file_path);
            string[] newline = lines[index].Split(";");
            newline[2] = (Int32.Parse(newline[2]) + 1).ToString();
            lines[index] = newline[0] + ";" + newline[1] + ";" + newline[2];
            File.WriteAllLines(file_path, lines);
        }

        public static string get_in_crowd(int crowd){
            string hash_key = Guid.NewGuid().ToString();
            replace_line(crowd);
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
            using (StreamReader reader = new StreamReader(file_path)){
                while (!reader.EndOfStream){
                    string[] crowd = reader.ReadLine().Split(";");
                    crowds.Add(crowd);
                }
            }
            return crowds;
        }

    }

}


