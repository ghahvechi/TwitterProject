using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace TwitterProject
{
    public class Command
    {
        public void CommandReader(List<string> command)
        {
            if (command[0].Equals("register"))
            {
                int userIndex = FindKeyPos(command, "--username");
                int passIndex = FindKeyPos(command, "--password");
                Console.WriteLine($"The username is: {command[userIndex + 1]}");
                Console.WriteLine($"The password is: {command[passIndex + 1]}");
            }
            else
            {
                Console.WriteLine("Your command is inValid, Please try one more time.");
            }
        }
        public int FindKeyPos(List<string> command, string key)
        {
            return command.IndexOf(key);
        } // For finding key and then it's value.
        public List<string> ConvertToLower(string[] command)
        {
            List<string> newCommand = new List<string>();
            for (int i = 0; i < command.Length; i++)
            {
                newCommand.Add(command[i].ToLower());
            }
            return newCommand;
        } // Convert it to lowerCase to have easier diff 

    }
}
