using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    public interface ITweetUtil
    {
        void AddTweet(string text);
        void AddCommnet(long tweetId, string text);
        void AddLike(long tweetId);
        void GetTimeLine();
        void GetTimeLine(int last);
        void GetComment(long tweetId);
        long GetCommentCount(long tweetId);
        long GetLikeCount(long tweetId);
        string PersianDate(DateTime DateTime1);
        void DelLike(long tweetId);
    }
}
