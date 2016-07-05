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
    public class QuizController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Quiz
        public ActionResult Index()
        {
            var quiz = db.Quiz.Include(q => q.Educator).Include(q => q.Tutorial);
            return View(quiz.ToList());
        }

        // GET: Quiz/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quiz.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // GET: Quiz/Create
        [Authorize]
        public ActionResult Create()
        {
            var user = User.Identity.GetUserId();
            ViewBag.SelectTutorial = new SelectList(db.Tutorials.Where(x => x.EducatorId == user).ToList(), "Title", "Title");
            //do something to link tutID and quizID
            return View();
        }

        // POST: Quiz/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Instruction,TutorialId")] QuizViewModel model)
        {
            if (ModelState.IsValid)
            {
                Quiz quiz = new Quiz
                {
                    Name = model.Name,
                    Instruction = model.Instruction,
                    EducatorId = User.Identity.GetUserId(),
                    Tutorial = model.TutorialSelection
                };
                db.Quiz.Add(quiz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Quiz/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quiz.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            ViewBag.TutorialId = new SelectList(db.Tutorials, "ID", "Title", quiz.TutorialId);
            return View(quiz);
        }

        // POST: Quiz/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Instruction,EducatorId,TutorialId")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TutorialId = new SelectList(db.Tutorials, "ID", "Title", quiz.TutorialId);
            return View(quiz);
        }

        // GET: Quiz/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quiz.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // POST: Quiz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quiz quiz = db.Quiz.Find(id);
            db.Quiz.Remove(quiz);
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
