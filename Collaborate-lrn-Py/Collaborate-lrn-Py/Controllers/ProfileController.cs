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
using Microsoft.AspNet.Identity.EntityFramework;

namespace Collaborate_lrn_Py.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            if (isStudent())
            {
                return View("Student", currentUser);
            }
            else
            {
                var educatorsTutorials = db.Tutorials.Where(x => x.EducatorId == currentUser.Id).ToList();
                educatorsTutorials.ForEach(x => currentUser.Points += (int)x.Votes);
                var educatorsQuizzes = db.Quiz.Where(x => x.EducatorId == currentUser.Id).ToList();
                EducatorViewModel eduViewModel = new EducatorViewModel()
                {
                    EducatorTutorials = educatorsTutorials,
                    EducatorQuizzes = educatorsQuizzes,
                    Points = currentUser.Points
                };
                return View("Educator", eduViewModel);
            }
        }
        public ActionResult ShowCollaborativeTutorials()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            //List<CollaborativeTutorial> v = db.CollaborativeTutorials.Where(x => x.CollaboratorId == currentUser.Id).ToList();
            return View("_CollabTutorials");
        }
        public List<Tutorial> GetCollabTutorials()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            //try
            //{
            //    var users = db.Tutorials.Select(y => y.Collaborators.Where(x => x.Id == currentUser.Id)).ToList();
            //    List<Tutorial> collabTutorials = users.Where(x => x.Select(y => y.).ToList();
            //    if (collabTutorials != null)
            //    {
            //        return collabTutorials;
            //    }
            //    return collabTutorials;
            //}
            //catch (NotSupportedException)
            //{
            //    return new List<Tutorial>();
            //}
            return new List<Tutorial>();
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
                //tut.Collaborators.Add(user);
                CollaborativeTutorial coTut = new CollaborativeTutorial()
                {
                    Tutorial = tut,
                    TutorialId = tut.ID,
                };
                coTut.Collaborators.Add(user);
                var c = coTut.Collaborators;
                db.CollaborativeTutorials.Add(coTut);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException)
            {
                return View(collaborator);
            }
        }
        public ActionResult Complete()
        {
            var currentUser = db.Users.Find(User.Identity.GetUserId());
            db.Entry(currentUser).State = EntityState.Modified;
            currentUser.Points += 20;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Linter()
        {
            return View();
        }
        public ActionResult Dashboard()
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
        public bool isStudent()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Student")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
