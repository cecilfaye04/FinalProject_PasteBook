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

        public JsonResult Logout()
        {
            Session.Clear();
            return Json(new { Status = "Session Cleared" }, JsonRequestBehavior.AllowGet);
        }
    }
}