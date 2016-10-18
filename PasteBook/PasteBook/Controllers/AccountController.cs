using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasteBook
{
    public class AccountController : Controller
    {
        PBManager pbManager = new PBManager();

        [HttpGet]
        public ActionResult Index()
        {
            Session["CountryList"] = new SelectList(pbManager.RetrieveCountry(), "ID", "Country");
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(ViewModel model)
        {

            if (pbManager.CheckIfUserNameExists(model.UserModel.User_Name) == true)
            {
                ModelState.AddModelError("UserModel.User_Name", "Username address already exists. Please enter a different User name.");
            }
            if (pbManager.CheckIfEmailExists(model.UserModel.Email_Address) == true)
            {
                ModelState.AddModelError("UserModel.Email_Address", "Email address already exists. Please enter a different Email Address.");
            }
            if (ModelState.IsValid)
            {
                pbManager.AddUserAccount(model);
                Session["UserName"] = model.UserModel.User_Name;
                Session["ID"] = model.UserModel.ID;
                return RedirectToAction("Index","PasteBook");
            }
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginPage(UserModel model)
        {
            bool result = false;
            var user = pbManager.GetUser(model.Email_Address);

            if (user == null)
            {
                ModelState.AddModelError("Email_Address", "Email Address doesn't exists.");
            }
            else
            {
                result = pbManager.Login(model);
            }

            if (result == true)
            {
                Session["UserName"] = user.User_Name;
                Session["ID"] = user.ID;
                return RedirectToAction("Index","PasteBook");
            }
            else
            {
                ModelState.AddModelError("Password", "Incorrect Password.");
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