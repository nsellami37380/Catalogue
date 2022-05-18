using projetCatalogueProduit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace projetCatalogueProduit.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
         LoginModel lm = new LoginModel();
         return View(lm);
        }

      [HttpPost]
      public ActionResult login(LoginModel loginModel, string returnUrl)
      {
       //  if (!string.IsNullOrEmpty(loginModel.UserName)
        //    && !string.IsNullOrEmpty(loginModel.Password))
        if ((loginModel.UserName == "admin") && (loginModel.Password == "admin"))
         {
            CATALOGUE_Entities db = new CATALOGUE_Entities();
            USER user = db.USER.Find(1);
            if (user.login == "admin")
            {
               user.password = FormsAuthentication.HashPasswordForStoringInConfigFile("admin", "SHA1");
               db.SaveChanges();
            }
            FormsAuthentication.SetAuthCookie(loginModel.UserName, false);
            if (string.IsNullOrEmpty(returnUrl))
               return RedirectToAction("Index","Home") ;
            else
               return Redirect(returnUrl);
         }
         else 
         {
            return RedirectToAction("Login");
         }
      }
      // ---------------------------------------------------------------------------------------
      [HttpGet]
      public ActionResult LogOut()
      {
         FormsAuthentication.SignOut();
         return RedirectToAction("Login");
         //return View("Login");
      }
      
      // ---------------------------------------------------------------------------------------

   }
}