using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    internal interface IProfile
    {
        void ShowProfile();
        void ShowProfile(int twitNum);
        void ShowProfile(string username);
        void ShowProfile(string username, int twitNum);
    }
}
