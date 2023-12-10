using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace TwitterProject
{
    public class CommandUtil
    {
        public bool loginStatus { get; set; } = false;
        public  List<object> ReadCommand(List<string> command, out string message)
        {
            var resultList = new List<object>();
            message = string.Empty;
            if (command[0].Equals("Register") && loginStatus == false)
            {
                //message = "command is valid";
                int userIndex = FindKeyPos(command, "--username");
                int passIndex = FindKeyPos(command, "--password");
                if (Regex.IsMatch(command[userIndex + 1], @"^[A-Za-z_]+$") && Regex.IsMatch(command[passIndex + 1], @"^\S{3,10000}$") && userIndex != -1 && passIndex != -1 && command.Count == 5)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[userIndex + 1]);
                    resultList.Add(command[passIndex + 1]);
                    message = "input parameter is valid";
                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("Login"))
            {
                //message = "command is valid";
                int userIndex = FindKeyPos(command, "--username");
                int passIndex = FindKeyPos(command, "--password");
                if (Regex.IsMatch(command[userIndex + 1], @"^[A-Za-z_]+$") && Regex.IsMatch(command[passIndex + 1], @"^\S{3,10000}$") && userIndex != -1 && passIndex != -1 && command.Count == 5)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[userIndex + 1]);
                    resultList.Add(command[passIndex + 1]);
                    loginStatus = true;
                    message = "input parameter is valid";
                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("Search") && loginStatus == true)
            {
                //message = "command is valid";
                int userIndex = FindKeyPos(command, "--username");
                if (Regex.IsMatch(command[userIndex + 1], @"^[A-Za-z_]+$") && userIndex != -1 && command.Count == 3)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[userIndex + 1]);
                    message = "input parameter is valid";
                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("Select") && loginStatus == true)
            {
                //message = "command is valid";
                var userIndex = FindKeyPos(command, "--username");
                var tweetIndex = FindKeyPos(command, "--tweet");
                if (userIndex != -1 && tweetIndex == 1 && command.Count == 3)
                {
                    if (Regex.IsMatch(command[userIndex + 1], @"^[A-Za-z_]+$"))
                    {
                        resultList.Add(command[0]);
                        resultList.Add(command[userIndex + 1]);
                        message = "input parameter is valid";
                    }
                    else
                    {
                        message = "input parameter is invalid";
                    }
                }
                else if (userIndex == -1 && tweetIndex != 1)
                {
                    if (Regex.IsMatch(command[userIndex + 1], @"^\d+$") && command.Count == 3)
                    {
                        resultList.Add(command[0]);
                        resultList.Add(command[tweetIndex + 1]);
                        message = "input parameter is valid";
                    }
                    else
                    {
                        message = "input parameter is invalid";
                    }
                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("Follow") && loginStatus == true)
            {
                //message = "command is valid";
                int userIndex = FindKeyPos(command, "--username");

                if (Regex.IsMatch(command[userIndex + 1], @"^[A-Za-z_]+$") && userIndex != -1 && command.Count == 3)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[userIndex + 1]);
                    message = "input parameter is valid";
                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("Unfollow") && loginStatus == true)
            {
                //message = "command is valid";
                int userIndex = FindKeyPos(command, "--username");
                if (Regex.IsMatch(command[userIndex + 1], @"^[A-Za-z_]+$") && userIndex != -1 && command.Count == 3)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[userIndex + 1]);
                    message = "input parameter is valid";
                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("TimeLine") && loginStatus == true)
            {
                //message = "command is valid";
                int lastIndex = FindKeyPos(command, "--last");
                if (lastIndex != -1)
                {
                    if (Regex.IsMatch(command[lastIndex + 1], @"^\d+$") && command.Count == 3)
                    {
                        resultList.Add(command[0]);
                        resultList.Add(command[lastIndex + 1]);
                        message = "input parameter is valid";
                    }
                    else
                    {
                        message = "input parameter is invalid";
                    }
                }
                else
                {
                    if (command.Count == 1)
                    {
                        resultList.Add(command[0]);
                        message = "input parameter is valid";
                    }
                    else
                    {
                        message = "input parameter is invalid";
                    }
                }
            }
            else if (command[0].Equals("Like") && loginStatus == true)
            {
                //message = "command is valid";
                int idIndex = FindKeyPos(command, "--id");
                if (Regex.IsMatch(command[idIndex + 1], @"^\d+$") && idIndex != -1 && command.Count == 3)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[idIndex + 1]);
                    message = "input parameter is valid";
                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("Comment") && loginStatus == true)
            {
                //message = "command is valid";
                var idIndex = FindKeyPos(command, "--id");
                var textIndex = FindKeyPos(command, "--text");
                if (Regex.IsMatch(command[idIndex + 1], @"^\d+$") && idIndex != -1 && textIndex != -1 && command.Count == 5)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[idIndex + 1]);
                    resultList.Add(command[textIndex + 1]);
                    message = "input parameter is valid";
                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("SetBio") && loginStatus == true)
            {
                //message = "command is valid";
                var textIndex = FindKeyPos(command, "--text");
                if (textIndex != -1 && command.Count == 3)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[textIndex + 1]);
                    message = "input parameter is valid";
                }
                else
                {
                    message = "input parameter is invalid";

                }
            }
            else if (command[0].Equals("ChangePassword") && loginStatus == true)
            {
                int oldIndex = FindKeyPos(command, "--old");
                int newIndex = FindKeyPos(command, "--new");
                if (Regex.IsMatch(command[oldIndex + 1], @"^\S{3,10000}$") && oldIndex != -1 && Regex.IsMatch(command[newIndex + 1], @"^\S{3,10000}$") && newIndex != -1 && command[oldIndex + 1].Equals(command[newIndex + 1]) && command.Count == 5)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[oldIndex + 1]);
                    resultList.Add(command[newIndex + 1]);
                    message = "input parameter is valid";
                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("ShowProfile") && loginStatus == true)
            {
                int lastIndex = FindKeyPos(command, "--last");
                if (lastIndex != -1)
                {
                    if (Regex.IsMatch(command[lastIndex + 1], @"^\d+$") && command.Count == 3)
                    {
                        resultList.Add(command[0]);
                        resultList.Add(command[lastIndex + 1]);
                        message = "input parameter is valid";
                    }
                    else
                    {
                        message = "input parameter is invalid";
                    }
                }
                else
                {
                    if (command.Count == 1)
                    {
                        resultList.Add(command[0]);
                        message = "input parameter is valid";
                    }
                    else
                    {
                        message = "input parameter is invalid";
                    }
                }
            }
            else if (command[0].Equals("Logout") && loginStatus == true)
            {
                if (command.Count == 1)
                {
                    resultList.Add(command[0]);
                    loginStatus = false;
                    message = "input parameter is valid";

                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else
            {
                Console.WriteLine("command is invalid");
            }
            return resultList;

        }
        public  int FindKeyPos(List<string> command, string key)
        {
            return command.IndexOf(key);
        } // For finding key and then it's value.
        public  List<string> ConvertToLower(string[] command)
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
