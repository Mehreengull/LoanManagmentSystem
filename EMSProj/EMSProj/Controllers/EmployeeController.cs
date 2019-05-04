using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using EMSProj.CollectionViewModels;
using System.Threading.Tasks;

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

        public ActionResult AddLoan()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var emp = db.Employees.Where(c => c.UserId == user.Id).SingleOrDefault();

            var l = db.Loans.Where(c => c.empId == emp.empId).ToList();
            foreach(var lonVal in l)
            {
                if(lonVal.loanStatus == "p")
                {
                                        
                    return RedirectToAction("LoanList", new { error = "Loan Request is Pending" });
                }
                else
                {
                    ViewBag.error = "";
                    var collection = new EmployeeLoanCollectionViewModel
                    {
                        Loan = new Loan(),
                        Employee = emp,

                    };

                    return View(collection);
                }
            }
            return HttpNotFound();


        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddLoan(EmployeeLoanCollectionViewModel model)
        {
            var loan = new Loan
            {
                empId = model.Employee.empId,
                requestAmount = model.Loan.requestAmount,
                requestDate = DateTime.Now.Date,
                loanAmount = 0,
                noOfInsatllments = 12,
                loanStatus = "P",
                // Pending : P

                
                

            };


            db.Loans.Add(loan);
            db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }

        public ActionResult LoanStatus()
        {

            var user = UserManager.FindById(User.Identity.GetUserId());
            var emp = db.Employees.Where(c => c.UserId == user.Id).SingleOrDefault();

            var loan = db.Loans.Where(c => c.empId == emp.empId).SingleOrDefault();

            var collection = new EmployeeLoanCollectionViewModel
            {
                Loan = db.Loans.Where(c=>c.loanId == loan.loanId).SingleOrDefault(),
               
                
                Employee = db.Employees.Where(c => c.empId == emp.empId).SingleOrDefault()

            };

            return View(collection);
        }
        
        public ActionResult LoanList(string error = "")
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var emp = db.Employees.Where(c => c.UserId == user.Id).SingleOrDefault();

            var loan = db.Loans.Where(c => c.empId == emp.empId).ToList();
            ViewBag.name = emp.FirstName + " " + emp.LastName;
            ViewBag.error = error;

            return View(loan);
        }
    }
}