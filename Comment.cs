using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    public class Comment : ISerializerData
    {
        public Comment() // ایجاد نمونه خالی جهت پذیرش دیتا- مقصد 
        {

        }
        public Comment(long userId, long tweetId, string text, DateTime createDate, bool isArchived) //  ایجاد نمونه مقداردهی شده جهت افزودن دیتا- منبع
        {
            UserId = userId;
            TweetId = tweetId;
            Text = text;
            CreateDate = createDate;
            IsArchived = isArchived;
        }
        public long Id { get; set; }
        public long UserId { get; set; }
        public long TweetId { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsArchived { get; set; }
    }
}
