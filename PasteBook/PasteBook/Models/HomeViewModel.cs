using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook
{
    public class HomeViewModel
    {
        public LikeModel LikeModel { get; set; }
        public List<LikeModel> LikeList { get; set; }
        public List<PostModel> PostList { get; set; }
    }
}