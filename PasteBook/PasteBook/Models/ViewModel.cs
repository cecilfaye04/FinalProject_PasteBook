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
        public FriendModel FriendModel { get; set; }
        public List<FriendModel> FriendList { get; set; }
        public List<Ref_CountryModel> CountryList {get;set;}
    }
}