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

namespace Collaborate_lrn_Py.Controllers
{
    public class PathsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Paths
        public ActionResult Index()
        {
            var paths = db.Paths.Where(p => p.Published == true).ToList();
            return View(paths);
        }

        // GET: Paths/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Path path = db.Paths.Find(id);
            if (path == null)
            {
                return HttpNotFound();
            }
            return View(path);
        }

        // GET: Paths/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paths/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PathName,Votes,CreatorId")] Path path)
        {
            if (ModelState.IsValid)
            {
                path.Creator = db.Users.Find(User.Identity.GetUserId());
                path.CreatorId = path.Creator.Id;
                db.Paths.Add(path);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(path);
        }

        public ActionResult AddToUsersPaths(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Path path = db.Paths.Find(id);
            if (path == null)
            {
                return HttpNotFound();
            }
            if (path != null)
            {
                var currentUser = db.Users.Find(User.Identity.GetUserId());
                if (currentUser.LearningPathModel == null)
                {
                    currentUser.LearningPathModel = new LearningPathModel();
                    db.SaveChanges();
                }
                currentUser.LearningPathModel.LearningPaths.Add(path);
                //db.LearningPaths.Add()
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Profile", null);
        }
        
        public ActionResult AddTutorialToPath(int? id)
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
                    var currentUser = db.Users.Find(User.Identity.GetUserId());
                    ViewData["PathSelection"] = new SelectList(currentUser.Paths.ToList(), "PathName", "PathName");
                    PathViewModel pathModel = new PathViewModel() { TutorialId = tutorial.ID};
                    return View(pathModel);
                }
                catch (InvalidOperationException)
                {
                    return View();
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddTutorialToPath([Bind(Include = "TutorialId,PathSelection")] PathViewModel model)
        {
            var currentUser = db.Users.Find(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                Path selectedPath = currentUser.Paths.First(x => x.PathName == model.PathSelection);
                Tutorial tutorial = db.Tutorials.First(x => x.ID == model.TutorialId);
                selectedPath.Tutorials.Add(tutorial);
                db.Entry(currentUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Profile");
            }            
            ViewData["PathSelection"] = new SelectList(currentUser.Paths.ToList(), "PathName", "PathName");
            return View(model);

        }

        public ActionResult Publish(int? id)
        {
            //need to make sure somewhere it's the proper publisher
            using (var db = new ApplicationDbContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Path path = db.Paths.Find(id);
                if (path == null)
                {
                    return HttpNotFound();
                }
                if (path != null)
                {
                    try
                    {
                        path.Published = true;
                        db.Entry(path).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Profile");
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

        public ActionResult UpVote(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Path path = db.Paths.Find(id);
            if (path == null)
            {
                return HttpNotFound();
            }
            if (path != null)
            {
                path.Votes += 1;
                db.Entry(path).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Paths/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Path path = db.Paths.Find(id);
            if (path == null)
            {
                return HttpNotFound();
            }

            return View(path);
        }

        // POST: Paths/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PathName,Votes,CreatorId")] Path path)
        {
            if (ModelState.IsValid)
            {
                db.Entry(path).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(path);
        }

        // GET: Paths/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Path path = db.Paths.Find(id);
            if (path == null)
            {
                return HttpNotFound();
            }
            return View(path);
        }

        // POST: Paths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Path path = db.Paths.Find(id);
            db.Paths.Remove(path);
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
