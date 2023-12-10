using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    internal class Search : ISearch
    {
        private long _currentUserId;
        public Search(long currentUserId)
        {
            _currentUserId = currentUserId;
            design = new Design();
        }
        public IDesign design { get; set; }
        public void Searching(string input) 
        {
            
            if (_currentUserId == 0)
            {
                Console.WriteLine("Please login first.");
            }
            else
            {
                IFileUtil<User> cUserData = new FileUtil<User>("User.json");
                IFileUtil<Following> cFollowData = new FileUtil<Following>("Following.json");
                var users = cUserData.ReadDataFromFile().Where(u => u.UserName.ToLower().StartsWith(input) && u.IsArchived == false).ToList();
                if (users.Count() == 0)
                {
                    design.Error("No Match.");
                    Console.WriteLine();
                }
                else
                {
                    var followings = cFollowData.ReadDataFromFile();
                    int counter = 1;
                    foreach (var user in users)
                    {
                        long followersCount = followings.Count(f => f.FollowingUserId == user.Id && f.IsArchived == false);
                        long followingCount = followings.Count(f => f.UserId == user.Id && f.IsArchived == false);
                        bool status;
                        if (followings.Any(f => f.UserId == _currentUserId && f.FollowingUserId == user.Id && f.IsArchived == false))
                        {
                            status = true;
                        }
                        else
                        {
                            status = false;
                        }
                        design.PrintSearchResult(counter, user.UserName, followersCount, followingCount, status);
                        counter++;
                    }
                }
            }
        }
    }
}
