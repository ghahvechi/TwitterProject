using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    public class Like : ISerializerData
    {
        public Like() // ایجاد نمونه خالی جهت پذیرش دیتا- مقصد 
        {

        }
        public Like(long userId, long tweetId,  DateTime createDate, bool isArchived) //  ایجاد نمونه مقداردهی شده جهت افزودن دیتا- منبع
        {
            UserId = userId;
            TweetId = tweetId;
            CreateDate = createDate;
            IsArchived = isArchived;
        }
        public long Id { get; set; }
        public long UserId { get; set; }
        public long TweetId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsArchived { get; set; }
    }
}
