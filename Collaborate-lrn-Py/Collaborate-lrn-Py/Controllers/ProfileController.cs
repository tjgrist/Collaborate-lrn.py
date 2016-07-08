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
        
        public ActionResult ShowQuiz()
        {
            var currentUser = db.Users.Find(User.Identity.GetUserId());
            List<Quiz> educatorsQuizzes = db.Quiz.Where(x => x.EducatorId == currentUser.Id).ToList();
            return PartialView("_ProfileQuizzes", educatorsQuizzes);
        }

        public ActionResult AddCollaborator()
        {
            ViewData["Collaborator"] = new SelectList(db.Users.Distinct().ToList(), "UserName", "UserName");
            var user = User.Identity.GetUserId();
            ViewData["TutorialSelection"] = new SelectList(db.Tutorials.Where(x => x.EducatorId == user).ToList(), "Title", "Title");
            return View();
        }

        [HttpPost]
        public ActionResult AddCollaborator(CollaborateViewModel collaborator)
        {
            string searchedCollaborator = collaborator.InputCollaborator;
            try
            {
                ApplicationUser user = db.Users.First(x => x.UserName == searchedCollaborator);
                Tutorial tut = db.Tutorials.First(x => x.Title == collaborator.TutorialSelection);
                tut.Collaborators.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException)
            {
                return View();
            }
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
