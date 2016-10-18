using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasteBook.Controllers
{
    public class PasteBookController : Controller
    {
        ProfileManager pbManager = new ProfileManager();
        // GET: PasteBook
        [HttpGet]
        public ActionResult Index()
        {
            ViewModel vm = new ViewModel();
            vm.PostList = pbManager.RetrievePost();
            vm.LikeList = pbManager.RetrieveLike();
            return View(vm);
        }
        [HttpPost]
        [ActionName("Post")]
        public ActionResult Index(ViewModel model)
        {
            model.PostModel.Created_Date = DateTime.Now;
            model.PostModel.Poster_ID = (int)Session["ID"];
            model.PostModel.Profile_Owner_ID = (int)Session["ID"];
            pbManager.AddPost(model.PostModel);
            return RedirectToAction("Index", "PasteBook");
        }

        //[Route("{controller}/{username}")]
        public new ActionResult Profile(string username)
        {
            ViewModel model = new ViewModel();
            model.UserModel = pbManager.GetUserInfo(username);
            ViewBag.Country = pbManager.GetCountryName((int)model.UserModel.Country_ID);
            return View(model);
        }

        public JsonResult LikePost(LikeModel model)
        {
            pbManager.LikePost(model);
            return Json(new { Url = Url.Action("Index"), JsonRequestBehavior.AllowGet});
            //return Json(new { Message = }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Friend()
        {
            return View();
        }
    }
}