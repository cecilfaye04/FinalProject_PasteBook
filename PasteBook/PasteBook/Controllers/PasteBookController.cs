using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasteBook.Controllers
{
    public class PasteBookController : Controller
    {
        // GET: PasteBook
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public new ActionResult Profile(string username)
        {
            return View();
        }
    }
}