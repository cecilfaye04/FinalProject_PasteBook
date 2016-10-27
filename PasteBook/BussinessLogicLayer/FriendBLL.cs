using BusinessLogic;
using DataAccess;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer
{
    public class FriendBLL
    {
        FriendDAL friendManager = new FriendDAL();
        GenericDAL<PB_FRIENDS> friendDAL = new GenericDAL<PB_FRIENDS>();
        GenericDAL<PB_NOTIFICATION> notificationDAL = new GenericDAL<PB_NOTIFICATION>();

        public int AddFriend(PB_FRIENDS friend)
        {
            int result = 0;
            friend.CREATED_DATE = DateTime.Now;
            result = friendDAL.GenericAdd(friend);

            if (result == 1)
            {
                PB_NOTIFICATION notif = new PB_NOTIFICATION();
                notif.SENDER_ID = friend.USER_ID;
                notif.CREATED_DATE = DateTime.Now;
                notif.NOTIF_TYPE = "F";
                notif.SEEN = "N";
                notif.RECEIVER_ID = friend.FRIEND_ID;
                notificationDAL.GenericAdd(notif);
            }

            return result;
        }

        public int AcceptFriendRequest(int id)
        {
            PB_FRIENDS friend = friendDAL.GetSpecific(x => x.ID == id);
            friend.CREATED_DATE = DateTime.Now;
            friend.REQUEST = "Y";
            friendDAL.GenericEdit(friend);

            PB_NOTIFICATION notif = notificationDAL.GetSpecific(x => x.SENDER_ID == friend.USER_ID && x.RECEIVER_ID == friend.FRIEND_ID);
            return notificationDAL.GenericDelete(notif);

        }

        public int RejectFriendRequest(int id)
        {
            int result = 0;
            PB_FRIENDS friend = friendDAL.GetSpecific(x => x.ID == id);
            result = friendDAL.GenericDelete(friend);

            PB_NOTIFICATION notif = notificationDAL.GetSpecific(x => x.SENDER_ID == friend.USER_ID && x.RECEIVER_ID == friend.FRIEND_ID);
            notificationDAL.GenericDelete(notif);
            return result;
        }

        public List<PB_FRIENDS> RetrieveFriendList(int id)
        {
            List<PB_FRIENDS> listOfFriend = new List<PB_FRIENDS>();
            listOfFriend = friendManager.RetrieveFriendList(id);
            return listOfFriend;
        }

        public PB_FRIENDS RetrieveFriend(int friendId,int userID)
        {
            PB_FRIENDS friend = new PB_FRIENDS();
            friend = friendDAL.GetSpecific((x => x.FRIEND_ID == friendId && x.USER_ID == userID || x.USER_ID == friendId && x.FRIEND_ID == userID));
            return friend;
        }

    }
}
