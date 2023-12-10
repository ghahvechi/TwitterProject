using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    public class TweetUtil
    {
        public TweetUtil(long currentUserId)
        {
            CurrentUserId = currentUserId;
        }
        public long CurrentUserId { get; set; }
        public void AddTweet (string text)
        {
            var fileUtil = new FileUtil<Tweet>("Tweet.json");
            var tweet = new Tweet();
            tweet.UserId = CurrentUserId;
            tweet.Text = text;
            tweet.CreateDate = DateTime.Now;
            tweet.IsArchived = false;
            fileUtil.WriteDataToFile(tweet);
        }
        public void AddCommnet (long tweetId,string text)
        {
            var fileUtil = new FileUtil<Comment>("Comment.json");
            var comment = new Comment();
            comment.UserId = CurrentUserId;
            comment.TweetId = tweetId;
            comment.Text = text;
            comment.CreateDate = DateTime.Now;
            comment.IsArchived = false;
            fileUtil.WriteDataToFile(comment);
        }
        public void AddLike(long tweetId)
        {

        }
    }
}
