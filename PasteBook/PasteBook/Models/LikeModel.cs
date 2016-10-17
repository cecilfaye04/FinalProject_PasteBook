using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook.Models
{
    public class LikeModel
    {
        public int ID { get; set; }
        public int Post_ID { get; set; }
        public int Liked_By { get; set; }
    }
}