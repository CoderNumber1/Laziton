using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarlAnthonyJames.Com.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
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
