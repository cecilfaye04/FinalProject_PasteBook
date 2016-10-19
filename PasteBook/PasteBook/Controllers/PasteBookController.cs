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
        FriendManager friendManager = new FriendManager();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomePartialView(int ID)
        {
            HomeViewModel homeModel = new HomeViewModel();
            homeModel.PostList = pbManager.RetrievePost(ID);
            homeModel.LikeList = pbManager.RetrieveLike();
            return PartialView("HomePartialView", homeModel);
        }

        public ActionResult Friend()
        {
            ViewModel vmModel = new ViewModel();
            vmModel.FriendList = friendManager.RetrieveFriend((int)Session["ID"]);
            foreach (var item in vmModel.FriendList)
            {
                item.User_FullName = friendManager.GetUserByID(item.User_ID).First_Name + " " + friendManager.GetUserByID(item.User_ID).Last_Name;
                item.User_UserName = friendManager.GetUserByID(item.User_ID).User_Name;
                item.Friend_Username = friendManager.GetUserByID(item.Friend_ID).User_Name;
                item.Friend_Name = friendManager.GetUserByID(item.Friend_ID).First_Name + " " + friendManager.GetUserByID(item.Friend_ID).Last_Name;
            }
            return View(vmModel);
        }

        public new ActionResult Profile(string username)
        {
            ViewModel model = new ViewModel();
            model.UserModel = pbManager.GetUserInfo(username);
            ViewBag.Country = pbManager.GetCountryName((int)model.UserModel.Country_ID);
            model.FriendList = friendManager.RetrieveFriend((int)Session["ID"]);
            return View(model);
        }

        public JsonResult LikePost(LikeModel model)
        {
            pbManager.LikePost(model);
            return Json(new { Url = Url.Action("Index"), JsonRequestBehavior.AllowGet});
        }

        public JsonResult AddFriend(FriendModel model)
        {
            friendManager.AddFriend(model);
            return Json(new { Url = Url.Action("Profile"), JsonRequestBehavior.AllowGet });
        }

        public JsonResult AddPost(string postContent)
        {
            PostModel model = new PostModel();
            model.Created_Date = DateTime.Now;
            model.Poster_ID = (int)Session["ID"];
            model.Profile_Owner_ID = (int)Session["ID"];
            model.Content = postContent;
            return Json(new { PostResult = pbManager.AddPost(model), JsonRequestBehavior.AllowGet });
        }

      


    }
}