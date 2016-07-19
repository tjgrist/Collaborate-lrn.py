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
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Collaborate_lrn_Py.Controllers
{
    [Authorize]
    public class TutorialsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tutorials
        public ActionResult Index(string searchString)
        {
            var publishedTutorials = db.Tutorials.Where(x => x.Published == true).ToList();
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    var searchTutorials = db.Tutorials.Where(x => x.Title.Contains(searchString));
            //    return View(searchTutorials);
            //}
            if (isStudent())
            {
                return View("Public", publishedTutorials);
            }
            return View(publishedTutorials);
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
        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Educator")]
        public ActionResult Create(TutorialViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tutorial = new Tutorial
                {
                    Title = model.Title,
                    Description = model.Description,
                    Difficulty = model.Difficulty,
                    BodyText = model.BodyText,
                    CodeSample = model.CodeSample,
                    CreationDate = DateTime.Now,
                    EducatorId = User.Identity.GetUserId()
                    //Votes = 0
                }; 
                db.Tutorials.Add(tutorial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize(Roles = "Educator")]
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
            if (tutorial.EducatorId == User.Identity.GetUserId())
            {
                return View(tutorial);
            }
            ViewBag.Message = "You cannot edit that tutorial.";
            return RedirectToAction("Index", ViewBag.Message);
        }

        // POST: Tutorials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Educator")]
        public ActionResult Edit(Tutorial tutorial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tutorial).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "You edited your Tutorial! Make sure to re-publish it.";
                return RedirectToAction("Index");
            }
            return View(tutorial);
        }

        // GET: Tutorials/Delete/5
        [Authorize(Roles = "Educator")]
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

        [Authorize(Roles = "Educator")]
        public ActionResult Publish(int? id)
        {
            //need to make sure somewhere it's the proper publisher
            using (var db = new ApplicationDbContext())
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
                if (tutorial != null)
                {
                    try
                    {
                        tutorial.Published = true;
                        db.Entry(tutorial).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        foreach (var eve in dbEx.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                }
            }
            return RedirectToAction("Profile", "Profiles");
        }   
        [Authorize]
        public ActionResult TakeTutorial(int? id)
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
            if (tutorial != null)
            {
                TutQuizViewModel model = new TutQuizViewModel() { ModelTutorial = tutorial };
                try
                {
                    Quiz quiz = db.Quiz.First(x => x.TutorialId == tutorial.ID);
                    model.ModelQuiz = quiz;
                    return View(model);
                }
                catch (InvalidOperationException)
                {
                    ViewBag.Message = "There doesn't seem to be a quiz associated with this tutorial yet.";
                    model.ModelQuiz = null;
                    return View(model);
                }

                
            }
            return View();
        }
        [Authorize(Roles = "Student")]   
        public ActionResult UpVote(int? id)
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
            if (tutorial != null)
            {
                tutorial.Votes += 1;
                db.Entry(tutorial).State = EntityState.Modified;
                db.SaveChanges();
            }
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
