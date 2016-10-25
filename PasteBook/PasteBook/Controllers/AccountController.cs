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
        public ActionResult Index(UserViewModel model)
        {

            if (accountManager.CheckIfUserNameExists(model.UserEF.USER_NAME) == true)
            {
                ModelState.AddModelError("UserEF.USER_NAME", "Username address already exists. Please enter a different User name.");
            }
            if (accountManager.CheckIfEmailExists(model.UserEF.EMAIL_ADDRESS) == true)
            {
                ModelState.AddModelError("UserEF.EMAIL_ADDRESS", "Email address already exists. Please enter a different Email Address.");
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
            return View();
        }

        [HttpPost]
        public ActionResult LoginPage(UserViewModel model)
        {
            bool result = false;
            var user = accountManager.GetUser(model.UserEF.EMAIL_ADDRESS);

            if (user == null)
            {
                ModelState.AddModelError("UserEF.EMAIL_ADDRESS", "Email Address doesn't exists.");
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
                ModelState.AddModelError("UserEF.PASSWORD", "Incorrect Password.");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Setting()
        {
            PB_USER model = new PB_USER();
            model = userManager.GetUserInfo((string)Session["UserName"]);
            ViewBag.CountryList = new SelectList(accountManager.RetrieveCountry(), "ID", "COUNTRY", model.COUNTRY_ID);
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Setting(PB_USER model)
        {
            userManager.EditUserProfile(model);
            ViewBag.CountryList = new SelectList(accountManager.RetrieveCountry(), "ID", "COUNTRY", model.COUNTRY_ID);
            Session["UserName"] = model.USER_NAME;
            return RedirectToAction("Setting");
        }

        [ActionName("EditSecuritySetting")]
        public ActionResult Setting(PB_USER user, string newPassword)
        {
            bool result = false;
            result = accountManager.CheckIfPasswordMatch(user.PASSWORD, (int)Session["ID"]);

            if (result == true)
            {
                user.PASSWORD = newPassword;
                userManager.EditSecurityAccount(user);

                return RedirectToAction("Setting");
            }
            else
            {
                ModelState.AddModelError("PASSWORD", "Incorrect Password.");
                return RedirectToAction("Setting");
            }
        }

        public JsonResult Logout()
        {
            Session.Clear();
            return Json(new { Status = "Session Cleared" }, JsonRequestBehavior.AllowGet);
        }
    }
}