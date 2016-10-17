using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook.Models
{
    public class NotificationModel
    {
        public int ID { get; set; }
        public int Receiver_ID { get; set; }
        public string Notif_Type { get; set; }
        public int Sender_ID { get; set; }
        public System.DateTime Created_Date { get; set; }
        public Nullable<int> Comment_ID { get; set; }
        public Nullable<int> Post_ID { get; set; }
        public string Seen { get; set; }
    }
}