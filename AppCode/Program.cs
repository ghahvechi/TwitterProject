using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var command = new CommandUtil();
            while (!command.ExecuteCommand().Equals("Exit"))
            {
                Console.WriteLine("\n\nPlease Enter any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
