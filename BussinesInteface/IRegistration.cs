using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    public interface IRegistration
    {
        void RegisterUser(string username, string password);
        void SetBio(string bioText);
        void ChangePassword(string oldPassword, string newPassword);
    }
}
