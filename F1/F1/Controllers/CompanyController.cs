using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F1.Models;
namespace F1.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }

        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        [HttpPost]
        public ActionResult Create(companyViewModel model)
        {
            try
            {
                DB51Entities db = new DB51Entities();
                Company c = new Company();
                c.Code = Convert.ToInt16(model.Cid);
                c.Name = model.coName;
                c.Fax = model.fax;
                c.Email = model.email;
                c.PhoneNo = model.phoneNumber;
                c.PostalCode = model.postalCode;
                c.Url = model.url;
                db.Companies.Add(c);
                db.SaveChanges();


                return View();

               // return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Company/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Company/Delete/5
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
