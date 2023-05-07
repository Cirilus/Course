using System.IO;


namespace utils{
    public class Utils{
        // the safety enter of the choice
        public static void saveEnter(ref int choice){
            try{
                string? input = Console.ReadLine();
                if (input == "q"){
                    choice = -1;
                }
                choice = Int32.Parse(input);
            } 
            catch (FormatException){
                System.Console.WriteLine("\nEnter please a number\n");
                choice = -1;
            }
            catch {
                System.Console.WriteLine("\nSomething going wrong, please try again\n");
                choice = -1;
            }
        }
    }
}