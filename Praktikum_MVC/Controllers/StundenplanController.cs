using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Praktikum_MVC.Models;

namespace Praktikum_MVC.Controllers
{
    public class StundenplanController : Controller
    {
        // GET: Stundenplan
        public ActionResult Index()
        {
            ViewBag.stundenplan = Stundenplan.GetMockupDaten();
            return View();
        }
    }
}