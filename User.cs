using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    public class User: ISerializerData
    {
        public User() // ایجاد نمونه خالی جهت پذیرش دیتا- مقصد 
        {

        }
        public User(string username, string password,  DateTime createDate,string bio, bool isArchived) //  ایجاد نمونه مقداردهی شده جهت افزودن دیتا- منبع
        {
            UserName = username;
            PassWord = password;
            CreateDate = createDate;
            Bio = bio;
            IsArchived = isArchived;
        }
        public long Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public DateTime CreateDate { get; set; }
        public string Bio { get; set; }
        public bool  IsArchived {get;set;}
    }
}
