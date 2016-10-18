using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasteBook
{
    public class ViewModel
    {
        public UserModel UserModel { get; set; }
        public PostModel PostModel { get; set; }
        public LikeModel LikeModel { get; set; }
        public List<LikeModel> LikeList {get;set;}
        public List<PostModel> PostList { get; set; }
        public List<Ref_CountryModel> CountryList {get;set;}
    }
}