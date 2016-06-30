using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Collaborate_lrn_Py.Models;
using Microsoft.AspNet.Identity;

namespace Collaborate_lrn_Py.Controllers
{
    public class TutorialsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tutorials
        public ActionResult Index()
        {
            return View(db.Tutorials.ToList());
        }

        // GET: Tutorials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorial tutorial = db.Tutorials.Find(id);
            if (tutorial == null)
            {
                return HttpNotFound();
            }
            return View(tutorial);
        }

        // GET: Tutorials/Create
        [Authorize(Roles = "Educator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tutorials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TutorialViewModel model)
        {
            if (ModelState.IsValid)
            {
                //do creation logic
                var tutorial = new Tutorial
                {
                    Title = model.Title,
                    Description = model.Description,
                    Difficulty = model.Difficulty,
                    BodyText = model.BodyText,
                    CreationDate = DateTime.Now,
                    EducatorId = User.Identity.GetUserId()
                }; 
                db.Tutorials.Add(tutorial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Tutorials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorial tutorial = db.Tutorials.Find(id);
            if (tutorial == null)
            {
                return HttpNotFound();
            }
            return View(tutorial);
        }

        // POST: Tutorials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Difficulty,CreationDate,Rating,EducatorId")] Tutorial tutorial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tutorial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tutorial);
        }

        // GET: Tutorials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorial tutorial = db.Tutorials.Find(id);
            if (tutorial == null)
            {
                return HttpNotFound();
            }
            return View(tutorial);
        }

        // POST: Tutorials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tutorial tutorial = db.Tutorials.Find(id);
            db.Tutorials.Remove(tutorial);
            db.SaveChanges();
            return RedirectToAction("Index");
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
