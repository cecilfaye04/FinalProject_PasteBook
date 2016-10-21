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

        public int AddFriend(PB_FRIENDS friend)
        {
            friend.CREATED_DATE = DateTime.Now;
            return friendManager.AddFriend(friend);
        }

        public int AcceptFriendRequest(int id)
        {
            return friendManager.AcceptFriendRequest(id);
        }
        public List<PB_FRIENDS> RetrieveFriendList(int id)
        {
            List<PB_FRIENDS> listOfFriend = new List<PB_FRIENDS>();
            listOfFriend = friendManager.RetrieveFriendList(id);
            return listOfFriend;
        }

    }
}
