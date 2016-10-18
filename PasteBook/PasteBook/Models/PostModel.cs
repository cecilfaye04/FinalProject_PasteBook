using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook
{
    public class PostModel
    {
        public int ID { get; set; }
        public System.DateTime Created_Date { get; set; }
        public string Content { get; set; }
        public int Profile_Owner_ID { get; set; }
        public string Poster_Name { get; set; }
        public string Owner_Name { get; set; }
        public int Poster_ID { get; set; }
      

    }
}