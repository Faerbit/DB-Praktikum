using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Praktikum_MVC.Controllers
{
    public class DokumenteController : Controller
    {
        // GET: Dokumente
        public ActionResult Index()
        {
            return View();
        }
    }
}