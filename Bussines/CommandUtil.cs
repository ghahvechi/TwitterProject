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
        private long currentUser = 0;
        private List<object> ReadCommand(List<string> command, out string message)
        {
            var resultList = new List<object>();
            message = string.Empty;
            if (command[0].Equals("Register") && loginStatus == false)
            {
                int userIndex = FindKeyPos(command, "--username");
                int passIndex = FindKeyPos(command, "--password");
                if (Regex.IsMatch(command[userIndex + 1], @"^[A-Za-z_]+$") && Regex.IsMatch(command[passIndex + 1], @"^\S{3,10000}$") && userIndex != -1 && passIndex != -1 && command.Count == 5)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[userIndex + 1]);
                    resultList.Add(command[passIndex + 1]);
                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("Login"))
            {
                int userIndex = FindKeyPos(command, "--username");
                int passIndex = FindKeyPos(command, "--password");
                if (Regex.IsMatch(command[userIndex + 1], @"^[A-Za-z_]+$") && Regex.IsMatch(command[passIndex + 1], @"^\S{3,10000}$") && userIndex != -1 && passIndex != -1 && command.Count == 5)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[userIndex + 1]);
                    resultList.Add(command[passIndex + 1]);
                    loginStatus = true;
                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("Search") && loginStatus == true)
            {
                int userIndex = FindKeyPos(command, "--username");
                if (Regex.IsMatch(command[userIndex + 1], @"^[A-Za-z_]+$") && userIndex != -1 && command.Count == 3)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[userIndex + 1]);
                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("Select") && loginStatus == true)
            {
                var userIndex = FindKeyPos(command, "--username");
                var tweetIndex = FindKeyPos(command, "--tweet");
                if (userIndex == 1 && tweetIndex == -1 && command.Count == 3)
                {
                    if (Regex.IsMatch(command[userIndex + 1], @"^[A-Za-z_]+$"))
                    {
                        resultList.Add(command[0]);
                        resultList.Add(command[userIndex + 1]);
                        resultList.Add("username");
                    }
                    else
                    {
                        message = "input parameter is invalid";
                    }
                }
                else if (userIndex == -1 && tweetIndex == 1)
                {
                    if (Regex.IsMatch(command[tweetIndex + 1], @"^\d+$") && command.Count == 3)
                    {
                        resultList.Add(command[0]);
                        resultList.Add(command[tweetIndex + 1]);
                        resultList.Add("tweet");
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
                int userIndex = FindKeyPos(command, "--username");

                if (Regex.IsMatch(command[userIndex + 1], @"^[A-Za-z_]+$") && userIndex != -1 && command.Count == 3)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[userIndex + 1]);
                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("Unfollow") && loginStatus == true)
            {
                int userIndex = FindKeyPos(command, "--username");
                if (Regex.IsMatch(command[userIndex + 1], @"^[A-Za-z_]+$") && userIndex != -1 && command.Count == 3)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[userIndex + 1]);
                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("TimeLine") && loginStatus == true)
            {
                int lastIndex = FindKeyPos(command, "--last");
                if (lastIndex != -1)
                {
                    if (Regex.IsMatch(command[lastIndex + 1], @"^\d+$") && command.Count == 3)
                    {
                        resultList.Add(command[0]);
                        resultList.Add(command[lastIndex + 1]);
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
                    }
                    else
                    {
                        message = "input parameter is invalid";
                    }
                }
            }
            else if (command[0].Equals("Like") && loginStatus == true)
            {
                int idIndex = FindKeyPos(command, "--id");
                if (Regex.IsMatch(command[idIndex + 1], @"^\d+$") && idIndex != -1 && command.Count == 3)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[idIndex + 1]);
                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("Dislike") && loginStatus == true)
            {
                int idIndex = FindKeyPos(command, "--id");
                if (Regex.IsMatch(command[idIndex + 1], @"^\d+$") && idIndex != -1 && command.Count == 3)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[idIndex + 1]);
                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("Comment") && loginStatus == true)
            {
                var idIndex = FindKeyPos(command, "--id");
                var textIndex = FindKeyPos(command, "--text");
                if (Regex.IsMatch(command[idIndex + 1], @"^\d+$") && idIndex != -1 && textIndex != -1)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[idIndex + 1]);
                    string commentText = "";
                    if (idIndex == command.Count - 2)
                    {
                        commentText += command[textIndex + 1].Split('"')[1] + " ";
                        for (int i = textIndex + 2; i < idIndex - 2; i++)
                        {
                            commentText += command[i] + " ";
                        }
                        commentText += command[idIndex - 1].Split('"')[0];
                    }
                    else
                    {
                        commentText += command[textIndex + 1].Split('"')[1] + " ";
                        for (int i = textIndex + 2; i < command.Count - 1; i++)
                        {
                            commentText += command[i] + " ";
                        }
                        commentText += command[command.Count - 1].Split('"')[0];
                    }
                    resultList.Add(commentText);
                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("SetBio") && loginStatus == true)
            {
                var textIndex = FindKeyPos(command, "--text");
                if (textIndex != -1 && command.Count == 3)
                {
                    resultList.Add(command[0]);
                    string bioText = command[textIndex + 1].Split('"')[1] + " ";
                    for (int i = textIndex + 2; i < command.Count - 1; i++)
                    {
                        bioText += command[i] + " ";
                    }
                    bioText += command[command.Count - 1].Split('"')[0];
                    resultList.Add(bioText);
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
                if (Regex.IsMatch(command[oldIndex + 1], @"^\S{3,10000}$") && oldIndex != -1 && Regex.IsMatch(command[newIndex + 1], @"^\S{3,10000}$") && newIndex != -1 && !command[oldIndex + 1].Equals(command[newIndex + 1]) && command.Count == 5)
                {
                    resultList.Add(command[0]);
                    resultList.Add(command[oldIndex + 1]);
                    resultList.Add(command[newIndex + 1]);
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

                }
                else
                {
                    message = "input parameter is invalid";
                }
            }
            else if (command[0].Equals("Tweet") && loginStatus == true)
            {
                var textIndex = FindKeyPos(command, "--text");
                if (textIndex != -1)
                {
                    resultList.Add(command[0]);
                    string tweetText = command[textIndex + 1].Split('"')[1] + " ";
                    for (int i = textIndex + 2; i < command.Count - 1; i++)
                    {
                        tweetText += command[i] + " ";
                    }
                    tweetText += command[command.Count - 1].Split('"')[0];
                    resultList.Add(tweetText);
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
        public string ExecuteCommand()
        {
            IDesign design = new Design();
            var inputCommand = Console.ReadLine().Trim();
            var inputCommandList = new List<string>();
            inputCommandList = inputCommand.Split(' ').ToList();
            var message = string.Empty;
            var commandInfo = ReadCommand(inputCommandList, out message);
            if (commandInfo.Any())
            {
                ITweetUtil tweetUtil = new TweetUtil(currentUser);
                IRegistration register = new Registration(currentUser);
                ISearch search = new Search(currentUser);
                IFollowState followState = new FollowState(currentUser);
                IProfile profile = new Profile(currentUser);


                var command = commandInfo[0];
                if (command.Equals("Register"))
                {
                    register.RegisterUser(commandInfo[1].ToString(), commandInfo[2].ToString());
                }
                else if (command.Equals("Login"))
                {
                    ISession session = new Session();
                    currentUser =  session.Login(commandInfo[1].ToString(), commandInfo[2].ToString());
                    if (currentUser != 0)
                    {
                        loginStatus = true;
                    }
                }
                else if (command.Equals("Search"))
                {
                    search.Searching(commandInfo[1].ToString().ToLower());
                }
                else if (command.Equals("Select"))
                {
                    if (commandInfo[2].ToString().Equals("username"))
                    {
                        profile.ShowProfile(commandInfo[1].ToString());
                    }
                    else if (commandInfo[2].ToString().Equals("tweet"))
                    {
                        tweetUtil.GetComment(Convert.ToInt64(commandInfo[1]));
                    }
                }
                else if (command.Equals("Follow"))
                {
                    followState.Follow(commandInfo[1].ToString());
                }
                else if (command.Equals("Unfollow"))
                {
                    followState.UnFollow(commandInfo[1].ToString());
                }
                else if (command.Equals("TimeLine"))
                {
                    if (commandInfo.Count == 1)
                    {
                        tweetUtil.GetTimeLine();
                    }
                    else if (commandInfo.Count == 2)
                    {
                        tweetUtil.GetTimeLine(Convert.ToInt32(commandInfo[1]));
                    }
                }
                else if (command.Equals("Like"))
                {
                    tweetUtil.AddLike(Convert.ToInt64(commandInfo[1]));
                }
                else if (command.Equals("Dislike"))
                {
                    tweetUtil.DelLike(Convert.ToInt64(commandInfo[1]));
                }
                else if (command.Equals("Comment"))
                {
                    tweetUtil.AddCommnet(Convert.ToInt64(commandInfo[1]), commandInfo[2].ToString());
                }
                else if (command.Equals("SetBio"))
                {
                    register.SetBio(commandInfo[1].ToString());
                }
                else if (command.Equals("ChangePassword"))
                {
                    register.ChangePassword(commandInfo[1].ToString(), commandInfo[2].ToString());
                }
                else if (command.Equals("ShowProfile"))
                {
                    if (commandInfo.Count == 1)
                    {
                        profile.ShowProfile();
                    }
                    else if (commandInfo.Count == 2)
                    {
                        profile.ShowProfile(Convert.ToInt32(commandInfo[1]));
                    }
                }
                else if (command.Equals("Tweet"))
                {
                    tweetUtil.AddTweet(commandInfo[1].ToString());
                }
                else if (command.Equals("Logout"))
                {
                    currentUser = 0;
                    loginStatus = false;
                    design.Success("See you soon.");
                    Console.WriteLine();
                }
            }
            else
            {
                design.Error(message);
                Console.WriteLine();
            }
            return inputCommand;
        }

        private int FindKeyPos(List<string> command, string key)
        {
            return command.IndexOf(key);
        } 
    }
}
