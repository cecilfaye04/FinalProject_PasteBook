using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook.Models
{
    public class CommentModel
    {
        public int ID { get; set; }
        public int Post_ID { get; set; }
        public int Poster_ID { get; set; }
        public string Content { get; set; }
        public System.DateTime Date_Created { get; set; }
    }
}