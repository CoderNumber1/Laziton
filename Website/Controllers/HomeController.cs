using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [KarlAnthonyJames.Com.Util.ExceptionHandlerArea(RegisteredAreaName = "MyArea")]
        public ActionResult Future()
        {
            throw new Exception("WHOOPS!");
            return View();
        }

        public ActionResult AuthComplete()
        {
            return new ContentResult() { Content = "SUCCESS!" };
        }
    }
}
