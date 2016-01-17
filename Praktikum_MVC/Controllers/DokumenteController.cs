using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

using Praktikum_MVC.Models;

namespace Praktikum_MVC.Controllers
{
    public class DokumenteController : Controller
    {
        // GET: Dokumente
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Kategorien = Dokumente.getAllKategorien();
            return View();
        }

        // POST: Dokumente
        [HttpPost]
        public ActionResult Index(Kategorie kategorie)
        {
            ViewBag.Kategorien = Dokumente.getAllKategorien();
            if (Request.Form["kategorie"] != null)
            {
                kategorie = new Kategorie { kategorie = Request.Form["kategorie"] };
                ViewBag.Dokumente = Dokumente.getDokumenteByKategorie(kategorie);
                ViewBag.kategorie = kategorie.kategorie;
            }
            return View();
        }

        // GET: Dokumente/Statistik
        [HttpGet]
        public ActionResult Statistik()
        {
            var xValues = new List<string>();
            var yValues = new List<string>();
            var statistik = Dokumente.getKategorieStatistik();
            for(int i = 0; i < statistik.Count; i++) {
                xValues.Add(statistik[i].Item1);
                yValues.Add(statistik[i].Item2);
            }
            var chart = new Chart(width: 500, height: 500)
               .AddSeries(
                   chartType: "pie",
                   legend: "Dokumente",
                   xValue: xValues,
                   yValues: yValues
               ).GetBytes("png");
            return File(chart, "image/png");
        }

        // GET: Dokumente/KategorieStatistik
        [HttpGet]
        public ActionResult KategorieStatistik(string kategorie)
        {
            var xValues = new List<string>();
            var yValues = new List<string>();
            var statistik = Dokumente.getStatistikByKategorie(new Kategorie { kategorie = kategorie });
            for (int i = 0; i < statistik.Count; i++)
            {
                xValues.Add(statistik[i].Item1);
                yValues.Add(statistik[i].Item2);
            }
            var chart = new Chart(width: 500, height: 500)
               .AddSeries(
                   chartType: "pie",
                   legend: "Dokumente",
                   xValue: xValues,
                   yValues: yValues
               ).GetBytes("png");
            return File(chart, "image/png");
        }
    }
}