using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    public interface ISession
    {
        long Login(string username, string password);
    }
}
