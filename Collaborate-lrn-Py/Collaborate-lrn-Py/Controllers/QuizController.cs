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
using System.Diagnostics;

namespace Collaborate_lrn_Py.Controllers
{
    public class QuizController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Quiz
        public ActionResult Index()
        {
            var quiz = db.Quiz.ToList();
            return View(quiz);
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
        [Authorize(Roles = "Educator")]
        public ActionResult Create()
        {
            var user = User.Identity.GetUserId();
            ViewData["TutorialSelection"] = new SelectList(db.Tutorials.Where(x => x.EducatorId == user).ToList(), "Title", "Title");
            //do something to link tutID and quizID
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Educator")]
        public ActionResult Create(QuizViewModel model)
        {
            var user = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                Quiz quiz = new Quiz
                {
                    TutorialId = db.Tutorials.First(x => x.Title == model.TutorialSelection).ID,
                    EducatorId = User.Identity.GetUserId(),
                    Name = model.Name,
                    Goal = model.Goal,
                    Instruction = model.Instruction,
                    DisplayedCode = model.DisplayedCode,
                    ExpectedInput = model.ExpectedInput,
                    ExpectedOutput = model.ExpectedOutput,
                    Answer = model.Answer
                };
                db.Quiz.Add(quiz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["TutorialSelection"] = new SelectList(db.Tutorials.Where(x => x.EducatorId == user).ToList(), "Title", "Title");
            return View();
        }


        [Authorize(Roles = "Educator")]
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Educator")]
        public ActionResult Edit(Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quiz);
        }

        [Authorize(Roles = "Educator")]
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

        [Authorize(Roles = "Educator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quiz quiz = db.Quiz.Find(id);
            db.Quiz.Remove(quiz);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // [Authorize(Roles = "Student")]
        [HttpPost]
        public ActionResult AutoGrade(GradeViewModel model)
        {

            int? QuizId = Convert.ToInt32(model.expected);
            if (QuizId != null)
            {
                Quiz quiz = db.Quiz.First(x => x.Id == QuizId);

                //Debug.WriteLine("your code: " + model.yourcode + "L: " + model.yourcode.Length);
                //Debug.WriteLine("Ouput: " + model.output.ToString().Trim());
                //Debug.WriteLine("expected: " + quiz.ExpectedOutput.Trim());
                //var newyourcode = model.yourcode.Trim().Replace(" ", "");
                //Debug.WriteLine("your code trimmed: " + newyourcode + newyourcode.Length);
                //var replacequizexp = quiz.ExpectedInput.Trim().Replace(" ", "");
                //Debug.WriteLine("the quiz expected: " + replacequizexp + "..." + replacequizexp.Length);
                //Debug.WriteLine(newyourcode.Equals(replacequizexp));
                //Debug.WriteLine(model.output.Trim().Equals(quiz.ExpectedOutput));
                try
                {
                    string output = model.output.ToString().Trim();
                    //if (output.Equals(quiz.ExpectedOutput) && model.yourcode.Length.Equals(18))
                    //{
                    //    ViewBag.Message = "Nice try, but you cannot simply print the proper output.";
                    //}
                    if (output.Equals(quiz.ExpectedOutput) && model.yourcode.Length > quiz.ExpectedOutput.Length + 10)
                    {
                        ViewBag.Message = "Well done!";
                    }
                    else
                    {
                        ViewBag.Message = "Something may be wrong with your input. Try again.";
                    }
                }
                catch (NullReferenceException e)
                {
                    ViewBag.Message = e.Message + " It looks like the output of your code was empty.";
                }
                return PartialView("_Grade");
            }
            return View();
        }

        //could use .split at new line and spaces and iterate thru to match each substring 
        //could use to charArray() and do something similar
        //could check multiple times

        private bool CheckOutput(string output, Quiz quiz, string yourcode)
        {
            if (output.Equals(quiz.ExpectedOutput) && yourcode.Length == quiz.ExpectedOutput.Length + 9)
            {
                return false;
            }
            if (output.Equals(quiz.ExpectedOutput) && yourcode.Length > quiz.ExpectedOutput.Length + 10) //&& output.Length > quiz.ExpectedOutput.Length + 10
            {
                return true;
            }
            else
            {
                return false;
            }
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
