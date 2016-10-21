using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook
{
    public class ProfileViewModel
    {
        public List<PB_POST> PostList { get; set; }
        public PB_FRIENDS FriendModel { get; set; }
        public List<PB_FRIENDS> FriendList { get; set; }
        public PB_USER UserModel { get; set; }
    }
}