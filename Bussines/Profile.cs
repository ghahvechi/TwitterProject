using System;
using System.Linq;


namespace TwitterProject
{
    internal class Profile : IProfile
    {
        private long _currentUserId;
        public Profile(long currentUserId)
        {
            _currentUserId = currentUserId;
            design = new Design();
        }
        public IDesign design { get; set; }

        public void ShowProfile() 
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
                

                var followings = cFollowData.ReadDataFromFile();
                var users = cUserData.ReadDataFromFile("Id", _currentUserId.ToString());
                long followersCount = followings.Count(f => f.FollowingUserId == users[0].Id && f.IsArchived == false);
                long followingCount = followings.Count(f => f.UserId == users[0].Id && f.IsArchived == false);
                design.PrintProfile(users[0].UserName, users[0].Bio, followersCount, followingCount);
                Console.WriteLine();
                printTweets(_currentUserId);
            }
        }
        public void ShowProfile(int tweetNum)
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
                var followings = cFollowData.ReadDataFromFile();
                var users = cUserData.ReadDataFromFile("Id", _currentUserId.ToString());
                long followersCount = followings.Count(f => f.FollowingUserId == users[0].Id && f.IsArchived == false);
                int followingCount = followings.Count(f => f.UserId == users[0].Id && f.IsArchived == false);
                design.PrintProfile(users[0].UserName, users[0].Bio, followersCount, followingCount);
                Console.WriteLine();
                printTweets(_currentUserId, tweetNum);
            }
        }

        public void ShowProfile(string username)
        {
            if (_currentUserId == 0)
            {
                design.Error("Please Login first.");
                Console.WriteLine();
            }
            else
            {
                IFileUtil<User> cUserData = new FileUtil<User>("User.json");
                IFileUtil<Following> cFollowData = new FileUtil<Following>("Following.json");
                var followings = cFollowData.ReadDataFromFile();
                var users = cUserData.ReadDataFromFile("UserName", username);
                if (users.Count() == 0)
                {
                    design.Error("This user doesn't exist.");
                    Console.WriteLine();
                }
                else
                {
                    long followersCount = followings.Count(f => f.FollowingUserId == users[0].Id && f.IsArchived == false);
                    long followingCount = followings.Count(f => f.UserId == users[0].Id && f.IsArchived == false);
                    design.PrintProfile(users[0].UserName, users[0].Bio, followersCount, followingCount);
                    if (followings.Any(f => f.UserId == _currentUserId && f.FollowingUserId == users[0].Id && f.IsArchived == false))
                    {
                        design.Success("-- Fallowing --");
                        Console.WriteLine();
                    }
                    else
                    {
                        design.Error("-- Not Following --");
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    printTweets(users[0].Id);
                }
            }
        }
        public void ShowProfile(string username, int tweetNum)
        {
            if (_currentUserId == 0)
            {
                design.Error("Please Login first.");
                Console.WriteLine();
            }
            else
            {
                IFileUtil<User> cUserData = new FileUtil<User>("User.json");
                IFileUtil<Following> cFollowData = new FileUtil<Following>("Following.json");
                var followings = cFollowData.ReadDataFromFile();
                var users = cUserData.ReadDataFromFile("UserName", username);
                if (users.Count() == 0)
                {
                    design.Error("This user doesn't exist.");
                    Console.WriteLine();
                }
                else
                {
                    long followersCount = followings.Count(f => f.FollowingUserId == users[0].Id && f.IsArchived == false);
                    long followingCount = followings.Count(f => f.UserId == users[0].Id && f.IsArchived == false);
                    design.PrintProfile(users[0].UserName, users[0].Bio, followersCount, followingCount);
                    if (followings.Any(f => f.UserId == _currentUserId && f.FollowingUserId == users[0].Id && f.IsArchived == false))
                    {
                        design.Success("-- Fallowing --");
                        Console.WriteLine();
                    }
                    else
                    {
                        design.Error("-- Not Following --");
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    printTweets(users[0].Id, tweetNum);
                }
            }
        }
        private void printTweets(long userId)
        {
            IFileUtil<Tweet> tweetFile = new FileUtil<Tweet>("Tweet.json");
            var tweets = tweetFile.ReadDataFromFile().Where(i => i.UserId == userId).OrderBy(i => i.CreateDate).Reverse();
            ITweetUtil tweetUtil = new TweetUtil(userId);
            foreach (var item in tweets)
            { 
                design.PrintTweet(item.Id, tweetUtil.PersianDate(item.CreateDate), item.Text, tweetUtil.GetLikeCount(item.Id), tweetUtil.GetCommentCount(item.Id));
            }
        }
        private void printTweets(long userId ,int tweetNum)
        {
            IDesign design = new Design();
            IFileUtil<Tweet> tweetFile = new FileUtil<Tweet>("Tweet.json");
            var tweets = tweetFile.ReadDataFromFile().Where(i => i.UserId == userId).OrderBy(i => i.CreateDate).Reverse();
            ITweetUtil tweetUtil = new TweetUtil(userId);
            int Counter = 0;
            foreach (var item in tweets)
            {
                design.PrintTweet(item.Id, tweetUtil.PersianDate(item.CreateDate), item.Text, tweetUtil.GetLikeCount(item.Id), tweetUtil.GetCommentCount(item.Id));
                Counter++;
                if (Counter == tweetNum)
                    break;
            }
        }

    }
}
