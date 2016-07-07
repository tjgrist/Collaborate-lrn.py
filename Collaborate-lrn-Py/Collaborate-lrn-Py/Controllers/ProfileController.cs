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
    [Authorize]
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            var educatorsTutorials = db.Tutorials.Where(x => x.EducatorId == currentUser.Id).ToList();
            return View("Profile", educatorsTutorials);
        }

        // GET: ApplicationUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }
        
        public ActionResult ShowQuiz()
        {
            var currentUser = db.Users.Find(User.Identity.GetUserId());
            List<Quiz> educatorsQuizzes = db.Quiz.Where(x => x.EducatorId == currentUser.Id).ToList();
            return PartialView("_ProfileQuizzes", educatorsQuizzes);
        }
        public ActionResult Linter()
        {
            return View();
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
