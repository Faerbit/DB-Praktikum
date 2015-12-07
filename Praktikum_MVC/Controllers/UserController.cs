using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Praktikum_MVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            ViewBag.users = Praktikum_MVC.Models.User.getAll();
            return View();
        }

        // GET: NewUser
        [HttpGet]
        public ActionResult NewUser()
        {
            return View();
        }

        // POST: NewUser
        [HttpPost]
        public ActionResult NewUser(Praktikum_MVC.Models.NewUser newUser)
        {
            if (!ModelState.IsValid) {
                
                return View();
            }
            else {
                newUser.register();
                return RedirectToAction("Index", "User", new { success = "true" });
            }
        }
    }
}