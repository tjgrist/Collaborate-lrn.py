using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Collaborate_lrn_Py.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Thanks!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "tjgrst@gmail.com";

            return View();
        }
    }
}