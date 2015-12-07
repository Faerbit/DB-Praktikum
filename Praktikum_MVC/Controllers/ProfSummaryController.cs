using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Praktikum_MVC.Controllers
{
    public class ProfSummaryController : Controller
    {
        // GET: ProfSummary
        public ActionResult Index(string id)
        {   
            bool exists = Praktikum_MVC.Models.ProfSummary.exists(id);
            ViewBag.exists = exists;
            if (exists) {
                ViewBag.professor = Praktikum_MVC.Models.ProfSummary.load(id);
            }
            else {
                ViewBag.professoren = Praktikum_MVC.Models.Professor.getAll();
            }
            return View();
        }
    }
}