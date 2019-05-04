using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using EMSProj.CollectionViewModels;

namespace EMSProj.Controllers
{
    public class EmployeeController : Controller
    {
        DB51Entities db = new DB51Entities();
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        // GET: Employee
        public ActionResult Index()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var emp = db.Employees.Where(c => c.UserId == user.Id).SingleOrDefault();

            var depRel = db.Departments.Where(c => c.depId == emp.DepId).FirstOrDefault();
            var rankRel = db.Ranks.Where(c => c.rankId == emp.Rank).FirstOrDefault();

            var collection = new CollectionofAll
            {
               Department = db.Departments.Where(c => c.depId == depRel.depId).SingleOrDefault(),
               Rank = db.Ranks.Where(c=> c.rankId == rankRel.rankId).SingleOrDefault(),
               Employee = db.Employees.Where(c=>c.empId == emp.empId).SingleOrDefault()

            };

            return View(collection);


        }
        public ActionResult EditEmployee (int id)
        {
            var model = db.Employees.SingleOrDefault(c => c.empId == id);
            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(int id, Employee model)
        {
            var employee = db.Employees.Single(c => c.empId == id);
            employee.State = model.State;
            employee.City = model.City;
            employee.DateofBirth = (model.DateofBirth);
            employee.Province = model.Province;
            employee.Cnic = model.Cnic;

            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult LoanStatus()
        {
            return View();
        }
    }
}