using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook.Models
{
    public class FriendModel
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public int Friend_ID { get; set; }
        public string Request { get; set; }
        public string BlockedED { get; set; }
        public System.DateTime Created_Date { get; set; }
    }
}