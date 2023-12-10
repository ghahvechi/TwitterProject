using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    public class TweetUtil : ITweetUtil
    {
        public TweetUtil(long currentUserId)
        {
            CurrentUserId = currentUserId;
            design = new Design();
        }
        public long CurrentUserId { get; set; }
        public IDesign design { get; set; }
        public void AddTweet(string text)
        {
            var fileUtil = new FileUtil<Tweet>("Tweet.json");
            var tweet = new Tweet();
            tweet.UserId = CurrentUserId;
            tweet.Text = text;
            tweet.CreateDate = DateTime.Now;
            tweet.IsArchived = false;
            fileUtil.WriteDataToFile(tweet);
            design.Success("your tweet added successfully");
            Console.WriteLine();
        }
        public void AddCommnet(long tweetId, string text)
        {
            var fileUtil = new FileUtil<Comment>("Comment.json");
            var tweetUtil = new FileUtil<Tweet>("Tweet.json");
            var comment = new Comment();
            var tweets = tweetUtil.ReadDataFromFile();
            if (tweets.Any(l => l.Id == tweetId && l.IsArchived == false))
            {
                comment.UserId = CurrentUserId;
                comment.TweetId = tweetId;
                comment.Text = text;
                comment.CreateDate = DateTime.Now;
                comment.IsArchived = false;
                fileUtil.WriteDataToFile(comment);
                design.Success($"your comment added to {tweetId} successfully");
                Console.WriteLine();
            }
            else
            {
                design.Error($"{tweetId} Not Found");
                Console.WriteLine();
            }
        }
        public void AddLike(long tweetId)
        {
            var fileUtil = new FileUtil<Like>("Like.json");
            var likes = fileUtil.ReadDataFromFile();
            if (likes.Any(l => l.TweetId == tweetId && l.UserId == CurrentUserId && l.IsArchived == false))
            {
                design.Error("you liked this tweet.");
                Console.WriteLine();
            }
            else
            {
                var like = new Like();
                like.UserId = CurrentUserId;
                like.TweetId = tweetId;
                like.CreateDate = DateTime.Now;
                like.IsArchived = false;
                fileUtil.WriteDataToFile(like);
                design.Success($"you liked this tweet successfully.");
                Console.WriteLine();
            }
        }
        public void DelLike(long tweetId)
        {
            var fileUtil = new FileUtil<Like>("Like.json");
            var likes = fileUtil.ReadDataFromFile();
            var like = likes.FirstOrDefault(l => l.TweetId == tweetId && l.UserId == CurrentUserId && l.IsArchived == false);
            if (like != null)
            {
                fileUtil.ReWriteDataToFile("Id", like.Id.ToString(), "IsArchived", true);
                design.Success($"You disliked this tweet successfully.");
                Console.WriteLine();
            }
            else
            {
                design.Error("you haven't like this tweet.");
                Console.WriteLine();
            }

        }
        public void GetTimeLine()
        {
            var tweetFile = new FileUtil<Tweet>("Tweet.json");
            var followingFile = new FileUtil<Following>("Following.json");
            var followings = followingFile.ReadDataFromFile();
            followings = followings.Where(i => i.UserId == CurrentUserId && i.IsArchived == false).ToList();
            var tweets = tweetFile.ReadDataFromFile();
            if (!followings.Any())
            {
                design.Error("You do not have any followig");
                Console.WriteLine();
            }
            else if (!tweets.Any())
            {
                design.Error("there is no tweet");
                Console.WriteLine();
            }
            else
            {
                foreach (var item1 in followings)
                {
                    var followingTweets = tweets.Where(i => i.UserId == item1.FollowingUserId).OrderBy(i => i.CreateDate).Reverse();
                    if (followingTweets.Any())
                    {
                        foreach (var item2 in followingTweets)
                        {

                            /*Console.WriteLine($"#{item2.Id}: {item2.Text}");
                            Console.WriteLine($"{GetTwitterUserName(item2.Id)} | {GetLikeCount(item2.Id)} Like | {GetCommentCount(item2.Id)} Comment | {PersianDate(item2.CreateDate)}");*/
                            design.PrintForTimeLine(item2.Id, item2.Text, GetTwitterUserName(item2.Id), GetLikeCount(item2.Id), GetCommentCount(item2.Id), PersianDate(item2.CreateDate));
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        design.Error("your followings do not have any tweet");
                        Console.WriteLine();
                    }

                }
            }
        }
        public void GetTimeLine(int last)
        {
            var tweetFile = new FileUtil<Tweet>("Tweet.json");
            var followingFile = new FileUtil<Following>("Following.json");
            var followings = followingFile.ReadDataFromFile().Where(i => i.UserId == CurrentUserId);
            var tweets = tweetFile.ReadDataFromFile();
            if (!followings.Any())
            {
                design.Error("you do not have any followig");
                Console.WriteLine();
            }
            else if (!tweets.Any())
            {
                design.Error("there is no tweet");
                Console.WriteLine();
            }
            else
            {
                foreach (var item1 in followings)
                {
                    var counter = 0;
                    var followingTweets = tweets.Where(i => i.UserId == item1.FollowingUserId).OrderBy(i => i.CreateDate).Reverse();
                    if (followingTweets.Any())
                    {
                        foreach (var item2 in tweets.Where(i => i.UserId == item1.FollowingUserId).OrderBy(i => i.CreateDate).Reverse())
                        {
                            /*Console.WriteLine($"#{item2.Id}: {item2.Text}");
                                Console.WriteLine($"{GetTwitterUserName(item2.Id)} | {GetLikeCount(item2.Id)} Like | {GetCommentCount(item2.Id)} Comment | {PersianDate(item2.CreateDate)}");*/
                            design.PrintForTimeLine(item2.Id, item2.Text, GetTwitterUserName(item2.Id), GetLikeCount(item2.Id), GetCommentCount(item2.Id), PersianDate(item2.CreateDate));
                            Console.WriteLine();
                            counter++;
                            if (counter == last)
                            { break; }
                        }
                    }
                    else
                    {
                        design.Error("your followings do not have any tweet");
                        Console.WriteLine();
                    }
                }
            }
        }
        public void GetComment(long tweetId)
        {
            var tweetFile = new FileUtil<Tweet>("Tweet.json");
            var tweets = tweetFile.ReadDataFromFile();
            if (tweets.Any())
            {
                var tweet = tweets.Where(i => i.Id == tweetId);
                if (tweet.Any())
                {
                    var tweetMember = tweet.First();
                    //Console.WriteLine($@"#{tweetMember.Id}\n{GetTwitterUserName(tweetMember.Id)} | {GetLikeCount(tweetMember.Id)} Like | {GetCommentCount(tweetMember.Id)} Comment | {PersianDate(tweetMember.CreateDate)}");
                    design.PrintForSelectTweet(tweetMember.Id, tweetMember.Text, GetTwitterUserName(tweetMember.Id), GetLikeCount(tweetMember.Id), GetCommentCount(tweetMember.Id), PersianDate(tweetMember.CreateDate));
                    Console.WriteLine("\n\n");
                    Console.WriteLine("Comments:");
                    var commentFile = new FileUtil<Comment>("Comment.json");
                    var comments = commentFile.ReadDataFromFile();
                    if (comments.Any())
                    {
                        var comment = comments.Where(i => i.TweetId == tweetId);
                        if (comment.Any())
                        {
                            foreach (var item in comment)
                            {
                                design.PrintForSelectComment(GetCommenterUserName(item.Id),item.Text);
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            design.Error("the comments not found");
                        }
                    }
                    else
                    {
                        design.Error("there is no comment");
                    }

                }
                else
                {
                    design.Error("the tweet not found");
                }
            }
            else
            {
                design.Error("there is no tweet");
            }
        }
        private string GetTwitterUserName(long tweetId)
        {
            var tweetFile = new FileUtil<Tweet>("Tweet.json");
            var userFile = new FileUtil<User>("User.json");
            var tweets = tweetFile.ReadDataFromFile().Where(i => i.Id == tweetId);
            var userId = tweets.First().UserId;
            var users = userFile.ReadDataFromFile().Where(i => i.Id == userId);
            return users.First().UserName;
        }
        public long GetCommentCount(long tweetId)
        {
            var commentFile = new FileUtil<Comment>("Comment.json");
            var commentCount = commentFile.ReadDataFromFile().Count(i => i.TweetId == tweetId);
            return commentCount;
        }
        public long GetLikeCount(long tweetId)
        {
            var likeFile = new FileUtil<Like>("Like.json");
            var likeCount = likeFile.ReadDataFromFile().Count(i => i.TweetId == tweetId && i.IsArchived == false);
            return likeCount;
        }
        private string GetCommenterUserName(long commentId)
        {
            var commentFile = new FileUtil<Comment>("Comment.json");
            var userFile = new FileUtil<User>("User.json");
            var comments = commentFile.ReadDataFromFile().Where(i => i.Id == commentId);
            var userId = comments.First().UserId;
            var users = userFile.ReadDataFromFile().Where(i => i.Id == userId);
            return users.First().UserName;
        }
        public string PersianDate(DateTime DateTime1)
        {
            PersianCalendar PersianCalendar1 = new PersianCalendar();
            return string.Format(@"{0}/{1}/{2}",
                         PersianCalendar1.GetYear(DateTime1),
                         PersianCalendar1.GetMonth(DateTime1),
                         PersianCalendar1.GetDayOfMonth(DateTime1));
        }
    }
}
