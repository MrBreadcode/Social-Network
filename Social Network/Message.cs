using System;
using System.IO;

namespace Social_Networks
{
    struct Message
    {
        public static void ExceptionHandler(Exception exception)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(exception.Message);
            Console.ForegroundColor = color;

            // Open a file for appending exception information to an existing file named "Logs.txt". 
            using (StreamWriter outputFile = new StreamWriter(Path.Combine("Logs.txt"), true)) 
            {
                outputFile.WriteLine("Date : " + DateTime.Now);
                outputFile.WriteLine("Message : " + exception.Message);
                outputFile.WriteLine("Source : " + exception.Source);
                outputFile.WriteLine("Target Site : " + exception.TargetSite);
                outputFile.WriteLine("Stack Trace :\n" + exception.StackTrace);
                outputFile.WriteLine("<--------------------------------------------------------->");
                outputFile.WriteLine("<--------------------------------------------------------->");
            }
        }

        public static void SuccessOperationHandler(string message)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }

        public static void EventHandler(UserEventsArgs info)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(info.Message);
            Console.ForegroundColor = color;
        }
    }
}