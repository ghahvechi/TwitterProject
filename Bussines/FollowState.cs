using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    public class FollowState : IFollowState
    {
        long _currentUserId;

        public FollowState(long currentUserId)
        {
            _currentUserId = currentUserId;
            design = new Design();
        }
        public IDesign design { get; set; }
        public void Follow(string username)
        {

            if (_currentUserId == 0)
            {
                design.Error("Please login first.");
                Console.WriteLine();
            }
            else
            {
                IFileUtil<User> cUserData = new FileUtil<User>("User.json");
                IFileUtil<Following> cFollowData = new FileUtil<Following>("Following.json");
                var targetUsers = cUserData.ReadDataFromFile("UserName", username);
                var follownings = cFollowData.ReadDataFromFile();
                if (targetUsers.Count() == 0)
                {
                    design.Error("This user does not exists.");
                    Console.WriteLine();
                }
                else
                {
                    if (follownings.Any(f => f.UserId == _currentUserId && f.FollowingUserId == targetUsers[0].Id && f.IsArchived == false))
                    {
                        design.Error("You are already following this user.");
                        Console.WriteLine();
                    }
                    else
                    {
                        var Following = new Following(_currentUserId, targetUsers[0].Id, DateTime.Now, false);
                        cFollowData.WriteDataToFile(Following);
                        design.Success("You are now following this user.");
                        Console.WriteLine();
                    }
                }
            }
        }

        public void UnFollow(string username)
        {

            if (_currentUserId == 0)
            {
                design.Error("Please login first.");
                Console.WriteLine();
            }
            else
            {
                IFileUtil<User> cUserData = new FileUtil<User>("User.json");
                IFileUtil<Following> cFollowData = new FileUtil<Following>("Following.json");
                var targetUsers = cUserData.ReadDataFromFile("UserName", username);
                var follownings = cFollowData.ReadDataFromFile();
                if (targetUsers.Count() == 0)
                {
                    design.Error("This user does not exists.");
                    Console.WriteLine();
                }
                else
                {
                    var following = follownings.FirstOrDefault(f => f.UserId == _currentUserId && f.FollowingUserId == targetUsers[0].Id && f.IsArchived == false);
                    if (following == null)
                    {
                        design.Error("You are not following this user.");
                        Console.WriteLine();
                    }
                    else
                    {
                        var followingId = following.Id.ToString();
                        cFollowData.ReWriteDataToFile("Id", followingId, "IsArchived", true);
                        design.Success("You are now not following this user.");
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
