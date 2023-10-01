using Stripe;
using StripeDemoApp.Data;
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
        private readonly TenantService tenantInfoService;
        private readonly AppEventsService eventService;
        private readonly AppDataContext db;

        public HomeController(TenantService tenantInfoService, AppEventsService eventService, AppDataContext db)
        {
            this.tenantInfoService = tenantInfoService;
            this.eventService = eventService;
            this.db = db;
        }

        public ActionResult Index()
        {
            return View(eventService.GetEvents());
        }

        public ActionResult About()
        {


            //var options = new AccountCreateOptions { Type = "standard",  };
            //var service = new AccountService();
            //service.Create(options);


            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            eventService.RegisterTestEvents();

            return View();
        }
    }
}