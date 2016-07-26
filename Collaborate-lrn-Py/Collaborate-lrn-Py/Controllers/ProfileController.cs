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
            if (currentUser.LearningPathModel == null)
            {
                currentUser.LearningPathModel = new LearningPathModel();
            }
            if (isStudent())
            {
                return View("Student", currentUser);
            }
            else
            {
                var popularTutorials = db.Tutorials.OrderByDescending(x => x.Votes).Take(5).ToList();
                var educatorsTutorials = db.Tutorials.Where(x => x.EducatorId == currentUser.Id).ToList();
                educatorsTutorials.ForEach(x => currentUser.Points += x.Votes);
                var educatorsQuizzes = db.Quiz.Where(x => x.EducatorId == currentUser.Id).ToList();
                EducatorViewModel eduViewModel = new EducatorViewModel()
                {
                    EducatorTutorials = educatorsTutorials,
                    CollaborativeTutorials = GetCollabTutorials(),
                    EducatorQuizzes = educatorsQuizzes,
                    Points = currentUser.Points,
                    PopularTutorials = popularTutorials
                };
                return View("Educator", eduViewModel);
            }
        }
        private List<CollaborativeTutorial> GetCollabTutorials()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            try
            {
                var collaborationsList = currentUser.Collaborations.ToList();
                var collabtuts = new List<CollaborativeTutorial>() { };
                foreach (var item in collaborationsList)
                {
                    item.Tutorial = db.Tutorials.Find(item.TutorialId);
                    collabtuts.Add(item);
                }
                return collabtuts;
            }
            catch (NotSupportedException)
            {
                return null;
            }
        }

        public ActionResult AddCollaborator()
        {
            ViewData["Collaborator"] = new SelectList(db.Users.Distinct().ToList(), "UserName", "UserName");
            var user = User.Identity.GetUserId();
            ViewData["TutorialSelection"] = new SelectList(db.Tutorials.Where(x => x.EducatorId == user).ToList(), "Title", "Title");
            return View();
        }

        [HttpPost]
        public ActionResult AddCollaborator(CollaborateViewModel collaborate)
        {
            string searchedCollaborator = collaborate.InputCollaborator;
            try
            {
                ApplicationUser searchedUser = db.Users.First(x => x.UserName == searchedCollaborator);
                Tutorial tut = db.Tutorials.First(x => x.Title == collaborate.TutorialSelection);
                CollaborativeTutorial collaborativeTutorial = new CollaborativeTutorial()
                {
                    Tutorial = tut,
                    TutorialId = tut.ID,
                };
                collaborativeTutorial.Collaborators.Add(searchedUser);
                searchedUser.Collaborations.Add(collaborativeTutorial);
                db.Entry(searchedUser).State = EntityState.Modified;
                db.CollaborativeTutorials.Add(collaborativeTutorial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException)
            {
                return View(collaborate);
            }
        }

        [Authorize(Roles = "Student")]
        public ActionResult Complete()
        {
            var currentUser = db.Users.Find(User.Identity.GetUserId());
            db.Entry(currentUser).State = EntityState.Modified;
            currentUser.Points += 5;
            currentUser.CompletedTutorialsCount += 1;
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
