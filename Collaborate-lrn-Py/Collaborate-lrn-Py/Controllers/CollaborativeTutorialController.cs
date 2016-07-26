using Collaborate_lrn_Py.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Collaborate_lrn_Py.Controllers
{
    public class CollaborativeTutorialController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: CollaborativeTutorial
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Collaborate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollaborativeTutorial collabtutorial = db.CollaborativeTutorials.Find(id);
            Tutorial t = db.Tutorials.First(x => x.ID == collabtutorial.TutorialId);
            collabtutorial.Tutorial = t;
            collabtutorial.Comments = collabtutorial.Comments.OrderBy(x => x.TimeStamp).ToList();
            if (collabtutorial == null)
            {
                return HttpNotFound();
            }
            return View(collabtutorial);
        }
        [HttpPost]
        public ActionResult Collaborate(CollaborativeTutorial model)
        {
            CollaborativeTutorial collabTutorial = db.CollaborativeTutorials.Find(model.Id);
            Tutorial tut = db.Tutorials.First(x => x.ID == model.TutorialId);
            Comment comment = new Comment()
            {
                UserName = User.Identity.GetUserName(),
                Text = model.newComment,
                TimeStamp = DateTime.Now
            };
            collabTutorial.Comments.Add(comment);
            //db.Entry(collabTut).State = System.Data.Entity.EntityState.Modified;
            collabTutorial.Comments = collabTutorial.Comments.OrderBy(x => x.TimeStamp).ToList();
            db.SaveChanges();

            return RedirectToAction("Collaborate");
        }

        // POST: Tutorials/Delete/5
        public ActionResult DeleteComment(int? id, int? ctId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            db.Comments.Remove(comment);
            db.SaveChanges();
            return Redirect("http://localhost:13113/CollaborativeTutorial/Collaborate/" + ctId.ToString());
        }
    }
}