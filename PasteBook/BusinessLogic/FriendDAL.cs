using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class FriendDAL
    {
        public List<Exception> errorList = new List<Exception>();

        public int AddFriend(PB_FRIENDS friend)
        {
            int returnValue = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PB_FRIENDS.Add(friend);
                    PB_NOTIFICATION notif = new PB_NOTIFICATION() {
                        SENDER_ID = friend.USER_ID,
                        CREATED_DATE = DateTime.Now,
                        NOTIF_TYPE = "F",
                        RECEIVER_ID = friend.FRIEND_ID,
                        SEEN = "N"
                    };
                    context.PB_NOTIFICATION.Add(notif);
                    returnValue = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return returnValue;
        }

        public int AcceptFriendRequest(int FriendID)
        {
            int returnValue = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var friendToUpdate = context.PB_FRIENDS.Where(o => o.ID == FriendID);
                    foreach (var item in friendToUpdate)
                    {
                        item.REQUEST = "Y";
                    }
                    returnValue = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return returnValue;
        }
        public List<PB_FRIENDS> RetrieveFriendList(int userID)
        {
            List<PB_FRIENDS> friendList = new List<PB_FRIENDS>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return friendList = context.PB_FRIENDS
                        .Include("PB_USER")
                        .Include("PB_USER1")
                        .Where(x=>x.USER_ID == userID || x.FRIEND_ID == userID)
                        .OrderByDescending(x=>x.CREATED_DATE)
                        .ToList();
                }
            }
            catch (Exception ex)
            { 
                errorList.Add(ex);
            }
            return friendList = null;
        }

        


    }
}
