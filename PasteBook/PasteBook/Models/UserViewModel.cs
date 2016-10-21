using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasteBook
{
    public class UserViewModel
    {
        public PB_USER UserEF { get; set; }
        public List<PB_USER> UserList { get; set; }
        public List<SelectListItem> CountryList {get;set;}
    }
}