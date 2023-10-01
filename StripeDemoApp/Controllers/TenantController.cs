using StripeDemoApp.Models;
using StripeDemoApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StripeDemoApp.Controllers
{
    public class TenantController : Controller
    {
        private readonly TenantService tenantService;

        public TenantController(TenantService tenantService)
        {
            this.tenantService = tenantService;
        }
        // GET: Tenant
        public ActionResult Index()
        {
            var tenants = tenantService.GetAllTenantViewModel();
            return View(tenants);
        }

        // GET: Tenant/Details/5
        public ActionResult Details(int id)
        {
            var tenant = tenantService.GetTenantInfoViewModel(id);

            return View(tenant);
        }

        // GET: Tenant/Create
        public ActionResult Create()
        {
            return View(new TenantInfoViewModel());
        }

        // POST: Tenant/Create
        [HttpPost]
        public ActionResult Create( TenantInfoViewModel tenantInfo)
        {
            try
            {
                // TODO: Add insert logic here
                tenantService.CreateEditTenantInfo(tenantInfo);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tenant/Edit/5
        public ActionResult Edit(int id)
        {
            var tenant = tenantService.GetTenantInfoViewModel(id);

            return View(tenant);
        }

        // POST: Tenant/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TenantInfoViewModel tenantInfo)
        {
            try
            {
                // TODO: Add update logic here
                if (id != tenantInfo.Id) return View();

                tenantService.CreateEditTenantInfo(tenantInfo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tenant/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tenant/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
