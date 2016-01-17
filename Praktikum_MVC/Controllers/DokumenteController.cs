using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
        // GET: Dokumente/Statistik
        public ActionResult Statistik()
        {
            var chart = new Chart(width: 600, height: 400)
               .AddSeries(
                   chartType: "pie",
                   legend: "Rainfall",
                   xValue: new[] { "Jan", "Feb" },
                   yValues: new[] { "5", "10" }
               ).GetBytes("png");
            return File(chart, "image/png");
        }
    }
}