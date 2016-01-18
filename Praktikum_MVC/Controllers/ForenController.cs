using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Praktikum_MVC.Models;

namespace Praktikum_MVC.Controllers
{
    public class ForenController : Controller
    {
        private praktikumEntities db = new praktikumEntities();

        // GET: Foren
        [HttpGet]
        public ActionResult Index(int id = 0)
        {
            var foren = db.Foren.Where(f => f.OberforumID == null);
            ViewBag.Titel = "Wurzelebene";
            ViewBag.OberID = 0;
            ViewBag.ID = id;
            if (id != 0)
            {
                foren = db.Foren.Where(f => f.OberforumID == id);
                ViewBag.Titel = db.Foren.Where(f => f.ID == id).First().Bezeichnung;
                ViewBag.OberID = db.Foren.Where(f => f.ID == id).First().OberforumID;
                var diskussionen = db.Diskussionen.Where(d => d.ForumID == id);
                if(diskussionen.Any())
                {
                    ViewBag.Diskussionen = diskussionen;
                }
            }
            return View(foren.ToList());
        }

        // GET: Foren/Diskussion/5
        [HttpGet]
        public ActionResult Diskussion(int id)
        {
            var diskussion = db.Diskussionen.Find(id);
            if (diskussion == null)
            {
                return HttpNotFound();
            }
            db.Gesichtet(id);
            ViewBag.Beitraege = db.Beiträge.Where(b => b.DiskussionsID == id);
            ViewBag.Diskussion = diskussion;
            return View();
        }

        // POST: Foren/NewPost/5
        [HttpPost]
        public ActionResult NewPost(int id, Beiträge beitrag)
        {
            if(beitrag.Mitteilung == null)
            {
                return RedirectToAction("Diskussion", "Foren", new { id = id });
            }
            beitrag.Benutzer = (string) Session["authenticated"];
            beitrag.Änderungsdatum = DateTime.Now;
            beitrag.DiskussionsID = id;
            db.Beiträge.Add(beitrag);
            db.SaveChanges();
            return RedirectToAction("Diskussion", "Foren", new { id = id });
        }

        // POST: Foren/NewDiscussion/5
        [HttpGet]
        public ActionResult NewDiscussion(int id)
        {
            var diskussion = db.Diskussionen.Find(id);
            if (diskussion == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = id;
            return View();
        }

        // POST: Foren/NewDiscussion/5
        [HttpPost]
        public ActionResult NewDiscussion(int id, NewDiscussion newDiscussion)
        {
            Diskussionen diskussion = new Diskussionen();
            diskussion.AnzahlSichtungen = 0;
            diskussion.Titel = newDiscussion.title;
            diskussion.ForumID = id;
            db.Diskussionen.Add(diskussion);
            db.SaveChanges();
            Beiträge beitrag = new Beiträge();
            beitrag.Benutzer = (string)Session["authenticated"];
            beitrag.Änderungsdatum = DateTime.Now;
            beitrag.DiskussionsID = diskussion.ID;
            beitrag.Mitteilung = newDiscussion.content;
            db.Beiträge.Add(beitrag);
            db.SaveChanges();
            return RedirectToAction("Diskussion", "Foren", new { id = diskussion.ID });
        }
    
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
