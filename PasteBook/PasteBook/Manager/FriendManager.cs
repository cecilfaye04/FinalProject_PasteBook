using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook
{
    public class FriendManager
    {
        BLToMVCMapper mapper = new BLToMVCMapper();
        BLFriendManager BLmanager = new BLFriendManager();
        PasteBookManager manager = new PasteBookManager();

        public int AddFriend(FriendModel friend)
        {
            friend.Created_Date = DateTime.Now;
           return BLmanager.AddFriend(mapper.FriendMapper(friend));
        }

        public List<FriendModel> RetrieveFriend(int id)
        {
            List<FriendModel> listOfFriend = new List<FriendModel>();
            listOfFriend = mapper.FriendListMapper(BLmanager.RetrieveFriendList(id));
            return listOfFriend;
        }

        public UserModel GetUserByID(int id)
        {
           return mapper.GetUserMapper(manager.GetUserByID(id));
        }




    }
}