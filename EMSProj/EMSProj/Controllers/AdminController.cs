using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMSProj.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DB51Entities1 db = new DB51Entities1();
        public ActionResult Index()
        {
            return View();
        }

        // Doing Nothing
        public ActionResult UpdateCompanyInfo()
        {
            return View();
        }
        [HttpGet]
        // Doing Nothing
        public ActionResult CreateCompany()
        {
            return View();
        }
        [HttpPost]
        // Doing Nothing
        public ActionResult CreateCompany(Company model)
        {
            db.Companies.Add(model);
            db.SaveChanges();
            return RedirectToAction("CompaniesList");
        }
        [HttpGet]
        // Showing Companies Information
        public ActionResult CompaniesList()
        {
            var model = db.Companies.ToList();
            return View(model);
        }

        public ActionResult EditCompany(int id)
        {
            var model = db.Companies.SingleOrDefault(c => c.Code == id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCompany(int id, Company model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var com = db.Companies.Single(c => c.Code == id);
            com.Name = model.Name;
            com.Email = model.Email;
            com.PhoneNo = model.PhoneNo;
            com.PostalCode = model.PostalCode;
            com.Url = model.Url;
            com.Fax = model.Fax;
            db.SaveChanges();
            return RedirectToAction("CompaniesList");
        }
    }
}