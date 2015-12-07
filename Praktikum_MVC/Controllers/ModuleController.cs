using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Praktikum_MVC.Controllers
{
    public class ModuleController : Controller
    {
        // GET: Module
        public ActionResult Index()
        {
            ViewBag.Module = Praktikum_MVC.Models.Modul.getAll();
            return View();
        }
    }
}