using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BLFriendManager
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
                    return friendList = context.PB_FRIENDS.Where(x=>x.USER_ID == userID || x.FRIEND_ID == userID).ToList();
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
