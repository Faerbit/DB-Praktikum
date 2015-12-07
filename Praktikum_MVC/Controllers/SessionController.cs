using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Praktikum_MVC.Controllers
{
    public class SessionController : Controller
    {
        // GET: Session
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home", new { error = "login" });
        }
        // POST: Session
        [HttpPost]
        public ActionResult Index(Praktikum_MVC.Models.User user)
        {
            if (Session["authenticated"] != null && Session["authenticated"] != "") {
                Session["authenticated"] = "";
                return RedirectToAction("Index", "Home", new { logout = "true" });
            }
            if (user.checkPassword()) {
                Session["authenticated"] = user.username;
                return RedirectToAction("Index", "Home", new { login = "true" });
            }
            else {
                ViewBag.error = true;
            }
            return View();
        }
    }
}