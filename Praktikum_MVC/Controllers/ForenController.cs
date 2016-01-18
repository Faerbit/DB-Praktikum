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
            return View();
        }

        // POST: Foren/NewDiscussion/5
        [HttpPost]
        public ActionResult NewDiscussion(int id, Beiträge beitrag)
        {
            beitrag.Benutzer = (string)Session["authenticated"];
            //db.SaveChanges();
            return RedirectToAction("Diskussion", "Foren", new { id = id });
        }

        /*
        // GET: Foren/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Foren foren = db.Foren.Find(id);
            if (foren == null)
            {
                return HttpNotFound();
            }
            return View(foren);
        }

        // GET: Foren/Create
        public ActionResult Create()
        {
            ViewBag.OberforumID = new SelectList(db.Foren, "ID", "Bezeichnung");
            return View();
        }

        // POST: Foren/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Bezeichnung,OberforumID")] Foren foren)
        {
            if (ModelState.IsValid)
            {
                db.Foren.Add(foren);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OberforumID = new SelectList(db.Foren, "ID", "Bezeichnung", foren.OberforumID);
            return View(foren);
        }

        // GET: Foren/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Foren foren = db.Foren.Find(id);
            if (foren == null)
            {
                return HttpNotFound();
            }
            ViewBag.OberforumID = new SelectList(db.Foren, "ID", "Bezeichnung", foren.OberforumID);
            return View(foren);
        }

        // POST: Foren/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Bezeichnung,OberforumID")] Foren foren)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foren).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OberforumID = new SelectList(db.Foren, "ID", "Bezeichnung", foren.OberforumID);
            return View(foren);
        }

        // GET: Foren/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Foren foren = db.Foren.Find(id);
            if (foren == null)
            {
                return HttpNotFound();
            }
            return View(foren);
        }

        // POST: Foren/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Foren foren = db.Foren.Find(id);
            db.Foren.Remove(foren);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

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
