using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularAuthentication.Controllers
{
    public class AngularController : Controller
    {
        //
        // GET: /Angular/
        public ActionResult Index()
        {
            ViewBag.Message = "This is AngularJS version of Authentication Demo App.";
            return View();
        }
	}
}