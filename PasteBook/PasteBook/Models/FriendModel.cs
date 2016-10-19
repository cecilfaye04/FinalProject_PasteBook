using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook
{
    public class FriendModel
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public string User_UserName { get; set; }
        public string User_FullName { get; set; }
        public int Friend_ID { get; set; }
        public string Friend_Username { get; set; }
        public string Friend_Name { get; set; }
        public string Request { get; set; }
        public string Blocked { get; set; }
        public System.DateTime Created_Date { get; set; }
    }
}