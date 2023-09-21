using StripeDemoApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StripeDemoApp.Controllers
{
    public class HomeController : Controller
    {
        private EventService eventService = new EventService();
        public ActionResult Index()
        {
            return View(eventService.GetEvents());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}