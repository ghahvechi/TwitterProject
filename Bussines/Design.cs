using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    public class Design : IDesign
    {
        private void WriteColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
        public void Error(string message)
        {
            WriteColor(message, ConsoleColor.Red);
        }
        public void Success(string message)
        {
            WriteColor(message, ConsoleColor.Green);
        }
        public void PrintSearchResult(int counter, string username, long followersCount, long followingCount, bool status)
        {
            IDesign design = new Design();
            Console.Write($"{counter}.");
            WriteColor($"{username}", ConsoleColor.Cyan);
            Console.Write(" | ");
            WriteColor("followers: ", ConsoleColor.Yellow);
            Console.Write($"{followersCount}");
            Console.Write(" | ");
            WriteColor("following: ", ConsoleColor.Yellow);
            Console.Write($"{followingCount}");
            Console.Write(" | ");
            WriteColor("status: ", ConsoleColor.Yellow);
            if (status)
            {
                WriteColor("following", ConsoleColor.Green);
            }
            else
            {
                WriteColor("not following", ConsoleColor.Red);
            }
            Console.WriteLine();
        }

        public void PrintProfile(string username, string bio, long followersCount, long followingCount)
        {
            WriteColor("username: ", ConsoleColor.Yellow);
            WriteColor($"{username}", ConsoleColor.Cyan);
            Console.WriteLine();
            WriteColor("Bio: ", ConsoleColor.Yellow);
            WriteColor(bio, ConsoleColor.White);
            Console.WriteLine();
            WriteColor("Followers: ", ConsoleColor.Yellow);
            Console.Write($"{followersCount}");
            Console.Write(" | ");
            WriteColor("Following: ", ConsoleColor.Yellow);
            Console.Write($"{followingCount}");
            Console.WriteLine();
        }

        public void PrintTweet(long id, string date, string text, long likesCount, long commentsCount)
        {
            Console.Write($"#{id}: ");
            WriteColor($"{date}", ConsoleColor.White);
            Console.WriteLine();
            WriteColor(text, ConsoleColor.White);
            Console.WriteLine();
            WriteColor("Like: ", ConsoleColor.Magenta);
            Console.Write($"{likesCount} | ");
            WriteColor("Comments: ", ConsoleColor.Yellow);
            Console.Write($"{commentsCount}");
            Console.WriteLine();
        }

        public void PrintForTimeLine(long id, string text, string userName, long likesCount, long commentsCount, string date)
        {
            Console.Write($"#{id}: ");
            WriteColor($"{text}", ConsoleColor.White);
            Console.WriteLine();
            WriteColor($"{userName}", ConsoleColor.Cyan);
            Console.Write(" | ");
            Console.Write($"{likesCount}");
            WriteColor(" Like", ConsoleColor.Magenta);
            Console.Write($" | {commentsCount}");
            WriteColor(" Comment", ConsoleColor.Yellow);
            Console.Write(" | ");
            WriteColor($"{date}", ConsoleColor.White);
        }
        public void PrintForSelectTweet(long id, string text, string userName, long likesCount, long commentsCount, string date)
        {
            Console.Write($"#{id}");
            Console.WriteLine();
            WriteColor($"{text}", ConsoleColor.White);
            Console.WriteLine();
            WriteColor($"{userName}", ConsoleColor.Cyan);
            Console.Write(" | ");
            Console.Write($"{likesCount}");
            WriteColor(" Like", ConsoleColor.Magenta);
            Console.Write($" | {commentsCount}");
            WriteColor(" Comment", ConsoleColor.Yellow);
            Console.Write(" | ");
            WriteColor($"{date}", ConsoleColor.White);
        }
        public void PrintForSelectComment(string userName,string text)
        {
            WriteColor($"- {userName} : ", ConsoleColor.Cyan);
            WriteColor($"{text}", ConsoleColor.White);
        }
    }
}
