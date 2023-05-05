using System.IO;


namespace utils{
    public class Utils{
        public static int saveEnter(){
            int choice;
            try{
                string? input = Console.ReadLine();
                if (input == "q"){
                    return -1;
                }
                choice = Int32.Parse(input);
            } 
            catch (FormatException){
                System.Console.WriteLine("\nEnter please a number\n");
                return -1;
            }
            catch {
                System.Console.WriteLine("\nSomething going wrong, please try again\n");
                return -1;
            }
            return choice;
        }
    }
}