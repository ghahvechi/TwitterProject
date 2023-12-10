using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    public interface IDesign
    {
        void Error(string message);

        void Success(string message);

        void PrintSearchResult(int counter, string username, long followersCount, long followingCount, bool status);

        void PrintProfile(string username, string bio, long followersCount, long followingCount);

        void PrintTweet(long id, string date, string text, long likesCount, long commentsCount);
        void PrintForTimeLine(long id, string text, string userName, long likesCount, long commentsCount, string date);
        void PrintForSelectTweet(long id, string text, string userName, long likesCount, long commentsCount, string date);
        void PrintForSelectComment(string userName, string text);

    }
}
