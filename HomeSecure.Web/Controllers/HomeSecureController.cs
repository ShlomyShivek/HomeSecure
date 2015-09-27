using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeSecure.Data.Entities;

namespace HomeSecure.Web.Controllers
{
    public class HomeSecureController : Controller
    {
        //
        // GET: /HomeSecure/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MotionDetection()
        {
            ViewBag.NumTimes = 5;
            ViewBag.Test = "Message";
            MotionDetectionEvent a = new MotionDetectionEvent() { ID = 555 };
            return View(a);
        }
    }
}
