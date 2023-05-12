using System.IO;


namespace utils{
    
    public class Utils{
        /// <summary>
        /// The saveEnter function reads a user's input from the console, validates it,
        /// and updates the value of the choice parameter. If the user enters "q", the
        /// function sets the choice to -1. If the user enters a valid integer, the
        /// function updates the choice with that integer. If the input is not a valid
        /// integer or an exception occurs, the function sets the choice to -1 and
        /// displays an appropriate error message.
        /// </summary>
        /// <param name="choice">A reference to an integer variable that will be updated with the user's input.</param>
        public static void saveEnter(ref int choice, params string[] additionaltext){
            if (additionaltext.Length != 0){
                System.Console.WriteLine(additionaltext);
            }

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