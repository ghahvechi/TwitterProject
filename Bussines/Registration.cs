using System;
using System.Collections.Generic;
using System.Linq;


namespace TwitterProject
{
    public class Registration : IRegistration
    {
        private long
            _currentUserId;

        public Registration(long currentUserId) // if it's 0 it means that there is no log in.
        {
            _currentUserId = currentUserId;
            design = new Design();
        }
        public IDesign design { get; set; }

        public void
            RegisterUser(string username,
                string password) 
        {
            
            if (_currentUserId == 0)
            {
                IFileUtil<User> cData = new FileUtil<User>("User.json");
                List<User> users = new List<User>();
                users = cData.ReadDataFromFile();
                if (users.Any(u => u.UserName.Equals(username) && u.IsArchived == false))
                {
                    design.Error("This username is already taken.");
                    Console.WriteLine();
                }
                else
                {
                    var user = new User(username, password, DateTime.Now, "Hey there, I'm using fake twitter.", false);
                    cData.WriteDataToFile(user);
                    design.Success("You are now one of fake twitter members.");
                    Console.WriteLine();
                }
            }
            else
            {
                design.Error("You already registered and now you are logged in :)");
                Console.WriteLine();
            }
        }

        public void SetBio(string bioText) 
        {
            if (_currentUserId != 0)
            {
                IFileUtil<User> cData = new FileUtil<User>("User.json");
                cData.ReWriteDataToFile("Id", _currentUserId.ToString(), "Bio", bioText);
                design.Success("You're bio has been successfully  changed");
                Console.WriteLine();
            }
            else
            {
                design.Error("Please login firs.");
                Console.WriteLine();
            }
        }
        public void
            ChangePassword(string oldPassword,
                string newPassword) 
        {
            if (_currentUserId != 0)
            {
                IFileUtil<User> cData = new FileUtil<User>("User.json");
                var users = cData.ReadDataFromFile("Id", _currentUserId.ToString());
                if (users[0].PassWord.Equals(oldPassword))
                {
                    cData.ReWriteDataToFile("Id", _currentUserId.ToString(), "PassWord", newPassword);
                    design.Success("Your password has been successfully changed.");
                    Console.WriteLine();
                }
                else
                {
                    design.Error("Please enter the old password correctly.");
                    Console.WriteLine();
                }
            }
            else
            {
                design.Error("Please login firs.");
                Console.WriteLine();
            }
        }
    }
}