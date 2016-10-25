using BussinessLogicLayer;
using DataAccess;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasteBook.Controllers
{
    public class PasteBookController : Controller
    {
        UserBLL userManager = new UserBLL();
        FriendBLL friendManager = new FriendBLL();
        PostBLL postManager = new PostBLL();
        PostDAL postLayer = new PostDAL();
        AccountBLL accountManager = new AccountBLL();


        [HttpGet]
        public ActionResult Index()
        {
            ProfileViewModel model = new ProfileViewModel();
            model.UserModel = userManager.GetUserInfo((string)Session["UserName"]);
            return View(model);
        }

        public ActionResult HomePartialView(int ID,string page)
        {
            ProfileViewModel model = new ProfileViewModel();
            if (page == "Profile")
            {
                model.PostList = postLayer.RetrievePost(ID);
            }
            else
            {
                model.PostList = postManager.RetrieveHomePagePost(ID);
            }
          
            return PartialView("HomePartialView", model);
        }

        public ActionResult Friend()
        {
            ProfileViewModel model = new ProfileViewModel();
            model.FriendList = friendManager.RetrieveFriendList((int)Session["ID"]);
            return View(model);
        }

        public ActionResult Search(string Search)
        {
            UserViewModel model = new UserViewModel();
            model.UserList = userManager.SearchUser(Search);
            return View(model);
        }

        public new ActionResult Profile(string username)
        {
            ProfileViewModel model = new ProfileViewModel();
            model.UserModel = userManager.GetUserInfo(username);
            return View(model);
        }

        public ActionResult Post(int PostID)
        {
            PB_POST post = new PB_POST();
            post = postManager.RetrieveSpecificPost(PostID);
            return View(post);
        }

        public ActionResult NotificationPartialView()
        {
            UserActionViewModel model = new UserActionViewModel();
            model.NotificationList = postManager.RetrieveNotification((int)Session["ID"]);
            return PartialView("NotificationPartialView", model);
        }

        public ActionResult ProfileHeaderPartialView(string username)
        {
            ProfileViewModel model = new ProfileViewModel();
            model.UserModel = userManager.GetUserInfo(username);
            model.FriendModel = friendManager.RetrieveFriend(model.UserModel.ID,(int)Session["ID"]);
            return PartialView("ProfileHeaderPartialView", model);
        }

        [ActionName("EditProfilePicture")]
        public new ActionResult Profile(HttpPostedFileBase file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                file.InputStream.CopyTo(ms);
                byte[] profilePic = ms.GetBuffer();
                var User_Name = (string)Session["UserName"];
                userManager.EditProfilePicture(User_Name, profilePic);
                return RedirectToAction("Profile", "PasteBook", new { username = User_Name });
            }
        }

        
        public JsonResult LikePost(int Post_ID,int Receiver_ID)
        {
            postManager.LikePost(Post_ID, (int)Session["ID"],Receiver_ID);
            return Json(new { Url = Url.Action("Index"), JsonRequestBehavior.AllowGet });
        }

        public JsonResult UnlikePost(int ID)
        {
            postManager.UnlikePost(ID);
            return Json(new { Url = Url.Action("Index"), JsonRequestBehavior.AllowGet });
        }

        public JsonResult AddFriend(PB_FRIENDS model)
        {
            friendManager.AddFriend(model);
            return Json(new { Url = Url.Action("Profile"), JsonRequestBehavior.AllowGet });
        }

        public JsonResult AddPost(string postContent, int profileOwnerID)
        {
            PB_POST model = new PB_POST();
            model.CREATED_DATE = DateTime.Now;
            model.POSTER_ID = (int)Session["ID"];
            model.PROFILE_OWNER_ID = profileOwnerID;
            model.CONTENT = postContent;
            return Json(new { PostResult = postManager.AddPost(model), JsonRequestBehavior.AllowGet });
        }

        public JsonResult AddComment(PB_COMMENT model)
        {
            model.DATE_CREATED = DateTime.Now;
            model.POSTER_ID = (int)Session["ID"];
            return Json(new { result = postManager.AddComment(model), JsonRequestBehavior.AllowGet });
        }

        public JsonResult EditAboutMe(int userID, string content)
        {
            return Json(new { result = userManager.EditAboutMe(userID, content), JsonRequestBehavior.AllowGet });
        }

    
        public JsonResult AcceptFriend(int friendID)
        {
            return Json(new { result = friendManager.AcceptFriendRequest(friendID), JsonRequestBehavior.AllowGet });
        }

        public JsonResult RejectFriend(int friendID)
        {
            return Json(new { result = friendManager.RejectFriendRequest(friendID), JsonRequestBehavior.AllowGet });
        }

    }
}