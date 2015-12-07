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
            ViewBag.exists = true;
            ViewBag.professor = Praktikum_MVC.Models.ProfSummary.load(id);
            return View();
        }
    }
}