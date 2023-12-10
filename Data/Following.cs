using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    public class Following : ISerializerData
    {
        public Following() // ایجاد نمونه خالی جهت پذیرش دیتا- مقصد 
        {

        }
        public Following(long userId, long followingUserId, DateTime createDate, bool isArchived) //  ایجاد نمونه مقداردهی شده جهت افزودن دیتا- منبع
        {
            UserId = userId;
            FollowingUserId = followingUserId;
            CreateDate = createDate;
            IsArchived = isArchived;
        }
        public long Id { get; set; }
        public long UserId { get; set; }
        public long FollowingUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsArchived { get; set; }
    }
}
