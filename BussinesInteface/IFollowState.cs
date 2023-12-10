using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    internal interface IFollowState
    {
        void Follow(string username);
        void UnFollow(string username);
    }
}
