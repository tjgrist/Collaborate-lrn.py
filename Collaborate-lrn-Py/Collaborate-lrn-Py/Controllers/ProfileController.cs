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
                educatorsTutorials.ForEach(x => currentUser.Points += (int)x.Rating);
                var educatorsQuizzes = db.Quiz.Where(x => x.EducatorId == currentUser.Id).ToList();
                EducatorViewModel eduViewModel = new EducatorViewModel()
                {
                    EducatorTutorials = educatorsTutorials,
                    EducatorQuizzes = educatorsQuizzes,
                    Points = currentUser.Points
                };
                return View("Educator", eduViewModel);
            }        
        //    try
        //    {
        //        var collabTutorials = db.Tutorials.Select(y => y.Collaborators.Where(x => x.Id == currentUser.Id)).ToList(); 
        //        //List<Tutorial> usercollabs = collabTutorials.Where(x => x.Collaborators.First(y => y.Id == currentUser.Id)).ToList();
        //        if (collabTutorials != null)
        //        {
        //             PartialView("_CollaborativeTutorials", collabTutorials);
        //        }
        //    }
        //    catch (NotSupportedException)
        //    {
        //        return View("Profile", educatorsTutorials);
        //    }
        }

        public ActionResult AddCollaborator()
        {
            ViewData["Collaborator"] = new SelectList(db.Users.Distinct().ToList(), "UserName", "UserName");
            var user = User.Identity.GetUserId();
            ViewData["TutorialSelection"] = new SelectList(db.Tutorials.Where(x => x.EducatorId == user).ToList(), "Title", "Title");
            return View();
        }

        public ActionResult Complete()
        {
            var currentUser = db.Users.Find(User.Identity.GetUserId());
            db.Entry(currentUser).State = EntityState.Modified;
            currentUser.Points += 5;
            currentUser.CompletedTutorialsCount += 1;
            db.SaveChanges();
            return RedirectToAction("Index");
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
                return View(collaborator);
            }
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
