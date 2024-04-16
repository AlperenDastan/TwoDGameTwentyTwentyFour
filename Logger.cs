using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDGameTwentyTwentyFour
{
    // Implementing the Singleton Pattern for the Logger to ensure that only one instance is used throughout the game and not multiple. 
    public class Logger
    {
        public static void Log(string message)
        {
            Console.WriteLine($"[LOG] {DateTime.Now}: {message}");
        }
    }

}
