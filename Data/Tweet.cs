using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    public class Tweet: ISerializerData
    {
        public Tweet() // ایجاد نمونه خالی جهت پذیرش دیتا- مقصد 
        {

        }
        public Tweet(long userId, string text, DateTime createDate, bool isArchived) //  ایجاد نمونه مقداردهی شده جهت افزودن دیتا- منبع
        {
            UserId = userId;
            Text = text;
            CreateDate = createDate;
            IsArchived = isArchived;
        }
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsArchived { get; set; }
    }

}
