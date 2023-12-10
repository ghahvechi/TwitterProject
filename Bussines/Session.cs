using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    public class Session : ISession
    {
        public long Login(string username, string password)
        {
            IFileUtil<User> fileUtil = new FileUtil<User>("User.json");
            IDesign design = new Design();
            var user = fileUtil.ReadDataFromFile().Where(u => u.UserName.Equals(username) && u.PassWord.Equals(password)).ToList();
            if (user.Count != 0)
            {
                design.Success($"Wellcome {user[0].UserName}");
                Console.WriteLine();
                return user[0].Id;
            }
            else
            {
                design.Error("Username or password is incorrect.");
                Console.WriteLine();
                return 0;
            }
        }
    }
}
