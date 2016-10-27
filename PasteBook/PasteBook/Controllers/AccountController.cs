using BussinessLogicLayer;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasteBook
{
    public class AccountController : Controller
    {
        AccountBLL accountManager = new AccountBLL();
        UserBLL userManager = new UserBLL();

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.CountryList = new SelectList(accountManager.RetrieveCountry(), "ID", "COUNTRY");
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserViewModel model,string confirmPassword)
        {

            if (accountManager.CheckIfUserNameExists(model.UserEF.USER_NAME) == true)
            {
                ModelState.AddModelError("UserEF.USER_NAME", "Username address already exists. Please enter a different User name.");
            }
            if (accountManager.CheckIfEmailExists(model.UserEF.EMAIL_ADDRESS) == true)
            {
                ModelState.AddModelError("UserEF.EMAIL_ADDRESS", "Email address already exists. Please enter a different Email Address.");
            }
            if (model.UserEF.PASSWORD != confirmPassword)
            {
                ModelState.AddModelError("UserEF.PASSWORD", "Your password do not match.");
            }
            if (ModelState.IsValid)
            {
                accountManager.AddUserAccount(model.UserEF);
                Session["UserName"] = model.UserEF.USER_NAME;
                Session["ID"] = model.UserEF.ID;
                return View("LoginPage");
            }
            ViewBag.CountryList = new SelectList(accountManager.RetrieveCountry(), "ID", "COUNTRY");
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult LoginPage()
        {
            ViewBag.LoginError = false;
            return View();
        }

        [HttpPost]
        public ActionResult LoginPage(UserViewModel model)
        {
            bool result = false;
            var user = accountManager.GetUser(model.UserEF.EMAIL_ADDRESS);

            if (user == null)
            {
                ViewBag.LoginError = true;
            }
            else
            {
                result = accountManager.Login(model.UserEF);
            }

            if (result == true)
            {
                Session["UserName"] = user.USER_NAME;
                Session["ID"] = user.ID;
                return RedirectToAction("Index", "PasteBook");
            }
            else
            {
                ViewBag.LoginError = true;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Setting()
        {
            PB_USER model = new PB_USER();
            model = userManager.GetUserInfo((string)Session["UserName"]);
            ViewBag.CountryList = new SelectList(accountManager.RetrieveCountry(), "ID", "COUNTRY", model.COUNTRY_ID);
            ViewBag.EditProfileStatus = false;
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Setting(PB_USER model)
        {
            int result = userManager.EditUserProfile(model);
            if (result == 1)
            {
                ViewBag.EditProfileStatus = true;
            }
            ViewBag.CountryList = new SelectList(accountManager.RetrieveCountry(), "ID", "COUNTRY", model.COUNTRY_ID);
            Session["UserName"] = model.USER_NAME;
            return View(model);
        }

        [HttpGet]
        public ActionResult AccountSetting()
        {
            PB_USER model = new PB_USER();
            model = userManager.GetUserInfo((string)Session["UserName"]);
            model.PASSWORD = null;
            ViewBag.EditAccount = "default";
            return View(model);
        }

        [HttpPost]
        public ActionResult AccountSetting(PB_USER user,string password)
        {
            bool result = false;
            result = accountManager.CheckIfPasswordMatch(user.PASSWORD, (int)Session["ID"]);

            if (result == true)
            {
                user.PASSWORD = password;
                userManager.EditAccountSetting(user);
                ViewBag.EditAccount = "success";
            }
            else
            {
                ViewBag.EditAccount = "failed";
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult SecuritySetting()
        {
            PB_USER model = new PB_USER();
            model = userManager.GetUserInfo((string)Session["UserName"]);
            model.PASSWORD = null;
            ViewBag.SecurityStatus = "default";
            return View(model);
        }

        [HttpPost]
        public ActionResult SecuritySetting(PB_USER user,string newPassword)
        {
            bool result = false;
            result = accountManager.CheckIfPasswordMatch(user.PASSWORD, (int)Session["ID"]);

            if (result == true)
            {
                user.PASSWORD = newPassword;
                userManager.EditSecurityAccount(user);
                ViewBag.SecurityStatus = "success";
                return View(user);
            }
            else
            {
                ViewBag.SecurityStatus = "failed";
                return View(user);
            }
        }


        public JsonResult Logout()
        {
            Session.Clear();
            return Json(new { Status = "Session Cleared" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckIfUsernameExists(string username)
        {
            return Json(new { result = accountManager.CheckIfUserNameExists(username), JsonRequestBehavior.AllowGet });
        }
        public JsonResult CheckIfEmailExists(string email)
        {
            return Json(new { result = accountManager.CheckIfEmailExists(email), JsonRequestBehavior.AllowGet });
        }
    }
}